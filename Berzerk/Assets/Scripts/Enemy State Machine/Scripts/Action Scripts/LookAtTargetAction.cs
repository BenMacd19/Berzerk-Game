using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "PluggableAI/Actions/LookAtTarget")]
public class LookAtTargetAction : Action
{
    public override void Act(StateController controller)
    {
        LookAtTarget(controller);
    }

    private void LookAtTarget(StateController controller) {

        // Look at target
        Vector3 direction = controller.target.position - controller.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        controller.transform.rotation = Quaternion.Slerp (controller.transform.rotation, Quaternion.Euler (0, 0, angle), controller.currentEnemyStats.turnSpeed * Time.deltaTime);

    }

}
