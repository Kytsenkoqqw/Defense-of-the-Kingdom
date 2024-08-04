using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        
    }

    private void Update()
    {
        
    }

    private void EnemyMove()
    {
         
    }
}
