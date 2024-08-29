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
    
    [SerializeField] private PolygonCollider2D _upAttackArea;
    [SerializeField] private PolygonCollider2D _frontAttackArea;
    [SerializeField] private PolygonCollider2D _downAttackArea;

    [SerializeField] private Transform[] _waypoints;

    private void Start()
    {
        _objectTransform = transform; 
        _animator = GetComponent<Animator>();
        // Передайте ссылку на StateMachine
        ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, this));
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
    
    public void SetAttackAreaActive(string attackType)
    {
        // Деактивируем все коллайдеры
        _upAttackArea.enabled = false;
        _frontAttackArea.enabled = false;
        _downAttackArea.enabled = false;

        // Активируем нужный коллайдер
        switch (attackType)
        {
            case "UpAttack":
                _upAttackArea.enabled = true;
                break;
            case "FrontAttack":
                _frontAttackArea.enabled = true;
                break;
            case "DownAttack":
                _downAttackArea.enabled = true;
                break;
        }
    }
    
    public void StartStateCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
