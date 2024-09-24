using System;
using UnityEngine;

namespace State
{
    public class EnemyIdleState : ObjectState
    {

        private Animator _animator;
        private Transform _torchTransform;
        private Transform _guardTransform;
        private EnemyStateManager _enemyStateManager;
        private PolygonCollider2D[] _enemyAttackAreas;
        private DeathEnemy _deathEnemy;
        private DeathGuard _deathGuard;
        private float _speed = 2f;
        private bool _isFighting = false;
        private float _detectionRadius = 1f;

        public EnemyIdleState(Transform torchTransform,DeathGuard deathGuard, Animator animator, EnemyStateManager enemyStateManager,
            PolygonCollider2D[] enemyAttackAreas)
        {
            _torchTransform = torchTransform;
            _animator = animator;
            _enemyStateManager = enemyStateManager;
            _enemyAttackAreas = enemyAttackAreas;
            _deathGuard = deathGuard;
        }

        public override void EnterState()
        {
            // Найти ближайшего стражника
            FindNewGuard();
            Debug.Log("Enter Enemy Idle State");

            // Подписываемся на событие смерти врага
            _deathEnemy = _torchTransform.GetComponent<DeathEnemy>();
            if (_deathEnemy != null)
            {
                _deathEnemy.OnEnemyDie += StopMove;
            }
        }

        public override void UpdateState()
        {
            // Если стражник найден, враг двигается к нему
             if (_deathGuard != null)
            {
                if (!_isFighting)
                {
                    MoveTowardsGuard();
                }
            }
            else
            {
                FindNewGuard(); // Попробовать найти нового стражника
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exit Enemy Idle State");
            // Отписываемся от события смерти врага
            if (_deathEnemy != null)
            {
                _deathEnemy.OnEnemyDie -= StopMove;
            }
        }

        private void MoveTowardsGuard()
        {
            if (_deathGuard == null)
            {
                Debug.LogWarning("DeathGuard is null. Cannot move towards guard.");
                return; // Прекращаем выполнение метода, если стражник не найден
            }

            // Двигаем врага к цели (стражнику)
            _torchTransform.position = Vector2.MoveTowards(_torchTransform.position, _deathGuard.transform.position, _speed * Time.deltaTime);
            _animator.SetBool("IsMoving", true);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_torchTransform.position, _detectionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<DeathGuard>() != null)
                {
                    Debug.Log("Enemy detected, switching to FightState");
                    Transform _guardTransform = collider.transform;
                    _enemyStateManager.ChangeState(new EnemyFightState(_torchTransform, _animator, _guardTransform, _enemyStateManager, _enemyAttackAreas));
                    return;
                }
            }

            // Определяем направление и разворачиваем врага, если нужно
            Vector3 direction = _deathGuard.transform.position - _torchTransform.position;
            if ((direction.x > 0 && _torchTransform.localScale.x < 0) || (direction.x < 0 && _torchTransform.localScale.x > 0))
            {
                Flip();
            }
        }

        private void FindNewGuard()
        {
            // Ищем ближайшего стражника на сцене
            _deathGuard = FindObjectOfType<DeathGuard>();
            if (_deathGuard != null)
            {
                // Подписываемся на событие смерти стражника
                HealthSystem guardHealth = _deathGuard.GetComponent<HealthSystem>();
                if (guardHealth != null)
                {
                    guardHealth.OnDeath.AddListener(OnGuardDeath);
                }
            }
        }

        private void OnGuardDeath()
        {
            // Отписываемся от события смерти стражника
            if (_deathGuard != null)
            {
                HealthSystem guardHealth = _deathGuard.GetComponent<HealthSystem>();
                if (guardHealth != null)
                {
                    guardHealth.OnDeath.RemoveListener(OnGuardDeath);
                }
            }

            // Очищаем текущую цель и ищем нового стражника
            _deathGuard = null;
            FindNewGuard();
        }

        private void Flip()
        {
            // Меняем направление врага (инвертируем масштаб по оси X)
            Vector3 newScale = _torchTransform.localScale;
            newScale.x *= -1;
            _torchTransform.localScale = newScale;
        }

        private void StopMove(Vector3 position)
        {
            // Останавливаем врага при его смерти
            _speed = 0f;
            _animator.SetBool("IsMoving", false);
        }
    }
}