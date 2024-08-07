using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : ObjectState
{

    public override void EnterState(ObjectStateManager objectStateManager)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(ObjectStateManager objectStateManager)
    {
        float distance = Vector2.Distance(objectStateManager.transform.position, objectStateManager.target.position);

        if (distance <= objectStateManager.detectionDistance)
        {
            objectStateManager.ChangeState(objectStateManager.FightState);
        }
        else
        {
            // Логика поиска целей (например, перемещение в определенном направлении)
        }
    }

    public override void ExitState(ObjectStateManager objectStateManager)
    {
        throw new System.NotImplementedException();
    }
}
