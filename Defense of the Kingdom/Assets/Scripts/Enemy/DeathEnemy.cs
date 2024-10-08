using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
using UnityEngine.Events;

public class DeathEnemy : MonoBehaviour
{
    public Action<Vector3> OnEnemyDie;
    [SerializeField] private Canvas _hpBar;
    [SerializeField] private ObjectPool _objectPool;

    private Animator _animator;
    private HealthSystem _healthSystem;

    private void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDeath.AddListener(Die);
    }

    private void Die()
    {
        OnEnemyDie?.Invoke(transform.position);
        _animator.SetBool("IsDeathEnemy", true);
        _hpBar.enabled = false;
        _healthSystem.OnDeath.RemoveListener(Die);
        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        EnemyManager.Instance.UnregisterEnemy(gameObject);
        yield return new WaitForSeconds(4f);
        _objectPool.ReturnEnemy(gameObject);
    }
}
