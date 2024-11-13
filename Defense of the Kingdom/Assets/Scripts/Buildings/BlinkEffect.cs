using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlinkEffect : MonoBehaviour
{
    public UnityEvent StartBlink;
    [SerializeField] private Color startColor = Color.white;  
    [SerializeField] private Color blinkColor = Color.red;    
    [SerializeField] private float blinkSpeed = 2f;           

    private Renderer objectRenderer;
    private bool isBlinking;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("На объекте отсутствует компонент Renderer.");
            enabled = false;
            return;
        }

        objectRenderer.material.color = startColor;
        StartBlinking();
    }

    private void Update()
    {
        
    }

    private void StartBlinkEffect()
    {
        if (isBlinking)
        {
            StartBlink?.Invoke();
            // Плавно изменяем цвет между startColor и blinkColor
            float t = (Mathf.Sin(Time.time * blinkSpeed) + 1) / 2; // От 0 до 1
            objectRenderer.material.color = Color.Lerp(startColor, blinkColor, t);
        }
    }

    public void StartBlinking()
    {
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
        objectRenderer.material.color = startColor; // Возвращаем объекту начальный цвет
    }
}
