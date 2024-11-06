using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Buildings;
using TMPro;
using UnityEngine;

public class DestroyTower : MonoBehaviour, IBurningBuilding
{
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private GameObject[] _flame;

    private void Start()
    {
        foreach (GameObject obj  in _flame)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            } 
        } 
    }

    private void Update()
    {
        BurningBuilding();
        StopBurningBuilding();
        TowerDestroy();
    }
    
    public void BurningBuilding()
    {
        if (_healthSystem.currentHealth <= _healthSystem.maxHealth / 2) 
        {
            foreach (GameObject obj  in _flame)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                } 
            }
        }
    }

    public void StopBurningBuilding( )
    {
        if (_healthSystem.currentHealth > _healthSystem.maxHealth / 2) 
        {
            foreach (GameObject obj in _flame)
            {
                if (obj != null) 
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void TowerDestroy()
    {
        if (_healthSystem.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
