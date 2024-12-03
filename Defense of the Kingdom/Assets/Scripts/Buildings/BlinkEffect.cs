using System;
using System.Collections;
using System.Collections.Generic;
using Kalkatos.DottedArrow;
using UnityEngine;
using UnityEngine.Events;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private Arrow _arrow;
    
    [SerializeField] private Color color1 = Color.red;  
    [SerializeField] private Color color2 = Color.blue; 
    [SerializeField] private float duration = 2.0f;
    private Renderer objectRenderer;
    private bool _isBlinking;

    private void OnEnable()
    {
        _arrow.OnArrow += StartBlinking;
        _arrow.OffArrow += StopBlinking;
    }

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_isBlinking)
        {
           Blinking();
        }
    }

    private void OnDisable()
    {
        _arrow.OnArrow -= StartBlinking;
        _arrow.OffArrow -= StopBlinking;
    }

    public void StartBlinking()
    {
        _isBlinking = true;
    }

    public void StopBlinking()
    {
        _isBlinking = false;
        objectRenderer.material.color = color1;
    }

    private void Blinking()
    {
        float lerpTime = Mathf.PingPong(Time.time / duration, 1.0f);
        objectRenderer.material.color = Color.Lerp(color1, color2, lerpTime);
    }
    
}
