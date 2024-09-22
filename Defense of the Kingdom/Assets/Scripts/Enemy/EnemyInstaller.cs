using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private PolygonCollider2D[] _enemyAttackAreas;

        public override void InstallBindings()
        {
            Container.Bind<PolygonCollider2D[]>().WithId("EnemyAttackAreas").FromInstance(_enemyAttackAreas).AsTransient();
        }
    }
}