using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/FollowerFollowDecision")]
public class FollowerFollowDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInFollowDistance = TargetInFollowDistance(controller);
        return targetInFollowDistance;
    }

    private bool TargetInFollowDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);
        
        if (distanceToTarget < controller.currentEnemyStats.followDistance && distanceToTarget > controller.currentEnemyStats.explodeDistance) {
            return true;
        } else {
            return false;
        }

    }
}