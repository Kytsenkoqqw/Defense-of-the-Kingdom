using System;
using UnityEngine;

namespace Currensy
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private DeathEnemy _deathEnemy;
    
        private void OnEnable()
        {
            _deathEnemy.OnEnemyDie += DropLoot;
        }

        private void OnDisable()
        {
            _deathEnemy.OnEnemyDie -= DropLoot;
        }

        private void DropLoot(Vector3 enemyPosition)
        {
            Instantiate(_lootPrefab, enemyPosition, Quaternion.identity);
        }
        
    }
}