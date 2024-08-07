using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _commonEnemyPrefab;
    [SerializeField] private Transform[] _spawnPointEnemy;

    private void Start()
    {
        for (int i = 0; i < _spawnPointEnemy.Length; i++)
        {
           var newEnemy = Instantiate(_commonEnemyPrefab, _spawnPointEnemy[i].position, Quaternion.identity);
        }
    }
}
