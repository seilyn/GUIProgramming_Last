/**********************************
 * ��ũ��Ʈ ��� : Item Controller
 * �ۼ��� : 2021-12-03
 * �ۼ��� : ������
 * ********************************/
using UnityEngine;

public class Item : MonoBehaviour
{
    public Type type;
    public int value;
    public enum Type
    {
        Weapon,
        Life
    };

    void Update()
    {
        transform.Rotate(Vector3.down * 10 * Time.deltaTime);
    }
}
