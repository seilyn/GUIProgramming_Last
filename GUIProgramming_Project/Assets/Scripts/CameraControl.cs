/**********************************
 * 스크립트 기능 : Character Controller
 * 작성일 : 2021-12-02
 * 작성자 : 유찬영
 * ********************************/

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 offset;
    /// <summary>
    /// 카메라가 Target(Player)을 따라다닙니다.
    /// </summary>
    void Update() => transform.position = cameraTarget.position + offset;
}
