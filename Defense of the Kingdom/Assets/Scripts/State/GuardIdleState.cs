using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.WSA;
using Zenject;

public class GuardIdleState : ObjectState
{
    private Transform[] _waypoints;
    private PolygonCollider2D[] _attackAreas;
    private float _moveSpeed = 2f;
    private Transform _objectTransform;
    private Animator _animator;
    private int _currentWaypointIndex = 0;
    private float _waitTime = 2f; // Время ожидания на точке
    private float _waitTimer = 0f;
    private bool _isWaiting = false; // Флаг ожидания
    private float _detectionRadius = 15f;
    private StateManager _stateManager; // Ссылка на StateMachine
    private Transform _torchTransform;
    

    public GuardIdleState(Transform transform, Animator animator, Transform[] waypoints, StateManager stateManager, PolygonCollider2D[] attackAreas, Transform torchTransform)
    {
        _objectTransform = transform;
        _animator = animator;
        _waypoints = waypoints;
        _stateManager = stateManager;
        _attackAreas = attackAreas;
        _torchTransform = torchTransform;

        if (_waypoints == null || _waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is null or empty!");
        }
    }

    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
        OffAttackAnimation();
        MoveToNextWaypoint();
        PlayRunAnimation(true); // Включаем анимацию бега
    }

    public override void UpdateState()
    {
        if (_isWaiting)
        {
            HandleWaiting();
        }
        else
        {
            MoveTowardsCurrentWaypoint();
            CheckAndHandleWaypointArrival();
            CheckForEnemies();
        }
    }

    public override void ExitState()
    {
        Debug.Log("exit IdleState");
    }

    private void MoveTowardsCurrentWaypoint()
    {
        if (_waypoints.Length == 0) return;

        Vector2 direction = (_waypoints[_currentWaypointIndex].position - _objectTransform.position).normalized;
        _objectTransform.Translate(direction * _moveSpeed * Time.deltaTime);

        // Поворот в сторону движения
        _objectTransform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    private void CheckAndHandleWaypointArrival()
    {
        if (_waypoints.Length == 0) return;

        if ((_waypoints[_currentWaypointIndex].position - _objectTransform.position).sqrMagnitude < 0.01f)
        {
            StartWaiting(); // Начинаем ожидание
        }
    }

    private void HandleWaiting()
    {
        _waitTimer -= Time.deltaTime;
        if (_waitTimer <= 0)
        {
            _isWaiting = false; // Завершаем ожидание
            MoveToNextWaypoint(); // Переходим к следующей точке
        }
    }

    private void MoveToNextWaypoint()
    {
        if (_waypoints.Length == 0) return;

        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
        PlayRunAnimation(true);
    }

    private void StartWaiting()
    {
        PlayRunAnimation(false);
        _isWaiting = true; // Устанавливаем флаг ожидания
        _waitTimer = _waitTime; // Сбрасываем таймер
    }

    private void PlayRunAnimation(bool play)
    {
        if (_animator != null)
        {
            _animator.SetBool("IsMoving", play);
        }
    }


    private void CheckForEnemies()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_objectTransform.position, _detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            EnemyStateManager enemy = collider.GetComponent<EnemyStateManager>();
            if (enemy != null)
            {
                Transform enemyTransform = collider.transform;
                float distanceToEnemy = Vector2.Distance(_objectTransform.position, enemyTransform.position);

                // Если враг в радиусе detectionRadius, переходим в состояние преследования
                if (distanceToEnemy > 1.5f && distanceToEnemy <= _detectionRadius)
                {
                    Debug.Log("Enemy detected. Switching to ChaseState.");
                    _stateManager.ChangeState(new GuardChaseState(_objectTransform, _animator, _waypoints, enemyTransform, _stateManager, _attackAreas));
                    return;
                }
            }
        }
    }
    
    /*private void MoveTowardsEnemy(Transform enemyTransform)
    {
        Vector2 direction = (enemyTransform.position - _objectTransform.position).normalized;
        _objectTransform.Translate(direction * _moveSpeed * Time.deltaTime);

        // Поворот в сторону движения к врагу
        _objectTransform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    
        PlayRunAnimation(true); // Включаем анимацию бега
    }*/
    

    private void OffAttackAnimation()
    {
        if ((_animator != null))
        {
            _animator.SetBool("UpAttack", false);
            _animator.SetBool("FrontAttack", false);
            _animator.SetBool("DownAttack",false);
        }
        else
        {
            Debug.Log("Animation is null");
        }
       
    }

}
