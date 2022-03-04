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

        // Ray coming out from behind the enemy
        RaycastHit2D behindRay = DrawRayBehind(controller);

        // Debug ray coming out of the front of the enemy just for visualisation
        Debug.DrawRay(controller.transform.position, controller.transform.TransformDirection(Vector2.up) * controller.currentEnemyStats.keepDistance, Color.blue);

        // If the enemy has no behind itself to move this code will
        // find a suitable new location to move to
        if (RayHitWall(controller, behindRay)) {

            // Create rays for the left and righ of the AI
            RaycastHit2D leftRay = DrawAngledRay(controller, -controller.currentEnemyStats.keepDistanceDegrees);
            RaycastHit2D rightRay = DrawAngledRay(controller, controller.currentEnemyStats.keepDistanceDegrees);
            
            // Find the end positions of both rays
            Vector2 leftEndPosition = controller.transform.position + ((Quaternion.Euler(0, 0, -controller.currentEnemyStats.keepDistanceDegrees) * -controller.transform.TransformDirection(Vector2.up)) * controller.currentEnemyStats.keepDistance);
            Vector2 rightEndPosition = controller.transform.position + ((Quaternion.Euler(0, 0, controller.currentEnemyStats.keepDistanceDegrees) * -controller.transform.TransformDirection(Vector2.up)) * controller.currentEnemyStats.keepDistance);

            GraphNode nearestNode;

            // Only left ray hits a wall
            if (RayHitWall(controller, leftRay) && !RayHitWall(controller, rightRay)) {
                nearestNode = AstarPath.active.GetNearest(rightEndPosition, NNConstraint.Default).node;
            
            // Only right ray hits a wall
            } else if (RayHitWall(controller, rightRay) && !RayHitWall(controller, leftRay)) {
                nearestNode = AstarPath.active.GetNearest(leftEndPosition, NNConstraint.Default).node; 
            
            // Both rays hit a wall (AI stuck in corner)
            } else {
                
                // Move to the ray of longest length
                if (leftRay.distance > rightRay.distance) {
                    nearestNode = AstarPath.active.GetNearest(leftEndPosition, NNConstraint.Default).node;
                } else {
                    nearestNode = AstarPath.active.GetNearest(rightEndPosition, NNConstraint.Default).node;
                }

            }

            // Move to the nearest safe distance away from the player
            controller.ai.destination = (Vector3) nearestNode.position;
            //ai.SearchPath();

            // Return because we dont want the AI to move to its behind ray after it has detected a wall
            return;

        }

        // If the enemy is not going to touch a wall if they move backwards
        else {
            
            Vector2 rayBehindEndPosition = controller.transform.position + (-controller.transform.TransformDirection(Vector2.up) * controller.currentEnemyStats.keepDistance);

            GraphNode nearestNode = AstarPath.active.GetNearest(rayBehindEndPosition, NNConstraint.Default).node;
            
            controller.ai.destination = (Vector3) nearestNode.position;
            //ai.SearchPath();
        
        }

    }

    // Checks if a ray hits a wall
    bool RayHitWall(StateController controller, RaycastHit2D ray) {

        if (ray) {

            Debug.DrawRay(controller.transform.position, -controller.transform.TransformDirection(Vector2.up) * controller.currentEnemyStats.keepDistance, Color.red);
            return true;
        
        } else {
            return false;
        }

    }

    // Draws a ray behind the enemy
    RaycastHit2D DrawRayBehind(StateController controller) {

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(controller.transform.position, -controller.transform.TransformDirection(Vector2.up), controller.currentEnemyStats.keepDistance, controller.wallLayerMask);
        Debug.DrawRay(controller.transform.position, -controller.transform.TransformDirection(Vector2.up) * controller.currentEnemyStats.keepDistance, Color.red);

        return ray;

    }

    // Draws an array at a certain angle aways from the behind ray
    RaycastHit2D DrawAngledRay(StateController controller, float degrees) {
        
        Vector3 newDirection = -controller.transform.TransformDirection(Vector2.up);
        //Quaternion newDirection = Quaternion.AngleAxis(30, -transform.TransformDirection(Vector2.up));

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(controller.transform.position, (Quaternion.Euler(0, 0, degrees) * -controller.transform.TransformDirection(Vector2.up)), controller.currentEnemyStats.keepDistance, controller.wallLayerMask);
        Debug.DrawRay(controller.transform.position, (Quaternion.Euler(0, 0, degrees) * -controller.transform.TransformDirection(Vector2.up)) * controller.currentEnemyStats.keepDistance, Color.yellow);

        return ray;

    }

}
