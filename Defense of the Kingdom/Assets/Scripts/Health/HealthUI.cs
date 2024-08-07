using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;



public class HealthUI : MonoBehaviour
{
    public HealthSystem healthSystem; // Ссылка на компонент здоровья героя
    public Image healthBarImage; // Ссылка на UI-изображение для HP-бара

    void Start()
    {
        // Подписка на событие изменения здоровья
        healthSystem.OnHealthChanged.AddListener(UpdateHealthUI);
        // Инициализация HP-бара
        UpdateHealthUI(healthSystem.currentHealth);
    }

    // Метод для обновления UI
    void UpdateHealthUI(int currentHealth)
    {
        float fillAmount = (float)currentHealth / healthSystem.maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }
}