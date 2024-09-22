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
      private BuyingGuard _buyingGuard;

      [Inject] private Transform[] _waypoints;
      [Inject(Id = "GuardAttackAreas")]
      private PolygonCollider2D[] _attackAreas;
      
      public virtual void Start()
      {
         _guardTransform = transform;
         _animator = GetComponent<Animator>();
         _stateManager = GetComponent<StateManager>();
         
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
      
      public void InjectDependencies(Transform[] waypoints, PolygonCollider2D[] attackAreas)
      {
         _waypoints = waypoints;
         _attackAreas = attackAreas;
      }
      
      public Animator GetAnimator()
      {
         return _animator;
      }
      
      public void SetAttackAreaActive(string attackType)
      {

         foreach (var area in _attackAreas)
         {
            area.enabled = false;
         }
         
         switch (attackType)
         {
            case "UpAttack":
               StartCoroutine(OnOffAttackArea(_attackAreas[0], 0.40f, 0.3f)); // Передаем индекс 0 для верхней атаки
               break;
            case "FrontAttack":
               StartCoroutine(OnOffAttackArea(_attackAreas[1], 0.40f, 0.3f)); // Передаем индекс 1 для фронтальной атаки
               break;
            case "DownAttack":
               StartCoroutine(OnOffAttackArea(_attackAreas[2], 0.40f, 0.3f)); // Передаем индекс 2 для нижней атаки
               break;
         }
      }

      private IEnumerator OnOffAttackArea(PolygonCollider2D attackArea, float delayBeforeActivation, float activeTime)
      {
         yield return new WaitForSeconds(delayBeforeActivation);
         attackArea.enabled = true;
         yield return new WaitForSeconds(activeTime);
         attackArea.enabled = false;
      }

}