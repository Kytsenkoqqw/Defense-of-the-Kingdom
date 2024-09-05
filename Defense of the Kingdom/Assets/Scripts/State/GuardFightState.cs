using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Rendering.Universal;
using Zenject;

public class GuardFightState : FightState
{
    public GuardFightState(Transform objectTransform, Animator animator, Transform enemyTransform,
        Transform[] waypoints, StateMachine stateMachine)
        : base(objectTransform, animator, enemyTransform, waypoints, stateMachine) { }

    protected override ObjectState CreateIdleState()
    {
        return new GuardIdleState(_objectTransform, _animator, _waypoints, _stateMachine);
    }

    protected override void ActivateAttackArea(string attackType)
    {
        _stateMachine.SetAttackAreaActive(attackType);
    }
}
