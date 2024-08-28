using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyOnGuards : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 10;

    private Animator _animator;
    private DeathGuard _deathGuard;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        FindNewGuard();
    }

    private void Update()
    {
        if (_deathGuard != null)
        {
            EnemyMove();
            FindNewGuard();
        }
    }

    private void EnemyMove()
    {
        if (_deathGuard != null)
        {
            // Двигаем врага к цели
            transform.position = Vector2.MoveTowards(transform.position, _deathGuard.transform.position, _speed * Time.deltaTime);
            _animator.SetBool("IsMoving", true);

            // Определяем направление движения
            Vector3 direction = _deathGuard.transform.position - transform.position;

            // Поворачиваем врага в сторону движения
            if (direction.x > 0 && transform.localScale.x < 0)
            {
                Flip();
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            if (_speed <= 0)
            {
                _animator.SetBool("IsMoving", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<DeathGuard>())
        {
            HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
            if (heroHealth != null)
            {
                // Наносим урон герою
                heroHealth.TakeDamage(_damage);
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
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
