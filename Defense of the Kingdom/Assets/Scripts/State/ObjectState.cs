using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ObjectState : MonoBehaviour
{
    public abstract void EnterState(ObjectStateManager objectStateManager);
    public abstract void UpdateState(ObjectStateManager objectStateManager);
    public abstract void ExitState(ObjectStateManager objectStateManager);
}
