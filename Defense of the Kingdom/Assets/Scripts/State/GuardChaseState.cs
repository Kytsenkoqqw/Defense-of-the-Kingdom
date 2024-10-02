using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;

namespace State
{
    public class GuardChaseState : ObjectState
    {
        private Transform _objectTransform;
        private Transform _enemyTransform;
        private Animator _animator;
        private StateManager _stateManager;
        private float _moveSpeed = 3f; // Можно настроить скорость преследования
        private float _attackRadius = 1.2f; // Радиус атаки
        private Transform[] _waypoints;
        private PolygonCollider2D[] _attackAreas;
        private Transform _torchTransform;

        public GuardChaseState(Transform objectTransform, Animator animator, Transform enemyTransform,
            StateManager stateManager)
        {
            _objectTransform = objectTransform;
            _animator = animator;
            _enemyTransform = enemyTransform;
            _stateManager = stateManager;
        }

        public override void EnterState()
        {
            Debug.Log("Entering Chase State");
            PlayRunAnimation(true);
        }

        public override void UpdateState()
        {
            if (_enemyTransform == null) return;

            float distanceToEnemy = Vector2.Distance(_objectTransform.position, _enemyTransform.position);

            if (distanceToEnemy <= _attackRadius)
            {
                Debug.Log("Enemy in attack range. Switching to FightState.");
                _stateManager.ChangeState(new GuardFightState(_objectTransform, _animator, _enemyTransform, null,
                    _stateManager, null));
            }
            else
            {
                MoveTowardsEnemy();
            }

        }

        public override void ExitState()
        {
            Debug.Log("Exiting Chase State");
        }

        private void MoveTowardsEnemy()
        {
            Vector2 direction = (_enemyTransform.position - _objectTransform.position).normalized;
            _objectTransform.Translate(direction * _moveSpeed * Time.deltaTime);

            // Поворот в сторону движения
            _objectTransform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }

        private void PlayRunAnimation(bool play)
        {
            if (_animator != null)
            {
                _animator.SetBool("IsMoving", play);
            }
        }

    }

}