using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display_Text;
    private int avgFrameRate;
    private float timer = 0f;
    private float updateInterval = 0.5f; // Обновлять FPS каждые 0.5 секунд

    public void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            avgFrameRate = (int)(1f / Time.deltaTime);
            display_Text.text = avgFrameRate.ToString() + " FPS";
            timer = 0f;
        }
    }
}
