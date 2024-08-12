using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _attackArea;
    [SerializeField] private int _damage = 5;
    private Animator _animator;


    private void Start()
    {
        _attackArea.SetActive(false);
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<GuardAttack>())
        {
            HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(_damage);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(OnAttackArea());
            _animator.SetTrigger("Fight");
        }
    }

    IEnumerator OnAttackArea()
    {
        yield return new WaitForSeconds(0.40f);
        _attackArea.SetActive(true);
        yield return new WaitForSeconds(0.1f); // Время, на которое активируется область атаки
        _attackArea.SetActive(false);
    }
}
