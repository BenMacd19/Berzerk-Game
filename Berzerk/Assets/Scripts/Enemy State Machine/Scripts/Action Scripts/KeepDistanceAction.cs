using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "PluggableAI/Actions/KeepDistance")]
public class KeepDistanceAction : Action
{
    public override void Act(StateController controller)
    {
        KeepDistance(controller);
    }

    private void KeepDistance(StateController controller) {
        controller.rb.AddForce(-controller.transform.up * controller.currentEnemyStats.moveSpeed);
    }
}