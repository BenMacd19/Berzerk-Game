using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ShooterKeepDistanceDecision")]
public class ShooterKeepDistanceDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInKeepDistance = TargetInKeepDistance(controller);
        return targetInKeepDistance;
    }

    private bool TargetInKeepDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);
        
        if (distanceToTarget < controller.currentEnemyStats.keepDistance) {
            return true;
        } else {
            return false;
        }

    }
}