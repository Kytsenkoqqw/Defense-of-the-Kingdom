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
        Spawn();
    }

    // Метод для спавна врага
    private void Spawn()
    {
        // Получаем врага из пула
        GameObject newEnemy = _objectPool.GetEnemy();

        // Выбираем случайную точку спавна
        int spawnIndex = Random.Range(0, _spawnPoints.Length);
        newEnemy.transform.position = _spawnPoints[spawnIndex].position;

        // Инициализируем врага, если нужно
        // Например, передаём ему необходимые параметры
        newEnemy.GetComponent<EnemyStateManager>().Initialize(_enemyAttackAreas);
    }

    // Когда враг умирает, возвращаем его в пул
    public void OnEnemyDie(GameObject enemy)
    {
        _objectPool.ReturnEnemy(enemy);
    }
}
