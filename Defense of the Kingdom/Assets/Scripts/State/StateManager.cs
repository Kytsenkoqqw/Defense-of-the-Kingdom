using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = System.Object;

public class StateManager : MonoBehaviour
{
   
      private ObjectState _currentState;
      private Transform _guardTransform;
      private Animator _animator;
      private StateManager _stateManager;

      [Inject] private Transform[] _waypoints;
      [Inject] private PolygonCollider2D[] _attackAreas;

      private void Start()
      {
         _guardTransform = transform;
         _animator = GetComponent<Animator>();

         // Передайте ссылку на StateMachine
         ChangeState(new GuardIdleState(_guardTransform, _animator, _waypoints, _stateManager));
      }

      private void Update()
      {
         _currentState.UpdateState();
      }

      public void ChangeState(ObjectState newState)
      {
         if (_currentState != null)
         {
            _currentState.ExitState();
         }

         _currentState = newState;
         _currentState.EnterState();
      }
      
      public void SetAttackAreaActive(string attackType)
      {
         // Деактивируйте все коллайдеры
         foreach (var area in _attackAreas)
         {
            area.enabled = false;
         }

         // Активируйте нужный коллайдер
         switch (attackType)
         {
            case "UpAttack":
               _attackAreas[0].enabled = true; // Замените индекс на нужный
               break;
            case "FrontAttack":
               _attackAreas[1].enabled = true; // Замените индекс на нужный
               break;
            case "DownAttack":
               _attackAreas[2].enabled = true; // Замените индекс на нужный
               break;
         }
      }

}

