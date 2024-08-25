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
    }

    public override void Update()
    {
        if (_enemyTransform == null)
        {
            Debug.LogWarning("Enemy transform is null, switching to Idle state.");
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            return;
        }

        Vector2 direction = _enemyTransform.position - _objectTransform.position;

        // Определяем направление движения (слева или справа)
        float horizontalDirection = direction.x;

        // Поворачиваем стражника в сторону врага
        if (horizontalDirection > 0)
        {
            // Враг справа, стражник смотрит вправо
            _objectTransform.localScale = new Vector3(Mathf.Abs(_objectTransform.localScale.x), _objectTransform.localScale.y, _objectTransform.localScale.z);
        }
        else if (horizontalDirection < 0)
        {
            // Враг слева, стражник смотрит влево
            _objectTransform.localScale = new Vector3(-Mathf.Abs(_objectTransform.localScale.x), _objectTransform.localScale.y, _objectTransform.localScale.z);
        }

        // Выбор типа атаки на основе вертикальной позиции врага
        if (Mathf.Abs(direction.y) < 0.1f) // Если враг находится на одном уровне по высоте (с допуском)
        {
            FrontAttack(); // Боковая атака
        }
        else if (direction.y > 0)
        {
            AttackUp(); // Атака вверх
        }
        else
        {
            AttackDown(); // Атака вниз
        }

        // Проверяем, находится ли враг в радиусе атаки
        if (!IsEnemyInRange())
        {
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            GuardAttack();
        }
    }

    public override void Exit()
    {
        _animator.ResetTrigger("FrontAttack");
        _animator.ResetTrigger("DownAttack");
        _animator.ResetTrigger("UpAttack");
        Debug.Log("Exiting Fight State");
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
        _animator.SetTrigger("UpAttack");
    }

    private void AttackDown()
    {
        _animator.SetTrigger("DownAttack");
    }

    private void FrontAttack()
    {
        _animator.SetTrigger("FrontAttack");
    }

}
