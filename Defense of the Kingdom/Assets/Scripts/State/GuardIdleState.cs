using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float _detectionRadius = 5f;
    private MoveEnemyOnGuards _enemyComponent; // Измените имя, если необходимо
    private StateManager _stateManager; // Ссылка на StateMachine
    
    public GuardIdleState(Transform transform, Animator animator, Transform[] waypoints, StateManager stateManager)
    {
        _objectTransform = transform;
        _animator = animator;
        _waypoints = waypoints;
        _stateManager = stateManager;

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
        //PlayRunAnimation(true); // Включаем анимацию бега
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
        throw new NotImplementedException();
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
            if (collider.GetComponent<MoveEnemyOnGuards>() != null)
            {
                Debug.Log("Enemy detected, switching to FightState");
                Transform enemyTransform = collider.transform;
                _stateManager.ChangeState(new GuardFightState(_objectTransform, _animator, enemyTransform, _waypoints, _stateManager, _attackAreas));
                return; // Прекратить дальнейший поиск после нахождения первого врага
            }
        }
    }
    
    private void OffAttackAnimation()
    {
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack",false);
    }

}
