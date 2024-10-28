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
        private DeathGuard _deathGuard;
        private Transform _towerTransform;
    

        public EnemyFightState(Transform torchTransform,Transform towerTransform, Animator animator, Transform guardTransform, EnemyStateManager enemyStateManager, PolygonCollider2D[] enemyAttackAreas)
        {
            _torchTransform = torchTransform;
            _animator = animator;
            _guardTransform = guardTransform;
            _enemyStateManager = enemyStateManager;
            _enemyAttackAreas = enemyAttackAreas;
            _towerTransform = towerTransform;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Enemy Fight State");
        }

        public override void UpdateState()
        {
            if (_guardTransform == null)
            {
                OffAttackAnimation();
                Debug.LogWarning("Enemy transform is null, switching to Idle state.");
                _enemyStateManager.ChangeState(new EnemyBreakBuildingState(_torchTransform, _guardTransform, _animator, _towerTransform, _enemyStateManager, _enemyAttackAreas));
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
                _enemyStateManager.ChangeState(new EnemyIdleState(_torchTransform, _towerTransform,_deathGuard, _animator, _enemyStateManager, _enemyAttackAreas));
                OffAttackAnimation();
                _animator.SetBool("IsMoving", true);
            }
        }

        public override void ExitState()
        {
            Debug.Log("exit enemy fight state");
        }
        
        private void OffAttackAnimation()
        {
            _animator.SetBool("EnemyUpAttack", false);
            _animator.SetBool("EnemyFrontAttack", false);
            _animator.SetBool("EnemyDownAttack", false);
        }

        private bool IsEnemyInRange()
        {
            return Vector2.Distance(_torchTransform.position, _guardTransform.position) <= 1.7f;
        }

        private void AttackUp()
        {
            _animator.SetBool("EnemyUpAttack", true);
            _animator.SetBool("EnemyFrontAttack", false);
            _animator.SetBool("EnemyDownAttack", false);
            _enemyStateManager.SetAttackAreaActive("EnemyUpAttack");
        }

        private void FrontAttack()
        {
            _animator.SetBool("EnemyFrontAttack", true);
            _animator.SetBool("EnemyDownAttack", false);
            _animator.SetBool("EnemyUpAttack", false);
            _enemyStateManager.SetAttackAreaActive("EnemyFrontAttack");
        }

        private void AttackDown()
        {
            _animator.SetBool("EnemyDownAttack", true);
            _animator.SetBool("EnemyFrontAttack", false);
            _animator.SetBool("EnemyUpAttack", false);
            _enemyStateManager.SetAttackAreaActive("EnemyDownAttack");
        }
    }
}