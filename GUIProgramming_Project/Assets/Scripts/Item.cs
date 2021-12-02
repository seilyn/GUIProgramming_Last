/**********************************
 * 스크립트 기능 : Item Controller
 * 작성일 : 2021-12-03
 * 작성자 : 유찬영
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
