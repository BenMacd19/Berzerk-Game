using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ShooterFollowDecision")]
public class ShooterFollowDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInFollowDistance = TargetInFollowDistance(controller);
        return targetInFollowDistance;
    }

    private bool TargetInFollowDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);
        
        if (distanceToTarget < controller.currentEnemyStats.followDistance && distanceToTarget > controller.currentEnemyStats.shootDistance) {
            return true;
        } else {
            return false;
        }

    }
}