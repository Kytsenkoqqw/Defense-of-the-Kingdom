using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;
using Zenject;

public class StateMachine : MonoBehaviour
{
    private ObjectState _currentState;
    private Transform _objectTransform;
    private Animator _animator;

    [SerializeField] private PolygonCollider2D _upAttackArea;
    [SerializeField] private PolygonCollider2D _frontAttackArea;
    [SerializeField] private PolygonCollider2D _downAttackArea;

    [Inject]
    private Transform[] _waypoints;

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
                StartCoroutine(OnOffAttackAreaCollider(_upAttackArea, 0.4f, 1f)); // Увеличенное время активации
                break;
            case "FrontAttack":
                StartCoroutine(OnOffAttackAreaCollider(_frontAttackArea, 0.4f, 1f));
                break;
            case "DownAttack":
                StartCoroutine(OnOffAttackAreaCollider(_downAttackArea, 0.4f, 1f));
                break;
        }
    }

    IEnumerator OnOffAttackAreaCollider(PolygonCollider2D attackArea, float delayBeforeActivation, float activeTime)
    {
        yield return new WaitForSeconds(delayBeforeActivation); // Ждем перед активацией
        attackArea.enabled = true;
        yield return new WaitForSeconds(activeTime); // Время, на которое активируется область атаки
        attackArea.enabled = false;
    }
}
