/**********************************
 * 스크립트 기능 : Character Controller
 * 작성일 : 2021-12-02
 * 작성자 : 유찬영
 * ********************************/

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 0f;
    [SerializeField] float maxZoom = 0f;
    [SerializeField] float minZoom = 0f;
    /// <summary>
    /// 카메라 위치를 잡아주는 함수를 선언합니다.
    /// 카메라 위치 += 카메라 정면 벡터 * 스피드 * 방향 
    /// </summary>
    
    public Transform cameraTarget;
    public Vector3 offset;
    /// <summary>
    /// 카메라가 Target(Player)을 따라다닙니다.
    /// </summary>
    void Update() => transform.position = cameraTarget.position + offset;
}
