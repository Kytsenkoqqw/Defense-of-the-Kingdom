using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyOnGuards : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private GuardAttack _guard;

    private void Start()
    {
        _guard = FindObjectOfType<GuardAttack>();
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, _guard.transform.position, _speed * Time.deltaTime);
    }
}
