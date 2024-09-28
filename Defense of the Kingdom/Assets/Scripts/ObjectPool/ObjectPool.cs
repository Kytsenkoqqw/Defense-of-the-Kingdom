using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   public GameObject enemyPrefab;
   public int poolSize = 10;
   private Queue<GameObject> pool = new Queue<GameObject>();

   private void Start()
   {
      for (int i = 0; i < poolSize; i++)
      {
         var enemy = Instantiate(enemyPrefab);
         enemy.SetActive(false); // Отключаем объект
         pool.Enqueue(enemy); // Добавляем в пул
      }
   }
   
   public GameObject GetEnemy()
   {
      if (pool.Count > 0)
      {
         var enemy = pool.Dequeue();
         enemy.SetActive(true); // Активируем врага
         return enemy;
      }

      // Если врагов не хватает, можно добавить больше
      var newEnemy = Instantiate(enemyPrefab);
      return newEnemy;
   }

   public void ReturnEnemy(GameObject enemy)
   {
      enemy.SetActive(false); // Отключаем врага и возвращаем в пул
      pool.Enqueue(enemy);
   }
}
