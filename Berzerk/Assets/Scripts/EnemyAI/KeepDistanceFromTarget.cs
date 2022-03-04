using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class KeepDistanceFromTarget : MonoBehaviour
{
    IAstarAI ai;

    [SerializeField] Transform target;
    [SerializeField] float distanceToKeep;

    [Tooltip("Degrees added to current ray to find suitable new ray")]
    [SerializeField] float degrees;

    void Start () {
        ai = GetComponent<IAstarAI>();
    }

    void Update () {
        
        // Whilst the player is near the enemy
        if (targetInDistance()) {
            
            // Ray coming out from behind the enemy
            RaycastHit2D behindRay = drawRay();

            // Debug ray coming out of the front of the enemy just for visualisation
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * distanceToKeep, Color.blue);

            // If the enemy has no behind itself to move this code will
            // find a suitable new location to move to
            if (rayHitWall(behindRay)) {

                // Create rays for the left and righ of the AI
                RaycastHit2D leftRay = drawAngledRay(degrees);
                RaycastHit2D rightRay = drawAngledRay(-degrees);
                
                // Find the end positions of both rays
                Vector2 leftEndPosition = transform.position + ((Quaternion.Euler(0, 0, degrees) * -transform.TransformDirection(Vector2.up)) * distanceToKeep);
                Vector2 rightEndPosition = transform.position + ((Quaternion.Euler(0, 0, -degrees) * -transform.TransformDirection(Vector2.up)) * distanceToKeep);

                GraphNode nearestNode;

                // Only left ray hits a wall
                if (rayHitWall(leftRay) && !rayHitWall(rightRay)) {
                    nearestNode = AstarPath.active.GetNearest(rightEndPosition, NNConstraint.Default).node;
                
                // Only right ray hits a wall
                } else if (rayHitWall(rightRay) && !rayHitWall(leftRay)) {
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
                ai.destination = (Vector3) nearestNode.position;;
                ai.SearchPath();

                // Return because we dont want the AI to move to its behind ray after it has detected a wall
                return;

            }

            // If the enemy is not going to touch a wall if they move backwards
            else {
                
                Vector2 rayBehindEndPosition = transform.position + (-transform.TransformDirection(Vector2.up) * distanceToKeep);

                GraphNode nearestNode = AstarPath.active.GetNearest(rayBehindEndPosition, NNConstraint.Default).node;

                ai.destination = (Vector3) nearestNode.position;
                ai.SearchPath();
            
            }
            
        }  
    }

    // Checks if a ray hits a wall
    bool rayHitWall(RaycastHit2D ray) {

        if (ray) {

	        Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.up) * distanceToKeep, Color.red);
            return true;
	    
        } else {
            return false;
        }

    }
    
    // Draws a ray behind the enemy
    RaycastHit2D drawRay() {
	
	    // Walls are on layer 8
        int wallLayerMask = 1 << 8;

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(transform.position, -transform.TransformDirection(Vector2.up), distanceToKeep, wallLayerMask);
        Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.up) * distanceToKeep, Color.red);

	    return ray;

    }

    // Draws an array at a certain angle aways from the behind ray
    RaycastHit2D drawAngledRay(float degrees) {
        
        Vector3 newDirection = -transform.TransformDirection(Vector2.up);
        //Quaternion newDirection = Quaternion.AngleAxis(30, -transform.TransformDirection(Vector2.up));

        // Walls are on layer 8
        int wallLayerMask = 1 << 8;

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(transform.position, (Quaternion.Euler(0, 0, degrees) * -transform.TransformDirection(Vector2.up)), distanceToKeep, wallLayerMask);
        Debug.DrawRay(transform.position, (Quaternion.Euler(0, 0, degrees) * -transform.TransformDirection(Vector2.up)) * distanceToKeep, Color.yellow);

        return ray;

    }

    // Checks if the target is within the distance from the enemy
    bool targetInDistance() {

        float distToTarget = Vector3.Distance(target.position, transform.position);

        if (distToTarget <= distanceToKeep) {
            return true;
        } else {
            return false;
        }
    }

}