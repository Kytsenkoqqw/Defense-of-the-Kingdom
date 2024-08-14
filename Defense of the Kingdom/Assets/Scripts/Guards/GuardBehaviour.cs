using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GuardBehaviour : MonoBehaviour
{
    public static Action OnFighting;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Canvas _hpBar;
    
    private PlayerController _playerController;
    private Animator _animator;
    private Vector2 _previousPosition;
    private HealthSystem _healthSystem;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _animator = GetComponent<Animator>();
        _healthSystem = GetComponent<HealthSystem>();    
        _previousPosition = transform.position;
        _healthSystem.OnDeath.AddListener(Die);
    }

    private void Update()
    {
        MoveGuard();
        UpdateAnimation();
        UpdateDirection();
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void MoveGuard()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerController.transform.position, _speed * Time.deltaTime);
        
        float detectionRange = Vector2.Distance(transform.position, _playerController.transform.position);
        if (detectionRange <= 1)
        {
            OnFighting?.Invoke();
        }
    }

    private void UpdateAnimation()
    {
        // Вычисляем текущую скорость
        float speed = ((Vector2)transform.position - _previousPosition).magnitude / Time.deltaTime;

        // Запускаем анимацию бега, если скорость больше нуля
        _animator.SetBool("IsMoving", speed > 0.1f); // Используем небольшой порог для устранения ошибок округления

        _previousPosition = transform.position;
    }

    private void UpdateDirection()
    {
        // Определяем направление движения
        Vector2 direction = _playerController.transform.position - transform.position;

        // Если стражник движется влево
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Смотрит влево
        }
        // Если стражник движется вправо
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Смотрит вправо
        }
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

