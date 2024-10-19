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
        private GameObject _currentBuilding;  // Храним текущее здание, которое двигаем за мышью
        private bool _isPlacingBuilding = false;

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
        }
    }
}