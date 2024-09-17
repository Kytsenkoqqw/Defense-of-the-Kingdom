using System;
using System.Collections;
using System.Collections.Generic;
using Currensy;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _commonEnemyPrefab;
    [SerializeField] private Transform[] _spawnPointEnemy;
    [SerializeField] private Loot _lootManager; // Ссылка на Loot

    private void Start()
    {
        for (int i = 0; i < _spawnPointEnemy.Length; i++)
        {
            var newEnemy = Instantiate(_commonEnemyPrefab, _spawnPointEnemy[i].position, Quaternion.identity);
            DeathEnemy deathEnemy = newEnemy.GetComponent<DeathEnemy>();
            deathEnemy.OnEnemyDie += _lootManager.DropLoot; // Подписываем врага на событие лута
        }
    }
}
