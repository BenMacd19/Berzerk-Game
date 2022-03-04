using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "PluggableAI/Actions/StopMoving")]
public class StopMovingAction : Action
{
    public override void Act(StateController controller)
    {
        StopMoving(controller);
    }

    private void StopMoving(StateController controller) {

        // Stop moving
        GraphNode nearestNode = AstarPath.active.GetNearest(controller.transform.position, NNConstraint.Default).node;
        controller.ai.destination = (Vector3) nearestNode.position;

    }

}
