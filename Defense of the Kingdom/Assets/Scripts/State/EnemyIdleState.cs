using System;
using UnityEngine;

namespace State
{
    public class EnemyIdleState : ObjectState    
    {
        
        private Animator _animator;
        private DeathGuard _deathGuard;
        private DeathEnemy _deathEnemy;
        private Transform _guardTransform;
        private EnemyStateManager _enemyStateManager;
        private Transform _torchTransform;
        private PolygonCollider2D[] _enemyAttackAreas;
        [SerializeField] private float _speed = 5f;
        
        public EnemyIdleState(Transform transform, Animator animator, EnemyStateManager enemyStateManager, PolygonCollider2D[] enemyAttackAreas)
        {
            _guardTransform = transform;
            _animator = animator;
            _enemyStateManager = enemyStateManager;
            _enemyAttackAreas = enemyAttackAreas;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _deathEnemy = GetComponent<DeathEnemy>();
            _deathEnemy.OnEnemyDie += StopMove;
        }

        public override void EnterState()
        {
            FindNewGuard();
            Debug.Log("Enter Enemy Idle State");
        }

        public override void UpdateState()
        {
            if (_deathGuard != null)
            {
                EnemyMove();
                FindNewGuard();
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exit Enemt Idle State");
        }
        
    private void OnDisable()
    {
        _deathEnemy.OnEnemyDie -= StopMove;
    }

    private void EnemyMove()
    {
        if (_deathGuard != null)
        {
            // Двигаем врага к цели
            _guardTransform.position = Vector2.MoveTowards(_guardTransform.position, _deathGuard.transform.position, _speed * Time.deltaTime);
            _animator.SetBool("IsMoving", true);

            // Определяем направление движения
            Vector3 direction = _deathGuard.transform.position - _guardTransform.position;

            if (direction.x <= 1 && direction.y <= 1)
            {
                _speed = 0f;
                _enemyStateManager.ChangeState(new EnemyFightState(_torchTransform, _animator, _guardTransform, _enemyStateManager, _enemyAttackAreas));
            }
            else
            {
                _speed = 2f;
            }

            // Поворачиваем врага в сторону движения
            if (direction.x > 0 && _guardTransform.localScale.x < 0)
            {
                Flip();
            }
            else if (direction.x < 0 && _guardTransform.localScale.x > 0)
            {
                Flip();
            }

            if (_speed <= 0)
            {
                _animator.SetBool("IsMoving", false);
            }
        }
    }

    private void FindNewGuard()
    {
        _deathGuard = FindObjectOfType<DeathGuard>();
        if (_deathGuard != null)
        {
            HealthSystem guardHealth = _deathGuard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.AddListener(OnGuardDeath);
            }
        }
    }

    private void OnGuardDeath()
    {
        if (_deathGuard != null)
        {
            HealthSystem guardHealth = _deathGuard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.RemoveListener(OnGuardDeath);
            }
        }
        
        _deathGuard = null;
        FindNewGuard();
    }

    private void Flip()
    {
        // Меняем знак масштаба по оси X, чтобы развернуть объект
        Vector3 newScale = _guardTransform.localScale;
        newScale.x *= -1;
        _guardTransform.localScale = newScale;
    }

    private void StopMove(Vector3 position)
    {
        _speed = 0f;
    }
    }
}