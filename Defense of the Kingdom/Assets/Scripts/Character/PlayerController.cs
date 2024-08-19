using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
       MoveCharacter();
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

    private void MoveCharacter()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * _speed;

        transform.Translate(horizontalMove, verticalMove, 0);

        if (horizontalMove !=0 || verticalMove != 0)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
        
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);  
        }
        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
    }
}
