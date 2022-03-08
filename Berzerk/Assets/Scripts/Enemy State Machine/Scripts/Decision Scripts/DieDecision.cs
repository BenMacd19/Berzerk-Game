using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/DieDecision")]
public class DieDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInExplodeDistance = HasNoHealth(controller);
        return targetInExplodeDistance;
    }

    private bool HasNoHealth(StateController controller) {
        
        if (controller.enemyHealth.health <= 0) {
            return true;
        } else {
            return false;
        }

    }
}