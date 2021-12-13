/**********************************
 * 스크립트 기능 : Character Zoom In & Zoom Out
 * 작성일 : 2021-12-11
 * 작성자 : 유찬영
 * ********************************/

using UnityEngine;

public class CharacterZoom : MonoBehaviour
{

    public float rotateSpeed = 10.0f;
    public float zoomSpeed = 10.0f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Zoom();
    }
    /// <summary>
    /// 줌인 줌아웃 함수를 선언합니다.
    /// </summary>
    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 1.5)
        {
            mainCamera.fieldOfView += distance;
        }
    }
}
