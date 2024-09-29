using System;
using UnityEngine;

namespace Currensy
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private GameObject _lootPrefab;
        [SerializeField] private DeathEnemy _deathEnemy;
    
        public void Initialize(DeathEnemy deathEnemy)
        {
            _deathEnemy = deathEnemy;
            _deathEnemy.OnEnemyDie += DropLoot;
        }
        
        public void OnEnable()
        {
            _deathEnemy.OnEnemyDie += DropLoot;
        }

        public void OnDisable()
        {
            _deathEnemy.OnEnemyDie -= DropLoot;
        }

        public void DropLoot(Vector3 enemyPosition)
        {
            Debug.Log("Loot");
            Instantiate(_lootPrefab, enemyPosition, Quaternion.identity);
        }
        
    }
}