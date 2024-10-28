using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace State
{
    public class EnemyStateManager : MonoBehaviour
    {

        private Transform _torchTransform;
        private Animator _animator;
        private ObjectState _currentState;
        private EnemyStateManager _enemyStateManager;
        private Transform _guardTransform;
        private Transform _towerTransform;
        private DeathGuard _deathGuard;
        [Inject(Id = "EnemyAttackAreas")] private PolygonCollider2D[] _enemyAttackAreas;
        

        private void Start()
        {
            _torchTransform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _enemyStateManager = GetComponent<EnemyStateManager>();
            
            _deathGuard = FindObjectOfType<DeathGuard>();
            if (_deathGuard != null)
            {
                _guardTransform = _deathGuard.transform; 
            }
            
            ChangeState(new EnemyIdleState(_torchTransform, _towerTransform, _deathGuard, _animator, _enemyStateManager, _enemyAttackAreas));
        }
        
        public void Initialize(PolygonCollider2D[] enemyAttackAreas)
        {
            _enemyAttackAreas = enemyAttackAreas;
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

            foreach (var area in _enemyAttackAreas)
            {
                area.enabled = false;
            }
         
            switch (attackType)
            {
                case "EnemyUpAttack":
                    StartCoroutine(OnOffAttackArea(_enemyAttackAreas[0], 0.40f, 0.3f)); // Передаем индекс 0 для верхней атаки
                    break;
                case "EnemyFrontAttack":
                    StartCoroutine(OnOffAttackArea(_enemyAttackAreas[1], 0.40f, 0.3f)); // Передаем индекс 1 для фронтальной атаки
                    break;
                case "EnemyDownAttack":
                    StartCoroutine(OnOffAttackArea(_enemyAttackAreas[2], 0.40f, 0.3f)); // Передаем индекс 2 для нижней атаки
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
}