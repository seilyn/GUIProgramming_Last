/**********************************
 * ��ũ��Ʈ ��� : Character Controller
 * �ۼ��� : 2021-12-02
 * �ۼ��� : ������
 * ********************************/

// 12-07 ������ ���� : ���� ��� ����
using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;

    // Inspector â���� ������ �����ϵ��� public���� �����մϴ�.
    public int characterSpeed;      
    public float characterSubSpeed;
    public float characterJumpPower;

    public GameObject[] weapons;
    public bool[] hasWeapons;

    Vector3 moveVector;
    Animator animator;
    Rigidbody rigidBody;

    bool walkKeyDown;
    bool jumpKeyDown;   // ĳ���� ����
    bool attackKeyDown; // ����
    bool qKeyDown;      // Q Ű �Է�
    bool eKeyDown;     

    bool swapKeyDown;
    bool weaponTypeA;
    bool weaponTypeB;
    

    bool isJump;        // ���� ����
    bool isAttack;      // ���� ���� 
    bool isAtkReady;    // ���� �غ� ����
                        
    float atkDelay;     // ���� ������
    GameObject nearObject;
    Weapons equipWeapon; // ������ ����

    /// <summary>
    /// MonoBehavior Method
    /// </summary>
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �� �����Ӹ��� Update �Լ��� ȣ���մϴ�.
    /// </summary>
    void Update()
    {
        GetInput();
        CharacterMove();
        CharacterTurn();
        SetCharacterAnimation();
        CharacterJump();
        CharacterAttack();
        Interaction();
        Swap();
    }


    /// <summary>
    /// Ű�� �޾ƿɴϴ�.
    /// </summary>
    private void GetInput()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis   = Input.GetAxisRaw("Vertical");
        walkKeyDown    = Input.GetButton("Walk");
        jumpKeyDown    = Input.GetButton("Jump");
        attackKeyDown  = Input.GetButton("Attack");
        qKeyDown       = Input.GetButtonDown("Interaction");
        weaponTypeA = Input.GetButtonDown("SwapA");
        weaponTypeB = Input.GetButtonDown("SwapB");
    }
    /// <summary>
    /// ĳ������ �̵��� �����մϴ�.
    /// </summary>
    private void CharacterMove()
    {
        moveVector = new Vector3(horizontalAxis, 0, verticalAxis).normalized; // ���Ⱚ�� 1�� �����ϱ� ���� normalized�� ����մϴ�.

        // walkKeyDown(Shift) Ű�� ������ ��, �̵��ӵ��� �������� �����մϴ�.
        if (walkKeyDown) characterSubSpeed = 0.3f;
        else characterSubSpeed = 1.0f;
        
        transform.position += moveVector * characterSpeed * characterSubSpeed* Time.deltaTime;
    }
    /// <summary>
    /// ĳ������ �ִϸ��̼��� �����մϴ�.
    /// </summary>
    private void SetCharacterAnimation()
    {
        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkKeyDown);
        animator.SetBool("isAttack", attackKeyDown);
    }
    /// <summary>
    /// ĳ������ ȸ���� �����մϴ�.
    /// LookAt : ������ ���͸� ���Ͽ� ȸ�������ݴϴ�.
    /// </summary>
    private void CharacterTurn()
    {
        transform.LookAt(transform.position + moveVector); // ���ư��� �������� ĳ���Ͱ� �ٶ󺾴ϴ�.
    }
    /// <summary>
    /// ĳ������ ��������� �����մϴ�.
    /// </summary>
    private void CharacterJump()
    {
        if (jumpKeyDown && !isJump) // ����Ű�� ������, ���� ���°� False�϶��� Jump ����� �����մϴ�.
        {
            rigidBody.AddForce(Vector3.up * 3, ForceMode.Impulse); // ������� Jump ����� �����մϴ�.
            isJump = true;
        }
    }
   
    /// <summary>
    /// Jump ���°� �ƴ� �� ���⸦ ��ü�ϴ� �Լ��� �����մϴ�.
    /// </summary>
    private void Swap()
    {
        int weaponIndex = -1;
        if (weaponTypeA) weaponIndex = 1;
        if (weaponTypeB) weaponIndex = 0;
        if ((weaponTypeA || weaponTypeB) && !isJump) //���� ���°� �ƴϸ鼭, ������ Type�� A�ų� B �϶� ����˴ϴ�.
        {
            // ����϶��� ��Ȱ��ȭ�մϴ�.
            if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
            equipWeapon = weapons[weaponIndex].GetComponent<Weapons>(); // Weapons Ÿ������ ����ȯ�մϴ�.
            weapons[weaponIndex].SetActive(true);
        }

    }
    private void CharacterAttack()
    {
        if (equipWeapon == null) return;

        // ���ݵ����̿� �ð��� �����ݴϴ�.
        atkDelay += Time.deltaTime;

        //���ݰ��� ���θ� üũ�մϴ�.
        isAtkReady = equipWeapon.atkSpeed < atkDelay; 

        // ���� ������ 'E' Ű�� �������� ��, ���� ���� ���ΰ� True�϶�, �׸��� ���� ���°� �ƴҶ� ���������� �����մϴ�.
        if (attackKeyDown && isAtkReady && !isJump) 
        {
            // ������ �մϴ�.
            equipWeapon.TypeCheck();
            atkDelay = 0;
        }
    }

    /// <summary>
    /// ��ó ��ü�� null ���� �ƴϸ鼭, ���� ���°� �ƴϰ�, Q Ű�� ������ �� �����ϴ� �Լ��� �����մϴ�.
    /// </summary>
    private void Interaction()
    {
        if (qKeyDown && nearObject != null && !isJump)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    /// <summary>
    /// Jump ���� ���¸� ���� �浹 ���� �Լ��� �����մϴ�.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BaseTile") isJump = false; // ĳ���Ͱ� �ٴڿ� ����� �� jump ���¸� False�� �����մϴ�.
    }

    /// <summary>
    /// ������ �ִ� ���⸦ �����մϴ�.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
        Debug.Log(nearObject.name);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }

    
}
