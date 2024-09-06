using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectState : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void ExecuteState();
    public abstract void ExitState();
    
}
