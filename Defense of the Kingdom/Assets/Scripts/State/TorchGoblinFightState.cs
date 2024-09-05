using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchGoblinFightState : FightState    
{
    public TorchGoblinFightState(Transform objectTransform, Animator animator, Transform enemyTransform,
        Transform[] waypoints, StateMachine stateMachine)
        : base(objectTransform, animator, enemyTransform, waypoints, stateMachine) { }

    protected override ObjectState CreateIdleState()
    {
        return new TorchGoblinIdleState();
    }

    protected override void ActivateAttackArea(string attackType)
    {
        // Логика активации областей атаки для врагов (можно добавить свою логику)
        Debug.Log("Enemy attack area activation: " + attackType);
        _stateMachine.SetAttackAreaActive(attackType);
    }
}
