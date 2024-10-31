using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
 
public class AttackArea : MonoBehaviour
{
    [SerializeField] private int _damage = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DestroyTower>())
        {
            HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
            if (heroHealth != null)
            {
                heroHealth.TakeDamage(_damage);
            }
        }
    }
}
