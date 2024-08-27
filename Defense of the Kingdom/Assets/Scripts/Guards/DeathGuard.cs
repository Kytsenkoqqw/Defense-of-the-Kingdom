using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DeathGuard : MonoBehaviour
{
    [SerializeField] private Canvas _hpBar;
    
    private PlayerController _playerController;
    private Animator _animator;
    private HealthSystem _healthSystem;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDeath.AddListener(Die);
    }
    
    private void Die()
    {
        _animator.SetBool("IsDeath", true);
        _hpBar.enabled = false;
        StartCoroutine(DestroyGuard());
    }

    private IEnumerator DestroyGuard()
    {
        yield return  new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}

