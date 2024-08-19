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
    private Transform[] _waypoints; // Добавляем поле для waypoints
    private StateMachine _stateMachine; // Добавляем поле для stateMachine
    private HealthSystem _healthSystem;

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
        _animator.SetBool("IsFighting", true);
    }

    public override void Update()
    {
        if (_enemyTransform == null || !IsEnemyInRange())
        {
            // Если враг исчезает или выходит за пределы радиуса, переключаемся обратно в состояние покоя
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
        // Проверяем, находится ли враг в радиусе обнаружения
        return Vector2.Distance(_objectTransform.position, _enemyTransform.position) <= 5f; // Замените 5f на ваш радиус
    }
    
}
