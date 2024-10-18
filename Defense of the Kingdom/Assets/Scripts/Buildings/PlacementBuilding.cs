using System;
using System.Security.Cryptography;
using Currensy;
using Unity.VisualScripting;
using UnityEngine;

namespace Buildings
{
    public class PlacementBuilding : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private int  _buildingPrice;
         private Coins _coins;

         private void OnEnable()
         {
             _coins = FindObjectOfType<Coins>();
         }

         private void Update()
        {
            // Проверяем, есть ли здание для размещения
            if (BuyingBuilding.Instance != null && BuyingBuilding.Instance.SpawnedBuilding != null)
            {
                MoveBuildingToMousePosition();
                PlaceBuilding();
                ExitBuildingPlacement(_buildingPrice);
            }
        }

        public  void MoveBuildingToMousePosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Устанавливаем z в 0 для 2D
            BuyingBuilding.Instance.SpawnedBuilding.transform.position = mousePosition; // Передвигаем любое здание
        }

        public  void PlaceBuilding()
        {
            if (Input.GetMouseButtonDown(1) && BuyingBuilding.Instance.SpawnedBuilding != null) // ПКМ для размещения здания
            {
                // Проверяем, находится ли мышь над слоем Ground
                Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, _groundLayer);

                if (hit.collider != null)
                {
                    // Устанавливаем здание на место клика
                    GameObject placedBuilding = BuyingBuilding.Instance.SpawnedBuilding; 
                    placedBuilding.transform.position = hit.point;

                    // После установки отключаем перемещение здания
                    BuyingBuilding.Instance.SpawnedBuilding = null;

                    Debug.Log("Здание установлено на слое Ground!");
                }
                else
                {
                    Debug.Log("Невозможно установить здание. Не на слое Ground.");
                }
            }
        }

        private void ExitBuildingPlacement(int price)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(BuyingBuilding.Instance.SpawnedBuilding); // Отменяем установку здания
                _coins.AddCurrency(price);
            }
        }
    }
}