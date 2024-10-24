using UnityEngine;

namespace State
{
    public class EnemyBreakBuildingState : ObjectState
    {
        private Transform _torchTransform;
        private Transform _guardTransform;
        private Animator _animator;
        private Transform _towerTransform;
        private EnemyStateManager _enemyStateManager;
        private PolygonCollider2D[] _enemyAttackAreas;
        private int _moveSpeed = 2;

        public EnemyBreakBuildingState(Transform torchTransform, Transform guardTransform, Animator animator,
            Transform towerTransform, EnemyStateManager enemyStateManager, PolygonCollider2D[] enemyAttackAreas)
        {
            _torchTransform = torchTransform;
            _guardTransform = guardTransform;
            _animator = animator;
            _towerTransform = towerTransform;
            _enemyStateManager = enemyStateManager;
            _enemyAttackAreas = enemyAttackAreas;
        }

        public override void EnterState()
        {
            Debug.Log("Enter EnemyBreakBuildingState");
            if (_guardTransform == null)
            {
                FindAndMoveToNearestTower();
            }
        }

        public override void UpdateState()
        {
            FindAndMoveToNearestTower();
            MoveOnTower();
        }

        public override void ExitState()
        {
            Debug.Log("Exit EnemyBreakBuildingState");
        }

        private void MoveOnTower()
        {
            if (_towerTransform != null)
            {
                // Расчет направления к башне
                Vector2 direction = (_towerTransform.position - _torchTransform.position).normalized;

                // Перемещение врага к башне
                _torchTransform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;

                // Поворачиваем врага в сторону башни
                if (direction.x > 0)
                {
                    _torchTransform.localScale = new Vector3(Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    _torchTransform.localScale = new Vector3(-Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
                }

                // Здесь можно добавить логику анимации, если нужно
            }
        }

        private void FindAndMoveToNearestTower()
        {
            // Находим все объекты с тегом "Tower" на сцене
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            GameObject nearestTower = null;
            float nearestDistance = Mathf.Infinity;

            // Ищем ближайшую башню
            foreach (GameObject tower in towers)
            {
                float distance = Vector2.Distance(_torchTransform.position, tower.transform.position);
                if (distance < nearestDistance)
                {
                    _enemyStateManager.ChangeState(new EnemyFightState(_torchTransform, _animator, _guardTransform, _enemyStateManager, _enemyAttackAreas));
                    nearestDistance = distance;
                    nearestTower = tower;
                }
            }

            // Если нашли ближайшую башню, начинаем к ней движение
            if (nearestTower != null)
            {
                _towerTransform = nearestTower.transform;
                MoveToTower();
            }
            else
            {
                Debug.Log("No towers found in the scene.");
            }
        }

        private void MoveToTower()
        {
            if (_towerTransform != null)
            {
                float distanceToTower = Vector2.Distance(_torchTransform.position, _towerTransform.position);

                // Если расстояние до вышки больше 1, продолжаем движение
                if (distanceToTower > 2.0f)
                {
                    Vector2 direction = (_towerTransform.position - _torchTransform.position).normalized;
                    _torchTransform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;

                    // Поворачиваем врага в сторону башни
                    if (direction.x > 0)
                    {
                        _torchTransform.localScale = new Vector3(Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
                    }
                    else if (direction.x < 0)
                    {
                        _torchTransform.localScale = new Vector3(-Mathf.Abs(_torchTransform.localScale.x), _torchTransform.localScale.y, _torchTransform.localScale.z);
                    }
                }
                else
                {
                    // Останавливаем врага и включаем анимацию атаки
                    //   _animator.SetBool("IsAttacking", true);
                    Debug.Log("Enemy is close to the tower, starting attack.");
                }
            }
        }
    }
}