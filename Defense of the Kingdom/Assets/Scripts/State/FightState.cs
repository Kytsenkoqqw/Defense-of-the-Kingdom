using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightState : ObjectState
{
     protected Transform _objectTransform;
    protected Animator _animator;
    protected Transform _enemyTransform;
    protected Transform[] _waypoints;
    protected StateMachine _stateMachine;

    public FightState(Transform objectTransform, Animator animator, Transform enemyTransform,
        Transform[] waypoints, StateMachine stateMachine)
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
    }

    public override void Update()
    {
        if (_enemyTransform == null)
        {
            Debug.LogWarning("Enemy transform is null, switching to Idle state.");
            _stateMachine.ChangeState(CreateIdleState());
            return;
        }

        Vector2 direction = _enemyTransform.position - _objectTransform.position;

        float horizontalDirection = direction.x;

        // Поворачиваем персонажа в сторону врага
        if (horizontalDirection > 0)
        {
            _objectTransform.localScale = new Vector3(Mathf.Abs(_objectTransform.localScale.x),
                _objectTransform.localScale.y, _objectTransform.localScale.z);
        }
        else if (horizontalDirection < 0)
        {
            _objectTransform.localScale = new Vector3(-Mathf.Abs(_objectTransform.localScale.x),
                _objectTransform.localScale.y, _objectTransform.localScale.z);
        }

        // Логика выбора атаки
        if (Mathf.Abs(direction.y) < 0.3f)
        {
            FrontAttack();
        }
        else if (direction.y > 0)
        {
            AttackUp();
        }
        else
        {
            AttackDown();
        }

        // Проверяем, находится ли враг в радиусе атаки
        if (!IsEnemyInRange())
        {
            _stateMachine.ChangeState(CreateIdleState());
            OffAttackAnimation();
            _animator.SetBool("IsMoving", true);
        }
    }

    protected abstract ObjectState CreateIdleState();

    public override void Exit()
    {
        Debug.Log("Exiting Fight State");
    }

    protected virtual void OffAttackAnimation()
    {
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack", false);
    }

    protected bool IsEnemyInRange()
    {
        return Vector2.Distance(_objectTransform.position, _enemyTransform.position) <= 5f;
    }

    protected void AttackUp()
    {
        _animator.SetBool("UpAttack", true);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack",false);
        ActivateAttackArea("UpAttack");
    }

    protected void FrontAttack()
    {
        _animator.SetBool("FrontAttack", true);
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("DownAttack",false);
        ActivateAttackArea("FrontAttack");
    }

    protected void AttackDown()
    {
        _animator.SetBool("DownAttack", true);
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("FrontAttack",false);
        ActivateAttackArea("DownAttack");
    }

    protected abstract void ActivateAttackArea(string attackType);
}
