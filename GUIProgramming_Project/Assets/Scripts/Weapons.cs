/**********************************
 * ��ũ��Ʈ ��� : Weapon Controller
 * �ۼ��� : 2021-12-06
 * �ۼ��� : ������
 * ********************************/
using System;
using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public enum WeaponType { Melee }
    public WeaponType weaponType;
    public int damage;
    public float atkSpeed;            // ���ݼӵ��� �����մϴ�.
    public BoxCollider meleeAtkRange; // ���� ������ �����մϴ�.
    public TrailRenderer trailEffect; // ���� ������ �����մϴ�.
    
    /// <summary>
    /// ����ϴ� ������ Type�� üũ�ϴ� �Լ��� �����մϴ�.
    /// TypeCheck()�� ���� ��ƾ�̸�, WeaponSwing()�̶�� �ڷ�ƾ �Լ��� �Բ� �ߵ��˴ϴ�.
    /// </summary>
    public void TypeCheck()
    {
        if (weaponType == WeaponType.Melee)
        {
            StopCoroutine("WeaponSwing");  // WeaponSwing Coroutine�� �����մϴ�.
            StartCoroutine("WeaponSwing"); // WeaponSwing Coroutine�� �����մϴ�.
        }
    }
    /// <summary>
    /// ������ �Լ� Ŭ������ �����մϴ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator WeaponSwing()
    {
        // yield�� ����Ͽ� �ð��� ������ �ۼ��մϴ�.
        yield return new WaitForSeconds(0.3f); // 0.3�� ����մϴ�.

        // Unity Engine���� ��Ȱ��ȭ�ߴ� ���� ��Ÿ��� ���� ����Ʈ ����� Ȱ��ȭ�մϴ�.
        meleeAtkRange.enabled = true;
        trailEffect.enabled = true;

        // 0.3�� ��� �Ŀ� ���� ��Ÿ��� ���� ����Ʈ ����� ��Ȱ��ȭ�մϴ�.
        yield return new WaitForSeconds(0.3f); // 0.3�� ����մϴ�.
        meleeAtkRange.enabled = false;

        yield return new WaitForSeconds(0.3f); // 0.3�� ����մϴ�.
        trailEffect.enabled = false;
        // Coroutine�� Ż���մϴ�.
        yield break;
    }
}
