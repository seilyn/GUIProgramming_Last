/**********************************
 * ��ũ��Ʈ ��� : Character Zoom In & Zoom Out
 * �ۼ��� : 2021-12-11
 * �ۼ��� : ������
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
    /// ���� �ܾƿ� �Լ��� �����մϴ�.
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
