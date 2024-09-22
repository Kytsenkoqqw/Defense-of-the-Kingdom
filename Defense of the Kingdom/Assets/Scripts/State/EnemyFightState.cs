using System;
using UnityEngine;

namespace State
{
    public class EnemyFightState : ObjectState
    {
        private Transform _torchTransform;
        private Animator _animator;
        private Transform _guardTransform;
        private PolygonCollider2D[] _enemyAttackAreas;
        private EnemyStateManager _enemyStateManager;
    

        public EnemyFightState(Transform torchTransform, Animator animator, Transform guardTransform, EnemyStateManager enemyStateManager, PolygonCollider2D[] enemyAttackAreas)
        {
            _torchTransform = torchTransform;
            _animator = animator;
            _guardTransform = guardTransform;
            _enemyStateManager = enemyStateManager;
            _enemyAttackAreas = enemyAttackAreas;
        }
        
        public override void EnterState()
        {
            Debug.Log("Enter Enemy Fight State");
        }

        public override void UpdateState()
        {
            if (_guardTransform == null)
            {
                Debug.LogWarning("Enemy transform is null, switching to Idle state.");
                _enemyStateManager.ChangeState(new EnemyIdleState(_torchTransform, _animator,  _enemyStateManager, _enemyAttackAreas));
                return;
            }

            Vector2 direction = _guardTransform.position - _torchTransform.position;

            // Определяем направление движения (слева или справа)
            float horizontalDirection = direction.x;

            // Поворачиваем стражника в сторону врага
            if (horizontalDirection > 0)
            {
                _torchTransform.localScale = new Vector3(Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
            }
            else if (horizontalDirection < 0)
            {
                _torchTransform.localScale = new Vector3(-Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
            }

            // Выбор типа атаки на основе вертикальной позиции врага
            if (Mathf.Abs(direction.y) < 0.3f)
            {
                FrontAttack();
            }
            else if (direction.y > 0)
            {
                AttackUp();
            }
            else
            {
                AttackDown();
            }

            // Проверяем, находится ли враг в радиусе атаки
            if (!IsEnemyInRange())
            {
                _enemyStateManager.ChangeState(new EnemyIdleState(_guardTransform, _animator, _enemyStateManager, _enemyAttackAreas));
                OffAttackAnimation();
                _animator.SetBool("IsMoving", true);
            }
        }

        public override void ExitState()
        {
            throw new NotImplementedException();
        }
        
        private void OffAttackAnimation()
        {
            _animator.SetBool("UpAttack", false);
            _animator.SetBool("FrontAttack", false);
            _animator.SetBool("DownAttack", false);
        }

        private bool IsEnemyInRange()
        {
            return Vector2.Distance(_torchTransform.position, _guardTransform.position) <= 5f;
        }

        private void AttackUp()
        {
            _animator.SetBool("UpAttack", true);
            _animator.SetBool("FrontAttack", false);
            _animator.SetBool("DownAttack", false);
            _enemyStateManager.SetAttackAreaActive("UpAttack");
        }

        private void FrontAttack()
        {
            _animator.SetBool("FrontAttack", true);
            _animator.SetBool("DownAttack", false);
            _animator.SetBool("UpAttack", false);
            _enemyStateManager.SetAttackAreaActive("FrontAttack");
        }

        private void AttackDown()
        {
            _animator.SetBool("DownAttack", true);
            _animator.SetBool("FrontAttack", false);
            _animator.SetBool("UpAttack", false);
            _enemyStateManager.SetAttackAreaActive("DownAttack");
        }
    }
}