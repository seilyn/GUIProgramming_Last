/**********************************
 * ��ũ��Ʈ ��� : Character Controller
 * �ۼ��� : 2021-12-02
 * �ۼ��� : ������
 * ********************************/

using UnityEngine;

public class Controller : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;
    public int characterSpeed; // Inspector â���� ������ �����ϵ��� public���� ����
    public float characterSubSpeed;
    Vector3 moveVector;
    Animator animator;

    bool walkKeyDown;
    
    /// <summary>
    /// MonoBehavior Method
    /// </summary>
    void Start()
    {
        animator = GetComponentInChildren<Animator>();     
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
    }
    /// <summary>
    /// Ű�� �޾ƿɴϴ�.
    /// </summary>
    private void GetInput()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        walkKeyDown = Input.GetButton("Walk");
    }
    /// <summary>
    /// ĳ������ �̵��� �����մϴ�.
    /// </summary>
    private void CharacterMove()
    {
        moveVector = new Vector3(horizontalAxis, 0, verticalAxis).normalized; // ���Ⱚ�� 1�� �����ϱ� ���� normalized�� ����մϴ�.
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
    }
    /// <summary>
    /// ĳ������ ȸ���� �����մϴ�.
    /// LookAt : ������ ���͸� ���Ͽ� ȸ�������ݴϴ�.
    /// </summary>
    private void CharacterTurn()
    {
        transform.LookAt(transform.position + moveVector); // ���ư��� �������� ĳ���Ͱ� �ٶ󺾴ϴ�.
    }
}
