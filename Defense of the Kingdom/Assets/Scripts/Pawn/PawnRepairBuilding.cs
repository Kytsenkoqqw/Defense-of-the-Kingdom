using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PawnRepairBuilding : MonoBehaviour
{
    public UnityEvent StartPawnMove;
    private RepairBuilding _repairBuilding;
    [SerializeField] private LayerMask interactableLayer; // Укажите слой объекта для фильтрации Raycast
    [SerializeField] private Transform[] _buildings; // Массив объектов для перемещения
    [SerializeField] private float _pawnSpeed = 2f; // Скорость движения объекта
    
    private Transform targetBuilding = null; // Текущая цель для перемещения
    private bool isMoving = false; // Флаг, который показывает, что нужно двигаться

    private void Awake()
    {
        _repairBuilding = FindObjectOfType<RepairBuilding>();
    }

    private void Update()
    {
        DetectHover();
        MoveToTarget();
    }

    private void DetectHover()
    {
        // Преобразуем позицию мыши в мировые координаты
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Запускаем 2D Raycast из точки мыши
        RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);
        if (hitInfo.collider != null && !isMoving) // Изменение: не менять цель, если уже движемся
        {
            // Если Raycast попал в объект и персонаж не двигается, обновляем цель
            Debug.Log("Наведение на объект: " + hitInfo.collider.name);

            // Присваиваем целевое здание
            targetBuilding = hitInfo.collider.transform;

            // Запускаем движение, если была нажата кнопка
            if (Input.GetMouseButtonDown(0)) 
            {
                isMoving = true;
            }
        }
    }

    public void MoveToTarget()
    {
        // Если есть цель, начинаем движение
        if (isMoving && targetBuilding != null)
        {
            StartPawnMove?.Invoke();
            // Двигаем объект к выбранному зданию
            transform.position = Vector2.MoveTowards(transform.position, targetBuilding.position, _pawnSpeed * Time.deltaTime);

            // Проверяем, достигли ли мы цели
            if (Vector2.Distance(transform.position, targetBuilding.position) < 0.1f)
            {
                Debug.Log("Достигнуто здание: " + targetBuilding.name);
                isMoving = false; // Сбрасываем флаг после достижения цели
                targetBuilding = null; // Сбрасываем цель
                StartCoroutine(_repairBuilding.RepairBuildings());
            }
        }
    }

   
}
