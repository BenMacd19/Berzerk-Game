using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ExplodeDecision")]
public class ExplodeDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInExplodeDistance = TargetInExplodeDistance(controller);
        return targetInExplodeDistance;
    }

    private bool TargetInExplodeDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);
        
        if (distanceToTarget < controller.currentEnemyStats.explodeDistance) {
            return true;
        } else {
            return false;
        }

    }
}