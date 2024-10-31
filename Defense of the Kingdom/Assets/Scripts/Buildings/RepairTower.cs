using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class RepairTower : MonoBehaviour, IRepairBuilding
    {
        [SerializeField] private Button _repairButton;
        [SerializeField] private HealthSystem _healthSystem;
        

        private void Start()
        {
            _repairButton.onClick.AddListener(RepairBuilding);
            _repairButton.gameObject.SetActive(false);
        }

        private void OnMouseDown()
        {
            _repairButton.gameObject.SetActive(true);
        }


        public void RepairBuilding()
        {
            StartCoroutine(Repair());
        }

        private IEnumerator Repair()
        {
            yield return new WaitForSeconds(3);
            _healthSystem.Heal(60);
            
        }

        private void OnDisable()
        {
            _repairButton.onClick.RemoveListener(RepairBuilding);
        }
    }
}