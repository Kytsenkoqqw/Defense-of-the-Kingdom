using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace State
{
    public class EnemyStateManager : MonoBehaviour
    {

        private Transform _enemyTransform;
        private Animator _animator;
        private ObjectState _currentState;
        private EnemyStateManager _enemyStateManager;
        [Inject(Id = "EnemyAttackAreas")] private PolygonCollider2D[] _enemyAttackAreas;
        
        private void Start()
        {
            _enemyTransform = transform;
            _animator = GetComponent<Animator>();
            _enemyStateManager = GetComponent<EnemyStateManager>();
            
            ChangeState(new EnemyIdleState(_enemyTransform, _animator, _enemyStateManager, _enemyAttackAreas));
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
                case "UpAttack":
                    StartCoroutine(OnOffAttackArea(_enemyAttackAreas[0], 0.40f, 0.3f)); // Передаем индекс 0 для верхней атаки
                    break;
                case "FrontAttack":
                    StartCoroutine(OnOffAttackArea(_enemyAttackAreas[1], 0.40f, 0.3f)); // Передаем индекс 1 для фронтальной атаки
                    break;
                case "DownAttack":
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