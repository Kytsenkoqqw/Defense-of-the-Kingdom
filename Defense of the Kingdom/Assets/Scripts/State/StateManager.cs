using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private ObjectState _currentState;

    public void ChangeState(ObjectState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.EnterState();
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.ExecuteState();
        }
    }
}
