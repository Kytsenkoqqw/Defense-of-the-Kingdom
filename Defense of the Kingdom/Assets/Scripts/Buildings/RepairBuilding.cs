using System;
using System.Collections;
using Currensy;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class RepairBuilding : MonoBehaviour
    { 
        [SerializeField] private Wood _wood;
        [SerializeField] private HealthSystem _healthSystem;
        

        public IEnumerator RepairBuildings()
        {
            if (_wood.value < 2) 
            {
                Debug.Log("Wood " + _wood.value);
                Debug.Log("nema drov");
            }
            else
            {
                if (_healthSystem.currentHealth < _healthSystem.maxHealth)
                {
                    yield return new WaitForSeconds(3f);
                    _healthSystem.Heal(60);
                    _wood.SpendCurrency(2);
                    Debug.Log("Heal");
                }
                else
                {
                    Debug.Log("Full health");
                }
            }
        }
    }
}