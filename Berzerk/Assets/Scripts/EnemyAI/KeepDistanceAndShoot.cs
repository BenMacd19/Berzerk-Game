using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class KeepDistanceAndShoot : MonoBehaviour
{
    IAstarAI ai;

    enum state {Follow, Sentry, KeepDistance, Idle}

    [SerializeField] Transform target;
    [SerializeField] float followDistance;
    [SerializeField] float shootDistance;
    [SerializeField] float keepDistance;
    [SerializeField] float keepDistanceDegrees;
    [SerializeField] float turnSpeed;

    [Header("Bullet")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float rateOfFire = 0.25f;

    bool isFiring = false;
    int wallLayerMask = 1 << 8;

    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    void Update()
    {
        
        state currentState = GetState();

        if (currentState == state.Follow) {
            LookAtPlayer();  
            ai.destination = (Vector3) target.position;
            //ai.SearchPath();

        } else {
            GraphNode nearestNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
            ai.destination = (Vector3) nearestNode.position;
            //ai.SearchPath();
        }

        if (currentState == state.Sentry) {

            LookAtPlayer();

            if (!isFiring) {
                InvokeRepeating("Fire", 0.25f, rateOfFire);
                isFiring = true;
            }
            
        } else {
            CancelInvoke("Fire");
            isFiring = false;
        }

        if (currentState == state.KeepDistance) {
            LookAtPlayer();
            KeepDistanceFromTheTarget();
        }

    }

    // Returns the current state that the enemy is in
    // depending on how far way the enemy is from the target
    // and weather or not they can see the target
    state GetState() {

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray = Physics2D.Raycast(transform.position, (target.position - transform.position), distanceToTarget, wallLayerMask);
        Debug.DrawRay(transform.position, (target.position - transform.position), Color.white);

        // If the enemy cannot see the target
        if(ray) {
            return state.Idle;
        }  
        
        if (distanceToTarget <= followDistance && distanceToTarget > shootDistance) {
            Debug.DrawRay(transform.position, - (transform.position - target.position), Color.green);
            return state.Follow;
        } 
        
        if (distanceToTarget <= shootDistance && distanceToTarget > keepDistance) {
            Debug.DrawRay(transform.position, - (transform.position - target.position), Color.blue);
            return state.Sentry;
        } 
        
        if (distanceToTarget <= keepDistance) {
            Debug.DrawRay(transform.position, - (transform.position - target.position), Color.red);
            return state.KeepDistance;
        }

        else {
            return state.Idle;
        }

    }

    void KeepDistanceFromTheTarget() {

        // Ray coming out from behind the enemy
        RaycastHit2D behindRay = DrawRayBehind();

        // Debug ray coming out of the front of the enemy just for visualisation
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * keepDistance, Color.blue);

        // If the enemy has no behind itself to move this code will
        // find a suitable new location to move to
        if (RayHitWall(behindRay)) {

            // Create rays for the left and righ of the AI
            RaycastHit2D leftRay = DrawAngledRay(-keepDistanceDegrees);
            RaycastHit2D rightRay = DrawAngledRay(keepDistanceDegrees);
            
            // Find the end positions of both rays
            Vector2 leftEndPosition = transform.position + ((Quaternion.Euler(0, 0, -keepDistanceDegrees) * -transform.TransformDirection(Vector2.up)) * keepDistance);
            Vector2 rightEndPosition = transform.position + ((Quaternion.Euler(0, 0, keepDistanceDegrees) * -transform.TransformDirection(Vector2.up)) * keepDistance);

            GraphNode nearestNode;

            // Only left ray hits a wall
            if (RayHitWall(leftRay) && !RayHitWall(rightRay)) {
                nearestNode = AstarPath.active.GetNearest(rightEndPosition, NNConstraint.Default).node;
            
            // Only right ray hits a wall
            } else if (RayHitWall(rightRay) && !RayHitWall(leftRay)) {
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
            ai.destination = (Vector3) nearestNode.position;
            //ai.SearchPath();

            // Return because we dont want the AI to move to its behind ray after it has detected a wall
            return;

        }

        // If the enemy is not going to touch a wall if they move backwards
        else {
            
            Vector2 rayBehindEndPosition = transform.position + (-transform.TransformDirection(Vector2.up) * keepDistance);

            GraphNode nearestNode = AstarPath.active.GetNearest(rayBehindEndPosition, NNConstraint.Default).node;

            Debug.Log("MOVING");

            ai.destination = (Vector3) nearestNode.position;
            //ai.SearchPath();
        
        }

    }

    // Checks if a ray hits a wall
    bool RayHitWall(RaycastHit2D ray) {

        if (ray) {

            Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.up) * keepDistance, Color.red);
            return true;
        
        } else {
            return false;
        }

    }

    // Draws a ray behind the enemy
    RaycastHit2D DrawRayBehind() {

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(transform.position, -transform.TransformDirection(Vector2.up), keepDistance, wallLayerMask);
        Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.up) * keepDistance, Color.red);

        return ray;

    }

    // Draws an array at a certain angle aways from the behind ray
    RaycastHit2D DrawAngledRay(float degrees) {
        
        Vector3 newDirection = -transform.TransformDirection(Vector2.up);
        //Quaternion newDirection = Quaternion.AngleAxis(30, -transform.TransformDirection(Vector2.up));

        // Origin, Direction, Distance, Layermask to collid with
        RaycastHit2D ray= Physics2D.Raycast(transform.position, (Quaternion.Euler(0, 0, degrees) * -transform.TransformDirection(Vector2.up)), keepDistance, wallLayerMask);
        Debug.DrawRay(transform.position, (Quaternion.Euler(0, 0, degrees) * -transform.TransformDirection(Vector2.up)) * keepDistance, Color.yellow);

        return ray;

    }

    // Instantiates a new bullet object to be fired
    void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Rotates the Enemy toward the players direction
    // Rotation is smoothed using "Quaternion.Slerp"
    void LookAtPlayer() {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, angle), turnSpeed * Time.deltaTime);
    }

}
            
