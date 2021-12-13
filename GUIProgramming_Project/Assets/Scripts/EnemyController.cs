/**********************************
 * 스크립트 기능 : Enemy AI
 * 작성일 : 2021-12-12
 * 작성자 : 유찬영
 * ********************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour  
{
    /// <summary>
    /// 적 몬스터의 상태를 정의합니다. 
    /// idle   : 기본
    /// trace  : 추적
    /// attack : 공격
    /// dead   : 사망
    /// </summary>
    public enum EnemyState { idle, trace, attack, dead };
    public EnemyState state = EnemyState.idle;

    private Transform _transform;
    private Transform _playerTransform;
    private NavMeshAgent nav;
    private Animator _animator;
    /// <summary>
    /// traceDistance  : 추적 사정거리
    /// attackDistance : 공격 사정거리
    /// isDead         : 몬스터 사망여부
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

        // 코루틴을 시작합니다.
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
