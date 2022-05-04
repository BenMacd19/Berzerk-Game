using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Follow")]
public class FollowAction : Action
{
    public override void Act(StateController controller)
    {

        switch (controller.aiLevel)
        {
            case 1:
                FollowLevel1(controller);
                break;
            case 2:
                FollowLevel2(controller);
                break;
            case 3:
                FollowLevel3(controller);
                break;
        }
        
    }

    private void FollowLevel1(StateController controller) {
        LookAtTarget(controller);
        controller.agent.SetDestination(controller.target.position);
    }

    private void FollowLevel2(StateController controller) {
        // LookAtTarget(controller);
        // Rigidbody2D targetRb = controller.target.GetComponent<Rigidbody2D>();
        // Vector3 targetDir = controller.target.position - controller.transform.position;
        // float lookAhead = targetDir.magnitude / (controller.agent.speed + targetRb.velocity.magnitude);
        // controller.agent.SetDestination(controller.target.transform.position + controller.target.up * lookAhead * 5);
        LookAtTarget(controller);
        controller.agent.SetDestination(controller.target.position);
    }

    private void FollowLevel3(StateController controller) {
        // LookAtTarget(controller);
        // Rigidbody2D targetRb = controller.target.GetComponent<Rigidbody2D>();
        // Vector3 targetDir = controller.target.position - controller.transform.position;
        // float lookAhead = targetDir.magnitude / (controller.agent.speed + targetRb.velocity.magnitude);
        // controller.agent.SetDestination(controller.target.transform.position + controller.target.up * lookAhead * 5);
        LookAtTarget(controller);
        controller.agent.SetDestination(controller.target.position);
    }

    private void LookAtTarget(StateController controller) {
        Vector3 direction = controller.target.position - controller.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        controller.transform.rotation = Quaternion.Slerp (controller.transform.rotation, Quaternion.Euler (0, 0, angle), controller.currentEnemyStats.turnSpeed * Time.deltaTime);
    }

    // Predicts the targets location based on its current velocity
    private bool InterceptTargetDirection(Vector2 a, Vector2 b, Vector2 vA, float sB, out Vector2 result) {

        var aToB = b - a;
        var dC = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, vA) * Mathf.Deg2Rad;
        var sA = vA.magnitude;
        var r = sA / sB;

        if (SolveQuadratic(1-r*r, 2*r*dC*Mathf.Cos(alpha), -(dC*dC), out var root1, out var root2) == 0) {
            result = Vector2.zero;
            return false;
        }

        var dA = Mathf.Max(root1, root2);
        var t = dA / sB;
        var c = a + vA * t;
        result = (c - b).normalized;
        return true;

    }

    private int SolveQuadratic(float a, float b, float c, out float root1, out float root2) {

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0) {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        } else {
            root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            return discriminant > 0 ? 2 : 1;
        }

    }

}
