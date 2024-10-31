using System;
using System.Security.Cryptography;
using Currensy;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Buildings
{
    public class PlacementBuilding : MonoBehaviour
    {
        private GameObject _currentBuilding;  // Храним текущее здание, которое двигаем за мышью
        private bool _isPlacingBuilding = false;
        [SerializeField] private BuyingBuilding _buyingBuilding;
        [SerializeField] private Coins _coins;
        

        private void Update()
        {
            if (_isPlacingBuilding)
            {
                MoveBuildingToMousePosition();

                // Проверяем, если нажата правая кнопка мыши, то фиксируем здание
                if (Input.GetMouseButtonDown(1))  // Правая кнопка мыши
                {
                    PlaceBuilding();
                }
                CancelPlaceBuilding();
            }
        }

        // Начало перемещения здания за мышью
        public void StartPlacingBuilding(GameObject building)
        {
            _currentBuilding = building;
            _isPlacingBuilding = true;
        }

        // Метод для перемещения здания за мышью
        private void MoveBuildingToMousePosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Устанавливаем z в 0 для 2D

            if (_currentBuilding != null)
            {
                _currentBuilding.transform.position = mousePosition;
            }
        }

        // Метод для завершения перемещения и "установки" здания
        private void PlaceBuilding()
        {
            _isPlacingBuilding = false;
            _currentBuilding = null;
            //_coins.SpendCurrency(_buyingBuilding._buildingPrice);
        }

        private void CancelPlaceBuilding()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Destroy(_currentBuilding);
                _coins.AddCurrency(_buyingBuilding._buildingPrice);
            }
        }
    }
}