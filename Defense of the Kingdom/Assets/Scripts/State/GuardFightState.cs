using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using State;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GuardFightState : ObjectState
{
    private Transform _objectTransform;
    private Animator _animator;
    private Transform _enemyTransform;
    private Transform[] _waypoints;
    private PolygonCollider2D[] _attackAreas;
    private StateManager _stateManager;
    private Transform _torchTransform;
    private bool _hasSwitchedToIdle;


    public GuardFightState(Transform objectTransform, Animator animator, Transform enemyTransform,
        Transform[] waypoints, StateManager stateManager, PolygonCollider2D[] attackAreas)
    {
        _objectTransform = objectTransform;
        _animator = animator;
        _enemyTransform = enemyTransform;
        _waypoints = waypoints;
        _stateManager = stateManager;
        _attackAreas = attackAreas;
        _hasSwitchedToIdle = false;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Fight State");
        EnemyManager.Instance.OnAllEnemiesDeactivated += SwitchToIdleState;
    }

    public override void UpdateState()
    {
        /*if (_enemyTransform == null)
        {
            Debug.LogWarning("Enemy transform is null, switching to Idle state.");
            _stateManager.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateManager, _attackAreas, _torchTransform));
            return;
        }*/

        Vector2 direction = _enemyTransform.position - _objectTransform.position;

        // Определяем направление движения (слева или справа)
        float horizontalDirection = direction.x;

        // Поворачиваем стражника в сторону врага
        if (horizontalDirection > 0)
        {
            _objectTransform.localScale = new Vector3(Mathf.Abs(_objectTransform.localScale.x), _objectTransform.localScale.y, _objectTransform.localScale.z);
        }
        else if (horizontalDirection < 0)
        {
            _objectTransform.localScale = new Vector3(-Mathf.Abs(_objectTransform.localScale.x), _objectTransform.localScale.y, _objectTransform.localScale.z);
        }

        // Выбор типа атаки на основе вертикальной позиции врага
        if (Mathf.Abs(direction.y) < 0.3f)
        {
            FrontAttack();
        }
        else if (direction.y > 0)
        {
            AttackUp();
        }
        else
        {
            AttackDown();
        }

        // Проверяем, находится ли враг в радиусе атаки
        if (!IsEnemyInRange())
        {
            _stateManager.ChangeState(new GuardChaseState(_objectTransform, _animator, _waypoints, _enemyTransform, _stateManager, _attackAreas));
            OffAttackAnimation();
            _animator.SetBool("IsMoving", true);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Fight State");
        EnemyManager.Instance.OnAllEnemiesDeactivated -= SwitchToIdleState;
    }

    public void SwitchToIdleState()
    {
        if (!_hasSwitchedToIdle) // Проверяем, был ли метод вызван ранее
        {
            Debug.Log("Switch to Idle State");
            _hasSwitchedToIdle = true; // Устанавливаем флаг, чтобы метод больше не вызывался
            _stateManager.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateManager, _attackAreas, _torchTransform));
        }
    }

    private void OffAttackAnimation()
    {
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack", false);
    }

    private bool IsEnemyInRange()
    {
        return Vector2.Distance(_objectTransform.position, _enemyTransform.position) <= 1.7f;
    }

    private void AttackUp()
    {
        _animator.SetBool("UpAttack", true);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack", false);
        _stateManager.SetAttackAreaActive("UpAttack");
    }

    private void FrontAttack()
    {
        _animator.SetBool("FrontAttack", true);
        _animator.SetBool("DownAttack", false);
        _animator.SetBool("UpAttack", false);
        _stateManager.SetAttackAreaActive("FrontAttack");
    }

    private void AttackDown()
    {
        _animator.SetBool("DownAttack", true);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("UpAttack", false);
        _stateManager.SetAttackAreaActive("DownAttack");
    }
    
}
