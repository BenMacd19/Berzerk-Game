using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/IdleDecision")]
public class IdleDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetNotInFollowDistance = TargetNotInFollowDistance(controller);
        return targetNotInFollowDistance;
    }

    private bool TargetNotInFollowDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);
        
        if (distanceToTarget > controller.currentEnemyStats.followDistance) {
            return true;
        } else {
            return false;
        }

    }
}