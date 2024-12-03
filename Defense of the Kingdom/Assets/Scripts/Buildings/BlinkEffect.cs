using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.white;  
    [SerializeField] private Color blinkColor = Color.red;    
    [SerializeField] private float blinkSpeed = 2f;           

    private Renderer objectRenderer;
    public bool isBlinking;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("На объекте отсутствует компонент Renderer.");
            enabled = false;
            return;
        }
       // StartBlinking();

        objectRenderer.material.color = startColor;
    }

    private void Update()
    {
        if (isBlinking)
        {
            Debug.Log("Blink ");
            StartBlinkEffect();
        }
       
     
    }

    public void StartBlinkEffect()
    {
        if (isBlinking)
        {
            // Плавно изменяем цвет между startColor и blinkColor
            float t = (Mathf.Sin(Time.time * blinkSpeed) + 1) / 2; // От 0 до 1
            objectRenderer.material.color = Color.Lerp(startColor, blinkColor, t);
        }
    }

    public void StartBlinking()
    {
        Debug.Log("Start Blinking");
        isBlinking = true;
    }

    public void StopBlinking()
    {
        isBlinking = false;
        objectRenderer.material.color = startColor; // Возвращаем объекту начальный цвет
    }
}
