using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragArrow : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Camera mainCamera;
    private bool isDragging;

    private void Start()
    {
        mainCamera = Camera.main;
        lineRenderer.enabled = false; // Изначально скрываем стрелку
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Debug.Log("Click");
        lineRenderer.enabled = true; // Показываем стрелку при клике
    }

    private void OnMouseUp()
    {
        isDragging = false;
        lineRenderer.enabled = false; // Скрываем стрелку, когда отпускаем
    }

    private void Update()
    {
        if (isDragging)
        {
            // Устанавливаем начало стрелки на позицию объекта
            lineRenderer.SetPosition(0, transform.position);

            // Получаем позицию мыши в мировых координатах
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            // Устанавливаем конец стрелки на позицию мыши
            lineRenderer.SetPosition(1, mousePosition);
        }
    }
}
