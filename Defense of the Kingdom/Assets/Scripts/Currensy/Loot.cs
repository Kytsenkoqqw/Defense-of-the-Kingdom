using System;
using UnityEngine;

namespace Currensy
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private GameObject _lootPrefab;
        private DeathEnemy _deathEnemy;

        public void SetEnemy(DeathEnemy newEnemy)
        {
            if (_deathEnemy != null)
            {
                _deathEnemy.OnEnemyDie -= DropLoot;
            }

            _deathEnemy = newEnemy;
            _deathEnemy.OnEnemyDie += DropLoot;
        }

        private void DropLoot(Vector3 enemyPosition)
        {
            Instantiate(_lootPrefab, enemyPosition, Quaternion.identity);
        }
    }
}