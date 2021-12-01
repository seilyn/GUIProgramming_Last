/**********************************
 * 스크립트 기능 : Character Controller
 * 작성일 : 2021-12-02
 * 작성자 : 유찬영
 * ********************************/

using UnityEngine;

public class Controller : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;
    public int characterSpeed; // Inspector 창에서 수정이 용이하도록 public으로 선언
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
    /// 매 프레임마다 Update 함수를 호출합니다.
    /// </summary>
    void Update()
    {
        GetInput();
        CharacterMove();
        CharacterTurn();
        SetCharacterAnimation();
    }
    /// <summary>
    /// 키를 받아옵니다.
    /// </summary>
    private void GetInput()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        walkKeyDown = Input.GetButton("Walk");
    }
    /// <summary>
    /// 캐릭터의 이동을 구현합니다.
    /// </summary>
    private void CharacterMove()
    {
        moveVector = new Vector3(horizontalAxis, 0, verticalAxis).normalized; // 방향값을 1로 보정하기 위해 normalized를 사용합니다.
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
    }
    /// <summary>
    /// 캐릭터의 회전을 구현합니다.
    /// LookAt : 지정된 벡터를 향하여 회전시켜줍니다.
    /// </summary>
    private void CharacterTurn()
    {
        transform.LookAt(transform.position + moveVector); // 나아가는 방향으로 캐릭터가 바라봅니다.
    }
}
