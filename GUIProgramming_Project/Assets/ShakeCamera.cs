/**********************************
 * 스크립트 기능 : Camera Shake Controll
 * 작성일 : 2021-12-06
 * 작성자 : 유찬영
 * ********************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] float mForce = 0f;
    [SerializeField] Vector3 mOffset = Vector3.zero;

    Quaternion mOriginRot;
    public int waitingTime;

    void Start()
    {
        mOriginRot = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(ShakeCoroutine());
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {

            StopAllCoroutines();
            StartCoroutine(Reset());
        }
    }
    IEnumerator ShakeCoroutine()
    {
        
        Vector3 t_originEuler = transform.eulerAngles;
        while (true)
        {
            // x, y, z 축 무작위로 카메라를 흔들리게 제어합니다.

            float t_rotX = Random.Range(-mOffset.x, mOffset.x);
            float t_rotY = Random.Range(-mOffset.y, mOffset.y);
            float t_rotZ = Random.Range(-mOffset.z, mOffset.z);

            Vector3 t_randomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);

            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            while (Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, mForce * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }
    }
    IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation, mOriginRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, mOriginRot, mForce * Time.deltaTime);
            yield return null;
        }
    }
}
