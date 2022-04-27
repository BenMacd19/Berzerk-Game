using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/StopMoving")]
public class StopMovingAction : Action
{
    public override void Act(StateController controller)
    {
        StopMoving(controller);
    }

    private void StopMoving(StateController controller) {
        NavMeshHit myNavHit;
        if(NavMesh.SamplePosition(controller.transform.position, out myNavHit, 100 , -1)) {
            controller.agent.destination = myNavHit.position;
        }
    }

}
