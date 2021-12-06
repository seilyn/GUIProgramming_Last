/**********************************
 * 스크립트 기능 : Weapon Controller
 * 작성일 : 2021-12-06
 * 작성자 : 유찬영
 * ********************************/
using System;
using System.Collections;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public enum WeaponType { Melee }
    public WeaponType weaponType;
    public int damage;
    public float atkSpeed;            // 공격속도를 정의합니다.
    public BoxCollider meleeAtkRange; // 공격 범위를 정의합니다.
    public TrailRenderer trailEffect; // 공격 궤적을 정의합니다.
    
    /// <summary>
    /// 사용하는 무기의 Type을 체크하는 함수를 선언합니다.
    /// TypeCheck()은 메인 루틴이며, WeaponSwing()이라는 코루틴 함수가 함께 발동됩니다.
    /// </summary>
    public void TypeCheck()
    {
        if (weaponType == WeaponType.Melee)
        {
            StopCoroutine("WeaponSwing");  // WeaponSwing Coroutine을 정지합니다.
            StartCoroutine("WeaponSwing"); // WeaponSwing Coroutine을 실행합니다.
        }
    }
    /// <summary>
    /// 열거형 함수 클래스를 선언합니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator WeaponSwing()
    {
        // yield를 사용하여 시간차 로직을 작성합니다.
        yield return new WaitForSeconds(0.3f); // 0.3초 대기합니다.

        // Unity Engine에서 비활성화했던 공격 사거리와 공격 이펙트 기능을 활성화합니다.
        meleeAtkRange.enabled = true;
        trailEffect.enabled = true;

        // 0.3초 대기 후에 공격 사거리와 공격 이펙트 기능을 비활성화합니다.
        yield return new WaitForSeconds(0.3f); // 0.3초 대기합니다.
        meleeAtkRange.enabled = false;

        yield return new WaitForSeconds(0.3f); // 0.3초 대기합니다.
        trailEffect.enabled = false;
        // Coroutine을 탈출합니다.
        yield break;
    }
}
