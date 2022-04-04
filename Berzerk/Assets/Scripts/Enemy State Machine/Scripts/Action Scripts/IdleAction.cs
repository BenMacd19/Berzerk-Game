using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : Action
{

    public override void Act(StateController controller)
    {
        Idle(controller);
    }

    private void Idle(StateController controller) {

        // Stop moving
        GraphNode nearestNode = AstarPath.active.GetNearest(controller.transform.position, NNConstraint.Default).node;
        //controller.ai.destination = (Vector3) nearestNode.position;

        // Look at target
        Vector3 direction = Vector3.up;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        controller.transform.rotation = Quaternion.Slerp (controller.transform.rotation, Quaternion.Euler (0, 0, angle), controller.currentEnemyStats.turnSpeed * Time.deltaTime);

        //controller.transform.rotation = Quaternion.Euler(controller.transform.up);
  
    }

}
