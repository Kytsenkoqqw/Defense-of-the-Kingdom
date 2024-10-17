using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

namespace Buildings
{
    public class PlacementTower : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;

        private bool _isPlacingTower;
        
        private void Update()
        {
            if (BuyingTower.Instance != null && BuyingTower.Instance.SpawnedTower != null)
            {
                MoveTowerToMousePosition();
                PlaceTower();
                ExitTowerPacement();
            }
        }

        private void MoveTowerToMousePosition()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Устанавливаем z в 0 для 2D
            BuyingTower.Instance.SpawnedTower.transform.position = mousePosition;
        }
        
        private void PlaceTower()
        {
            if (Input.GetMouseButtonDown(1) && BuyingTower.Instance.SpawnedTower != null) // ПКМ для размещения башни
            {
                // Проверяем, находится ли мышь над слоем Ground
                Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero, Mathf.Infinity, _groundLayer);

                if (hit.collider != null)
                {
                    // Здесь мы можем, например, сохранить ссылку на размещённую башню, если это нужно
                    GameObject placedTower = BuyingTower.Instance.SpawnedTower; 

                    // Устанавливаем позицию башни на место клика
                    placedTower.transform.position = hit.point; // Устанавливаем башню на место клика

                    // Уничтожаем или деактивируем SpawnedTower после установки
                    BuyingTower.Instance.SpawnedTower = null; // Убираем ссылку на спавнённую башню, чтобы её больше не перемещать

                    Debug.Log("Башня установлена на слое Ground!");
                }
                else
                {
                    Debug.Log("Невозможно установить башню. Не на слое Ground.");
                }
            }
        }

        private void ExitTowerPacement()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(BuyingTower.Instance.SpawnedTower);
            }
        }

    }
}