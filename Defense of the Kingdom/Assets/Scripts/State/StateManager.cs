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
         _stateManager = GetComponent<StateManager>();

         // Передайте ссылку на StateMachine
         ChangeState(new GuardIdleState(_guardTransform, _animator, _waypoints, _stateManager, _attackAreas));
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

         // Проверьте, что индекс не выходит за пределы массива, перед активацией коллайдера
         switch (attackType)
         {
            case "UpAttack":
               if (_attackAreas.Length > 0)
                  _attackAreas[0].enabled = true;
               else
                  Debug.LogError("UpAttack area is not defined!");
               break;
            case "FrontAttack":
               if (_attackAreas.Length > 1)
                  _attackAreas[1].enabled = true;
               else
                  Debug.LogError("FrontAttack area is not defined!");
               break;
            case "DownAttack":
               if (_attackAreas.Length > 2)
                  _attackAreas[2].enabled = true;
               else
                  Debug.LogError("DownAttack area is not defined!");
               break;
            default:
               Debug.LogError("Unknown attack type: " + attackType);
               break;
         }
      }

}

