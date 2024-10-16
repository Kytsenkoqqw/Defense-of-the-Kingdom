using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private void Start()
    {
        SpawnPosition();
    }

    private void Update()
    {
        SpawnPosition();
        
        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }


    private void SpawnPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, _groundLayer))
        {
            transform.position = hit.point;
        }
    }
}
