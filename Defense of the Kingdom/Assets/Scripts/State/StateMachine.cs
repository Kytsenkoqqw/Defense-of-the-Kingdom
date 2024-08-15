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

    [SerializeField] private Transform[] _waypoints; // Задай пути в инспекторе

    private void Start()
    {
        _objectTransform = transform; // Получаем Transform текущего объекта
        _animator = GetComponent<Animator>();
        ChangeState(new IdleState(_objectTransform, _animator, _waypoints));
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
