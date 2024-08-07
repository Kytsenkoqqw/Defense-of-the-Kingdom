using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyOnGuards : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 10;
    
    private GuardAttack _guard;

    private void Start()
    {
        FindNewGuard();
    }

    private void Update()
    {
        if (_guard != null)
        {
            EnemyMove();
            FindNewGuard();
        }
    }

    private void EnemyMove()
    {
        if (_guard != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _guard.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<GuardAttack>())
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
        _guard = FindObjectOfType<GuardAttack>();
        if (_guard != null)
        {
            HealthSystem guardHealth = _guard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.AddListener(OnGuardDeath);
            }
        }
    }

    private void OnGuardDeath()
    {
        if (_guard != null)
        {
            HealthSystem guardHealth = _guard.GetComponent<HealthSystem>();
            if (guardHealth != null)
            {
                guardHealth.OnDeath.RemoveListener(OnGuardDeath);
            }
        }
        
        _guard = null;
        FindNewGuard();
    }
}
