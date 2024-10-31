using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
 
public class AttackArea : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<DestroyTower>(out var destroyTower) && other.TryGetComponent<HealthSystem>(out var heroHealth))
        {
            Debug.Log("bam");
            heroHealth.TakeDamage(_damage);
        }
    }
}
