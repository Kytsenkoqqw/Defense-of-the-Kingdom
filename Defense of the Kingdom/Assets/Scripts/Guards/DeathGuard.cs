using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DeathGuard : MonoBehaviour
{
   
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Canvas _hpBar;
    
    private PlayerController _playerController;
    private Animator _animator;
    private Vector2 _previousPosition;
    private HealthSystem _healthSystem;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDeath.AddListener(Die);
    }
    
    private void Die()
    {
        _speed = 0f;
        _hpBar.enabled = false;
        _animator.SetBool("IsDeath", true);
        StartCoroutine(DestroyGuard());
    }

    private IEnumerator DestroyGuard()
    {
        yield return  new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D other)
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
    }*/
}

