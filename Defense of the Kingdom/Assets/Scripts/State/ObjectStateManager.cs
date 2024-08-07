using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public ObjectState CurrentState { get; private set; }
    public SearchState SearchState = new SearchState();
    public FightState FightState = new FightState();

    public Transform target; // Цель, которую нужно искать или атаковать
    public float detectionDistance = 2.0f; // Дистанция для переключения между состояниями

    private void Start()
    {
        CurrentState = SearchState;
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void ChangeState(ObjectState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
