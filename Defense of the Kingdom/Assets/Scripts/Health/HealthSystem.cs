using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Событие, вызываемое при изменении здоровья
    public UnityEvent<int> OnHealthChanged;
    // Событие, вызываемое при смерти объекта
    public UnityEvent OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Метод для нанесения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        OnHealthChanged.Invoke(currentHealth);
    }

    // Метод для восстановления здоровья
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged.Invoke(currentHealth);
    }

    // Метод для обработки смерти объекта
    void Die()
    {
        OnDeath.Invoke();
        // Логика смерти объекта (например, отключение объекта)
        Destroy(gameObject);
    }
}
