using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class GuardAttackArea : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _damageInterval = 25f; // Интервал между нанесением урона

    private float _lastDamageTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyStateManager>())
        {
            HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
            if (heroHealth != null && Time.time - _lastDamageTime >= _damageInterval)
            {
                heroHealth.TakeDamage(_damage);
                _lastDamageTime = Time.time;
            }
        }
    } 
}
