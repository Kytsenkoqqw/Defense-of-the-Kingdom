using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GuardIdleState : ObjectState
{
    [Inject] 
    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private float _moveSpeed = 3f;
    private Transform _guardTransform;
    private Animator _animator;
    private int _currentWaypointIndex = 0;
    private float _waitTime = 2f;
    private float _waitTimer = 0f;
    private bool _isWaiting = false;
    private StateManager _stateManager;
    

    public GuardIdleState(Transform transform, Animator animator, Transform[] waypoints, StateManager stateManager)
    {
        _guardTransform = transform;
        _animator = animator;
        _waypoints = waypoints;
        _stateManager = stateManager;

        if (_waypoints == null || _waypoints.Length == 0)
        {
            Debug.LogError("Waypoints array is null or empty!");
        }
    }
    
    private void Start()
    {
        _stateManager = GetComponent<StateManager>();

        GuardIdleState guardIdleState = new GuardIdleState(_guardTransform, _animator, _waypoints, _stateManager);

        // Устанавливаем начальное состояние как Idle
        _stateManager.ChangeState(guardIdleState);
    }

    public override void EnterState()
    {
        Debug.Log("Enter GuardIdle");
    }

    public override void ExecuteState()
    {
        if (_isWaiting)
        {
            HandleWaiting();
        }
        else
        {
            MoveTowardsCurrentWaypoint();
            CheckAndHandleWaypointArrival();
            //CheckForEnemies();
        }
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
    
    private void MoveTowardsCurrentWaypoint()
    {
        if (_waypoints.Length == 0) return;

        Vector2 direction = (_waypoints[_currentWaypointIndex].position - _guardTransform.position).normalized;
        _guardTransform.Translate(direction * _moveSpeed * Time.deltaTime);

        // Поворот в сторону движения
        _guardTransform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    private void CheckAndHandleWaypointArrival()
    {
        if (_waypoints.Length == 0) return;

        if ((_waypoints[_currentWaypointIndex].position - _guardTransform.position).sqrMagnitude < 0.01f)
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

}
