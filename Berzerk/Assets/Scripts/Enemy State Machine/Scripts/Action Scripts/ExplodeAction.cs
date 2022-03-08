using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Explode")]
public class ExplodeAction : Action
{

    public override void Act(StateController controller)
    {
        Explode(controller);
    }

    private void Explode(StateController controller) { 
        GameObject explosionEffect = Instantiate(controller.currentEnemyStats.explodeEffect, controller.transform.position, Quaternion.identity);
        Destroy(controller.gameObject);
    }

}
