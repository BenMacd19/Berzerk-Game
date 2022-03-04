using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ShooterShootDecision")]
public class ShooterShootDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetInShootDistance = TargetInShootDistance(controller);
        return targetInShootDistance;
    }

    private bool TargetInShootDistance(StateController controller) {
        
        float distanceToTarget = Vector3.Distance(controller.target.position, controller.transform.position);

        if (distanceToTarget < controller.currentEnemyStats.shootDistance && distanceToTarget > controller.currentEnemyStats.keepDistance) {
            return true;
        } else {
            AiManager.Instance.RemoveEnemyAttacking(controller.GetInstanceID());
            return false;
        }

    }
}