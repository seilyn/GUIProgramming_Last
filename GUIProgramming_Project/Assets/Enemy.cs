/**********************************
 * 스크립트 기능 : Dummy Enemy Controller
 * 작성일 : 2021-12-06
 * 작성자 : 유찬영
 * ********************************/

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP;
    public int curHP;

    Rigidbody rigidBody;
    BoxCollider boxCollider;
    Material material;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
    }
    /// <summary>
    /// 캐릭터가 가지고 있는 무기와 충돌 판정이 발생할 시 HP를 감소시킵니다.
    /// 공격하지 않고 무기만 닿게 되면 발동하지 않습니다. Defalut => (Active = False)  
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            
            Vector3 damagedAction = transform.position - other.transform.position;
            Weapons weapons = other.GetComponent<Weapons>();
            //현재 체력에서 무기의 Damage만큼 감소시킵니다.
            curHP -= weapons.damage;

            // 코루틴을 실행합니다.
            StartCoroutine(OnDamage(damagedAction));
        }
    }
    /// <summary>
    /// 데미지를 입었을 때 실행하는 코루틴을 선언합니다.
    /// </summary>
    /// <param name="damagedAction"></param>
    /// <returns></returns>
    IEnumerator OnDamage(Vector3 damagedAction)
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // 현재 몬스터의 체력이 남아있는 경우입니다.
        if (curHP > 0)
        {
            material.color = Color.white;
        }
        // 체력이 전부 소진된 경우, 회색빛으로 변경하고 해당 몬스터를 2초 후 Destroy합니다.
        // 체력이 0 이하일 때 반대 방향으로 밀립니다.
        else
        {
            // 죽었을 때 반대 방향으로 밀립니다.
            damagedAction = damagedAction.normalized;
            damagedAction += Vector3.up;
            rigidBody.AddForce(damagedAction * 1, ForceMode.Impulse);

            material.color = Color.gray;
            Destroy(gameObject, 2);
        }
    }
}
