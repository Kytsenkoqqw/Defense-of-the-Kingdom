using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        float verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * _speed;
        
        transform.Translate(horizontalMove, verticalMove, 0);
    }
}
