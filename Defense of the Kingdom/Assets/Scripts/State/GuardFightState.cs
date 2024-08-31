using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;

public class GuardFightState : ObjectState
{
    private Transform _objectTransform;
    private Animator _animator;
    private Transform _enemyTransform;
    private Transform[] _waypoints;
    private StateMachine _stateMachine;
    private PolygonCollider2D _upAttackArea;
    private PolygonCollider2D _frontAttackArea;
    private PolygonCollider2D _downAttackArea;

    public GuardFightState(Transform objectTransform, Animator animator, Transform enemyTransform,
        Transform[] waypoints, StateMachine stateMachine,
        PolygonCollider2D upAttackArea, PolygonCollider2D frontAttackArea, PolygonCollider2D downAttackArea)
    {
        _objectTransform = objectTransform;
        _animator = animator;
        _enemyTransform = enemyTransform;
        _waypoints = waypoints;
        _stateMachine = stateMachine;
        _upAttackArea = upAttackArea;
        _frontAttackArea = frontAttackArea;
        _downAttackArea = downAttackArea;
    }

    public override void Enter()
    {
        Debug.Log("Entering Fight State");
    }

    public override void Update()
    {
        if (_enemyTransform == null)
        {
            Debug.LogWarning("Enemy transform is null, switching to Idle state.");
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            return;
        }

        Vector2 direction = _enemyTransform.position - _objectTransform.position;

        // Определяем направление движения (слева или справа)
        float horizontalDirection = direction.x;

        // Поворачиваем стражника в сторону врага
        if (horizontalDirection > 0)
        {
            // Враг справа, стражник смотрит вправо
            _objectTransform.localScale = new Vector3(Mathf.Abs(_objectTransform.localScale.x),
                _objectTransform.localScale.y, _objectTransform.localScale.z);
        }
        else if (horizontalDirection < 0)
        {
            // Враг слева, стражник смотрит влево
            _objectTransform.localScale = new Vector3(-Mathf.Abs(_objectTransform.localScale.x),
                _objectTransform.localScale.y, _objectTransform.localScale.z);
        }

        // Выбор типа атаки на основе вертикальной позиции врага
        if (Mathf.Abs(direction.y) < 0.3f) // Если враг находится на одном уровне по высоте (с допуском)
        {
            FrontAttack(); // Боковая атака
        }
        else if (direction.y > 0)
        {
            AttackUp(); // Атака вверх
        }
        else
        {
            AttackDown(); // Атака вниз
        }

        // Проверяем, находится ли враг в радиусе атаки
        if (!IsEnemyInRange())
        {
            _stateMachine.ChangeState(new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine));
            OffAttackAnimation();
            _animator.SetBool("IsMoving", true);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Fight State");
    }

    private void OffAttackAnimation()
    {
        _animator.SetBool("UpAttack", false);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack", false);
    }

    private bool IsEnemyInRange()
    {
        return Vector2.Distance(_objectTransform.position, _enemyTransform.position) <= 5f;
    }

    private void AttackUp()
    {
        _animator.SetBool("UpAttack", true);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("DownAttack", false);
        _upAttackArea.enabled = true;
    }

    private void FrontAttack()
    {
        _animator.SetBool("FrontAttack", true);
        _animator.SetBool("DownAttack", false);
        _animator.SetBool("UpAttack", false);
        _frontAttackArea.enabled = true;
    }

    private void AttackDown()
    {
        _animator.SetBool("DownAttack", true);
        _animator.SetBool("FrontAttack", false);
        _animator.SetBool("UpAttack", false);
        _downAttackArea.enabled = true;
    }
    
    IEnumerator OnAttackArea(GameObject attackArea, float activationDelay, float activeDuration)
    {
        yield return new WaitForSeconds(activationDelay);
        attackArea.SetActive(true);
        yield return new WaitForSeconds(activeDuration); // Время, на которое активируется область атаки
        attackArea.SetActive(false);
    }  
    
    
}
