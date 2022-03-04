using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FollowTargetAtDistance : MonoBehaviour
{

    IAstarAI ai;

    [SerializeField] Transform target;
    [SerializeField] float followTargetInDistance;
    [SerializeField] float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetInFollowDistance()) {
            LookAtPlayer();
            ai.destination = (Vector3) target.position;
            ai.SearchPath();
        } else {
            ai.destination = (Vector3) transform.position;
            ai.SearchPath();
        }
    }

    // Checks if the target is within the distance from the enemy
    bool targetInFollowDistance() {

        float distToTarget = Vector3.Distance(target.position, transform.position);

        if (distToTarget <= followTargetInDistance) {
            Debug.DrawRay(transform.position, - (transform.position - target.position), Color.red);
            return true;
        } else {
            return false;
        }
    }

    // Rotates the Enemy toward the players direction
    // Rotation is smoothed using "Quaternion.Slerp"
    void LookAtPlayer() {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, angle), turnSpeed * Time.deltaTime);
    }

}
