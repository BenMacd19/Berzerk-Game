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

    // IEnumerator DisableForce(StateController controller, PointEffector2D explosionForce) {
        
    //     controller.gameObject.GetComponent<Collider2D>().enabled = false;
    //     yield return new WaitForSeconds(0.1f);
    //     explosionForce.enabled = false;
    //     Destroy(controller.gameObject);
        
    // }

}
