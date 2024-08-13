using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathGuard : MonoBehaviour
{
    [SerializeField] private Canvas _hpBar;
    private Animator _animator;
    private HealthSystem _healthSystem;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();

        if (_healthSystem != null)
        {
            _healthSystem.OnDeath.AddListener(Die);
        }
    }

    private void Die()
    {
        _hpBar.enabled = false;
        _animator.SetBool("IsDeath", true);
        StartCoroutine(DestroyGuard());
    }

    private IEnumerator DestroyGuard()
    {
        yield return  new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
