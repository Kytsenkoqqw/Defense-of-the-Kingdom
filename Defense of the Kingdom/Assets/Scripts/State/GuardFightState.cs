using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GuardFightState : ObjectState
{
     private Transform _objectTransform;
    private Animator _animator;
    private Transform _enemyTransform;
    private Transform[] _waypoints; 
    private StateMachine _stateMachine;
    [SerializeField] private PolygonCollider2D _downAttackArea;
    [SerializeField] private PolygonCollider2D _upAttackArea;
    [SerializeField] private PolygonCollider2D _frontAttackArea;
    private float _attackDuration = 0.5f; // Длительность атаки в секундах

    public GuardFightState(Transform objectTransform, Animator animator, Transform enemyTransform, Transform[] waypoints, StateMachine stateMachine)
    {
        _objectTransform = objectTransform;
        _animator = animator;
        _enemyTransform = enemyTransform;
        _waypoints = waypoints;
        _stateMachine = stateMachine;
    }

    public override void Enter()
    {
        Debug.Log("Entering Fight State");
        /*_downAttackArea.enabled = false;
        _upAttackArea.enabled = false;
        _frontAttackArea.enabled = false;*/
    }

    public override void Update()
    {
        if (_enemyTransform == null)
        {
            Debug.LogWarning("Enemy transform is null, switching to Idle state.");
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            _animator.SetBool("IsFighting", false);
            return;
        }

        Vector2 direction = _enemyTransform.position - _objectTransform.position;

        if (direction.y > 0)
        {
            AttackUp();
        }
        else if (direction.y < 0)
        {
            AttackDown();
        }
        else
        {
            FrontAttack();
        }

        if (!IsEnemyInRange())
        {
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            _animator.SetBool("IsFighting", false);
        }
        else
        {
            GuardAttack();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Fight State");
        _animator.SetBool("IsFighting", false);
    }

    private void GuardAttack()
    {
        // Логика атаки
    }

    private bool IsEnemyInRange()
    {
        return Vector2.Distance(_objectTransform.position, _enemyTransform.position) <= 5f; 
    }

    private void AttackUp()
    {
        _animator.SetTrigger("AttackUp");
     //   StartCoroutine(OnOffAttackArea(_upAttackArea));
    }

    private void AttackDown()
    {
        _animator.SetTrigger("AttackDown");
      //  StartCoroutine(OnOffAttackArea(_downAttackArea));
    }

    private void FrontAttack()
    {
        _animator.SetTrigger("AttackFront");
      //  StartCoroutine(OnOffAttackArea(_frontAttackArea));
    }
    
    /*private IEnumerator OnOffAttackArea(PolygonCollider2D attackArea)
    {
        attackArea.enabled = true;
        yield return new WaitForSeconds(_attackDuration);
        attackArea.enabled = false;
    }*/

}
