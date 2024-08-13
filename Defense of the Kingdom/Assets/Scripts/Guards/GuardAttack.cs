using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuardAttack : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 10;
    
    private MoveEnemyOnGuards _enemy;
    private HealthSystem _healthSystem;
    private Animator _animator;

    private void Start()
    {
        _enemy = FindObjectOfType<MoveEnemyOnGuards>();
        _animator = GetComponent<Animator>();
        /*_healthSystem = GetComponent<HealthSystem>();

        if (_healthSystem != null)
        {
            _healthSystem.OnDeath.AddListener(Die);            
        }*/
    }

    private void Update()
    {
       MoveGuard();
    }

    private void MoveGuard()
    {
      //  transform.position = Vector2.MoveTowards(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<MoveEnemyOnGuards>())
        {
            HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
            if (heroHealth != null)
            {
                // Наносим урон герою
                heroHealth.TakeDamage(_damage);
            }
        }
    }

    /*private void Die()
    {
        _animator.SetBool("IsDeath", true);
    }
    */

}
