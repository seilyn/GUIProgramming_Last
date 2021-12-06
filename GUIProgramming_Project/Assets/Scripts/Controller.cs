/**********************************
 * 스크립트 기능 : Character Controller
 * 작성일 : 2021-12-02
 * 작성자 : 유찬영
 * ********************************/

// 12-07 유찬영 수정 : 공격 기능 구현
using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;

    // Inspector 창에서 수정이 용이하도록 public으로 선언합니다.
    public int characterSpeed;      
    public float characterSubSpeed;
    public float characterJumpPower;

    public GameObject[] weapons;
    public bool[] hasWeapons;

    Vector3 moveVector;
    Animator animator;
    Rigidbody rigidBody;

    bool walkKeyDown;
    bool jumpKeyDown;   // 캐릭터 점프
    bool attackKeyDown; // 공격
    bool qKeyDown;      // Q 키 입력
    bool eKeyDown;     

    bool swapKeyDown;
    bool weaponTypeA;
    bool weaponTypeB;
    

    bool isJump;        // 점프 상태
    bool isAttack;      // 공격 상태 
    bool isAtkReady;    // 공격 준비 상태
                        
    float atkDelay;     // 공격 딜레이
    GameObject nearObject;
    Weapons equipWeapon; // 장착된 무기

    /// <summary>
    /// MonoBehavior Method
    /// </summary>
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 매 프레임마다 Update 함수를 호출합니다.
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
    /// 키를 받아옵니다.
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
    /// 캐릭터의 이동을 구현합니다.
    /// </summary>
    private void CharacterMove()
    {
        moveVector = new Vector3(horizontalAxis, 0, verticalAxis).normalized; // 방향값을 1로 보정하기 위해 normalized를 사용합니다.

        // walkKeyDown(Shift) 키를 눌렀을 때, 이동속도가 느려지게 설정합니다.
        if (walkKeyDown) characterSubSpeed = 0.3f;
        else characterSubSpeed = 1.0f;
        
        transform.position += moveVector * characterSpeed * characterSubSpeed* Time.deltaTime;
    }
    /// <summary>
    /// 캐릭터의 애니메이션을 설정합니다.
    /// </summary>
    private void SetCharacterAnimation()
    {
        animator.SetBool("isRun", moveVector != Vector3.zero);
        animator.SetBool("isWalk", walkKeyDown);
        animator.SetBool("isAttack", attackKeyDown);
    }
    /// <summary>
    /// 캐릭터의 회전을 구현합니다.
    /// LookAt : 지정된 벡터를 향하여 회전시켜줍니다.
    /// </summary>
    private void CharacterTurn()
    {
        transform.LookAt(transform.position + moveVector); // 나아가는 방향으로 캐릭터가 바라봅니다.
    }
    /// <summary>
    /// 캐릭터의 점프기능을 구현합니다.
    /// </summary>
    private void CharacterJump()
    {
        if (jumpKeyDown && !isJump) // 점프키를 눌렀고, 점프 상태가 False일때만 Jump 기능이 동작합니다.
        {
            rigidBody.AddForce(Vector3.up * 3, ForceMode.Impulse); // 즉발적인 Jump 기능을 구현합니다.
            isJump = true;
        }
    }
   
    /// <summary>
    /// Jump 상태가 아닐 때 무기를 교체하는 함수를 선언합니다.
    /// </summary>
    private void Swap()
    {
        int weaponIndex = -1;
        if (weaponTypeA) weaponIndex = 1;
        if (weaponTypeB) weaponIndex = 0;
        if ((weaponTypeA || weaponTypeB) && !isJump) //점프 상태가 아니면서, 무기의 Type이 A거나 B 일때 실행됩니다.
        {
            // 빈손일때는 비활성화합니다.
            if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
            equipWeapon = weapons[weaponIndex].GetComponent<Weapons>(); // Weapons 타입으로 형변환합니다.
            weapons[weaponIndex].SetActive(true);
        }

    }
    private void CharacterAttack()
    {
        if (equipWeapon == null) return;

        // 공격딜레이에 시간을 더해줍니다.
        atkDelay += Time.deltaTime;

        //공격가능 여부를 체크합니다.
        isAtkReady = equipWeapon.atkSpeed < atkDelay; 

        // 공격 조건은 'E' 키가 눌러졌을 때, 공격 가능 여부가 True일때, 그리고 점프 상태가 아닐때 공격조건이 성립합니다.
        if (attackKeyDown && isAtkReady && !isJump) 
        {
            // 공격을 합니다.
            equipWeapon.TypeCheck();
            atkDelay = 0;
        }
    }

    /// <summary>
    /// 근처 물체가 null 값이 아니면서, 점프 상태가 아니고, Q 키를 눌렀을 때 동작하는 함수를 선언합니다.
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
    /// Jump 착지 상태를 위한 충돌 감지 함수를 선언합니다.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BaseTile") isJump = false; // 캐릭터가 바닥에 닿았을 때 jump 상태를 False로 변경합니다.
    }

    /// <summary>
    /// 가까이 있는 무기를 감지합니다.
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
