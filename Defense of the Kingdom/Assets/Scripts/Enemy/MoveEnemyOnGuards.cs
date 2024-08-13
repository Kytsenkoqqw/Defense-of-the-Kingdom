using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyOnGuards : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 10;
    
    private MoveGuardOnEnemy _moveGuard;

    private void Start()
    {
        FindNewGuard();
    }

    private void Update()
    {
        if (_moveGuard != null)
        {
            EnemyMove();
            FindNewGuard();
        }
    }

    private void EnemyMove()
    {
        if (_moveGuard != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _moveGuard.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<MoveGuardOnEnemy>())
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
        _moveGuard = FindObjectOfType<MoveGuardOnEnemy>();
        if (_moveGuard != null)
        {
            HealthSystem guardHealth = _moveGuard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.AddListener(OnGuardDeath);
            }
        }
    }

    private void OnGuardDeath()
    {
        if (_moveGuard != null)
        {
            HealthSystem guardHealth = _moveGuard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.RemoveListener(OnGuardDeath);
            }
        }
        
        _moveGuard = null;
        FindNewGuard();
    }
}
