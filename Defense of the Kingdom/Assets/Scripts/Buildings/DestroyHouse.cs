using System;
using UnityEngine;

namespace Buildings
{
    public class DestroyHouse : MonoBehaviour, IBurningBuilding
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
                HouseDestroy();
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
        
            private void HouseDestroy()
            {
                if (_healthSystem.currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

