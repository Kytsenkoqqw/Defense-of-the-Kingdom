using System;
using System.Collections;
using Currensy;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class RepairTower : MonoBehaviour, IRepairBuilding
    { 
        private Wood _wood;
        [SerializeField] private Button _repairButton;
        [SerializeField] private HealthSystem _healthSystem;

        private void Awake()
        {
            _wood = FindObjectOfType<Wood>();
        }

        private void OnEnable()
        {
            _repairButton.onClick.AddListener(RepairBuilding);
        }


        private void OnMouseDown()
        {
            _repairButton.gameObject.SetActive(true);
        }

        public void RepairBuilding()
        {
            if (_wood.value < 2)
            {
                Debug.Log("Wood " + _wood.value);
                Debug.Log("nema drov");
            }
            else
            {
                _healthSystem.Heal(60);
                _wood.SpendCurrency(2);
            }
        }

        private void OnDisable()
        {
            _repairButton.onClick.RemoveListener(RepairBuilding);
        }
    }
}