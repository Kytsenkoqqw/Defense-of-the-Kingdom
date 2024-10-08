using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public event Action OnAllEnemiesDeactivated; // Событие для оповещения, когда все враги деактивированы

    private List<GameObject> _enemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Чтобы этот объект сохранялся между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckEnemies();
    }

    // Метод для добавления врагов в список
    public void RegisterEnemy(GameObject enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
        }
    }

    // Метод для удаления врагов из списка (если они уничтожены)
    public void UnregisterEnemy(GameObject enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    // Проверка, активны ли все враги
    private void CheckEnemies()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy.activeSelf)
            {
                return; // Если хоть один враг активен, возвращаемся
            }
        }

        // Если все враги деактивированы, вызываем событие
        OnAllEnemiesDeactivated?.Invoke();
    }
}