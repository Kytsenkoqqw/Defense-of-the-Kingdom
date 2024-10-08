using UnityEngine;

namespace Enemy
{
    public class EnemyAttackArea : MonoBehaviour
    {
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _damageInterval = 25f; // Интервал между нанесением урона

        private float _lastDamageTime = 0f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<DeathGuard>())
            {
                HealthSystem heroHealth = other.gameObject.GetComponent<HealthSystem>();
                if (heroHealth != null && Time.time - _lastDamageTime >= _damageInterval)
                {
                    heroHealth.TakeDamage(_damage);
                    _lastDamageTime = Time.time;
                }
            }
        } 
    }
}