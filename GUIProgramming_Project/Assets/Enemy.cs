/**********************************
 * ��ũ��Ʈ ��� : Dummy Enemy Controller
 * �ۼ��� : 2021-12-06
 * �ۼ��� : ������
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
    /// ĳ���Ͱ� ������ �ִ� ����� �浹 ������ �߻��� �� HP�� ���ҽ�ŵ�ϴ�.
    /// �������� �ʰ� ���⸸ ��� �Ǹ� �ߵ����� �ʽ��ϴ�. Defalut => (Active = False)  
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            
            Vector3 damagedAction = transform.position - other.transform.position;
            Weapons weapons = other.GetComponent<Weapons>();
            //���� ü�¿��� ������ Damage��ŭ ���ҽ�ŵ�ϴ�.
            curHP -= weapons.damage;

            // �ڷ�ƾ�� �����մϴ�.
            StartCoroutine(OnDamage(damagedAction));
        }
    }
    /// <summary>
    /// �������� �Ծ��� �� �����ϴ� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    /// <param name="damagedAction"></param>
    /// <returns></returns>
    IEnumerator OnDamage(Vector3 damagedAction)
    {
        material.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        // ���� ������ ü���� �����ִ� ����Դϴ�.
        if (curHP > 0)
        {
            material.color = Color.white;
        }
        // ü���� ���� ������ ���, ȸ�������� �����ϰ� �ش� ���͸� 2�� �� Destroy�մϴ�.
        // ü���� 0 ������ �� �ݴ� �������� �и��ϴ�.
        else
        {
            // �׾��� �� �ݴ� �������� �и��ϴ�.
            damagedAction = damagedAction.normalized;
            damagedAction += Vector3.up;
            rigidBody.AddForce(damagedAction * 1, ForceMode.Impulse);

            material.color = Color.gray;
            Destroy(gameObject, 2);
        }
    }
}
