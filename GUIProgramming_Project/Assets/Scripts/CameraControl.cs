/**********************************
 * ��ũ��Ʈ ��� : Character Controller
 * �ۼ��� : 2021-12-02
 * �ۼ��� : ������
 * ********************************/

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 0f;
    [SerializeField] float maxZoom = 0f;
    [SerializeField] float minZoom = 0f;
    /// <summary>
    /// ī�޶� ��ġ�� ����ִ� �Լ��� �����մϴ�.
    /// ī�޶� ��ġ += ī�޶� ���� ���� * ���ǵ� * ���� 
    /// </summary>
    
    public Transform cameraTarget;
    public Vector3 offset;
    /// <summary>
    /// ī�޶� Target(Player)�� ����ٴմϴ�.
    /// </summary>
    void Update() => transform.position = cameraTarget.position + offset;
}
