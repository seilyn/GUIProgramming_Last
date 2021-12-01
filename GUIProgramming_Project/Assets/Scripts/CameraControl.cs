/**********************************
 * ��ũ��Ʈ ��� : Character Controller
 * �ۼ��� : 2021-12-02
 * �ۼ��� : ������
 * ********************************/

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform cameraTarget;
    public Vector3 offset;
    /// <summary>
    /// ī�޶� Target(Player)�� ����ٴմϴ�.
    /// </summary>
    void Update() => transform.position = cameraTarget.position + offset;
}
