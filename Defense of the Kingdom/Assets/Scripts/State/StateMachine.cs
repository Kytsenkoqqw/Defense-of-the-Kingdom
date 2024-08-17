using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class StateMachine : MonoBehaviour
{
    private ObjectState _currentState;
    private Transform _objectTransform;
    private Animator _animator;
    private LayerMask _enemyLayer;

    [SerializeField] private Transform[] _waypoints;

    private void Start()
    {
        _objectTransform = transform; 
        _animator = GetComponent<Animator>();
        // Передайте ссылку на StateMachine
        ChangeState(new IdleState(_objectTransform, _animator, _waypoints, this));
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(ObjectState newState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = newState;
        _currentState.Enter();
    }
}
