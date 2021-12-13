/**********************************
 * ��ũ��Ʈ ��� : Enemy AI
 * �ۼ��� : 2021-12-12
 * �ۼ��� : ������
 * ********************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour  
{
    /// <summary>
    /// �� ������ ���¸� �����մϴ�. 
    /// idle   : �⺻
    /// trace  : ����
    /// attack : ����
    /// dead   : ���
    /// </summary>
    public enum EnemyState { idle, trace, attack, dead };
    public EnemyState state = EnemyState.idle;

    private Transform _transform;
    private Transform _playerTransform;
    private NavMeshAgent nav;
    private Animator _animator;
    /// <summary>
    /// traceDistance  : ���� �����Ÿ�
    /// attackDistance : ���� �����Ÿ�
    /// isDead         : ���� �������
    /// </summary>
    public float traceDistance = 15.0f;
    public float attackDistance = 3.0f;
    public bool isdead = false;

    void Start()
    {
        _transform =gameObject.GetComponent<Transform>();
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nav = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();   

        // �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }

    IEnumerator CheckState()
    {
        while (!isdead)
        {
            yield return new WaitForSeconds(0.2f);
            float distance = Vector3.Distance(_playerTransform.position, _transform.position);
            if (distance <= attackDistance) state = EnemyState.attack;
            else if (distance <= traceDistance) state = EnemyState.trace;
            else state = EnemyState.idle;
        }
    }

    [Obsolete]
    IEnumerator CheckStateForAction()
    {
        while (!isdead)
        {
            switch (state)
            {
                case EnemyState.idle:
                    nav.Stop();
                    _animator.SetBool("isTrace", false);   
                    break;
                case EnemyState.trace:
                    nav.destination = _playerTransform.position;
                    nav.Resume();
                    _animator.SetBool("isTrace", true);
                    break;
                case EnemyState.attack:
                    break;
            }
            yield return null;
        }
    }   

    void Update()
    {
        
    }
}
