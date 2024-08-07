using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _damage = 15;
    

    private void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
        
        transform.Translate(horizontalMove, verticalMove, 0);
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
}
