using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathEnemy : MonoBehaviour
{
    public Action<Vector3> OnEnemyDie;
    [SerializeField] private Canvas _hpBar;

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
        OnEnemyDie?.Invoke(transform.position); // Передаем позицию врага при его смерти
        _animator.SetBool("IsDeathEnemy", true);
        _hpBar.enabled = false;
        StartCoroutine(DestroyEnemy());
        _healthSystem.OnDeath.RemoveListener(Die);
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
