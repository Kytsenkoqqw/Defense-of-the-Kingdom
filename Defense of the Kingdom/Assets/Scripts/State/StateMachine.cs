using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class StateMachine : MonoBehaviour
{
    private static StateMachine instance;

    public static StateMachine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StateMachine>();
            }
            return instance;
        }
        
    }

    private ObjectState currentState;
    private Transform objectTransform;

    private void Start()
    {
        objectTransform = transform;
        ChangeState(new IdleState(objectTransform));
    }

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(ObjectState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }
}
