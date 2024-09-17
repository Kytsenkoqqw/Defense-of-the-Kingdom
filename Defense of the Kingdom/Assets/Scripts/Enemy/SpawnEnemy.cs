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
    [SerializeField] private Loot _lootManager; // Ссылка на скрипт Loot

    private void Start()
    {
        for (int i = 0; i < _spawnPointEnemy.Length; i++)
        {
            // Спавним нового врага
            var newEnemy = Instantiate(_commonEnemyPrefab, _spawnPointEnemy[i].position, Quaternion.identity);

            // Получаем компонент DeathEnemy у нового врага
            DeathEnemy deathEnemy = newEnemy.GetComponent<DeathEnemy>();

            // Передаем ссылку на нового врага в скрипт Loot
            _lootManager.SetEnemy(deathEnemy);
        }
    }
}
