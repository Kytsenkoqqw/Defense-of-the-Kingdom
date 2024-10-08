using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset  = new Vector3(0,0,-15);

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;

        if (Input.GetMouseButton(1))
        {
            transform.position = Input.mousePosition + _offset;
        }
    }
    
    
}
