using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using State;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;  // Ссылка на пул объектов
    [SerializeField] private Transform[] _spawnPoints; // Точки спавна
    [Inject(Id = "EnemyAttackAreas")] private PolygonCollider2D[] _enemyAttackAreas;

    private void Start()
    {
        SpawnMultipleEnemies(5);
    }

    // Метод для спавна врага
    public void SpawnMultipleEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Получаем врага из пула
            GameObject newEnemy = _objectPool.GetEnemy();

            // Выбираем случайную точку спавна
            int spawnIndex = Random.Range(0, _spawnPoints.Length);
            newEnemy.transform.position = _spawnPoints[spawnIndex].position;

            // Инициализируем врага, если это необходимо
            newEnemy.GetComponent<EnemyStateManager>().Initialize(_enemyAttackAreas);

            // Найти компонент DeathEnemy и передать его в Loot
            DeathEnemy deathEnemy = newEnemy.GetComponent<DeathEnemy>();
            Loot lootComponent = FindObjectOfType<Loot>();

            if (deathEnemy != null && lootComponent != null)
            {
                lootComponent.Initialize(deathEnemy); // Подписываем Loot на событие смерти врага
            }
            else
            {
                Debug.LogError("DeathEnemy или Loot не найдены.");
            }
        }
    }
}
