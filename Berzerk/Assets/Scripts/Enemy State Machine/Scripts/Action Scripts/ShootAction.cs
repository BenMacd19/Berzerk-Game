using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Shoot")]
public class ShootAction : Action
{

    public override void Act(StateController controller)
    {

        switch (controller.aiLevel)
        {
            case 1:
                ShootLevel1(controller);
                break;
            case 2:
                ShootLevel2(controller);
                break;
            case 3:
                ShootLevel3(controller);
                break;
        }
        
    }

    private void ShootLevel1(StateController controller) {

        AiManager.Instance.AddEnemyAttacking(controller.GetInstanceID());

        LookAtTarget(controller);

        if (!CanAttack(controller)) return;

        controller.timer += Time.deltaTime;

        // Shoot at the rate of fire
        if(controller.timer > controller.currentEnemyStats.rateOfFire){
            controller.muzzleFlash.Play();
            GameObject bullet = Instantiate(controller.currentEnemyStats.bulletPrefab, controller.firePoint.position, controller.firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(RandomOffset(controller) * controller.currentEnemyStats.bulletForce, ForceMode2D.Impulse);
            controller.timer = 0;
        }
  
    }

    private void ShootLevel2(StateController controller) {

        AiManager.Instance.AddEnemyAttacking(controller.GetInstanceID());

        LookAtTarget(controller);

        if (!CanAttack(controller)) return;

        controller.timer += Time.deltaTime;

        // Shoot at the rate of fire
        if(controller.timer > controller.currentEnemyStats.rateOfFire){
            GameObject bullet = Instantiate(controller.currentEnemyStats.bulletPrefab, controller.firePoint.position, controller.firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(controller.firePoint.up * controller.currentEnemyStats.bulletForce, ForceMode2D.Impulse);
            controller.timer = 0;
        }
  
    }

    private void ShootLevel3(StateController controller) {

        Rigidbody2D targetRb = controller.target.GetComponent<Rigidbody2D>();

        if (InterceptTargetDirection(controller.target.position, controller.transform.position, targetRb.velocity, controller.currentEnemyStats.bulletForce, out var direction)) {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            controller.transform.rotation = Quaternion.Slerp (controller.transform.rotation, Quaternion.Euler (0, 0, angle), controller.currentEnemyStats.turnSpeed * Time.deltaTime);
        } else {
            LookAtTarget(controller);
        }

        controller.timer += Time.deltaTime;

        // Shoot at the rate of fire
        if(controller.timer > controller.currentEnemyStats.rateOfFire){
            controller.muzzleFlash.Play();
            GameObject bullet = Instantiate(controller.currentEnemyStats.bulletPrefab, controller.firePoint.position, controller.firePoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(controller.firePoint.up * controller.currentEnemyStats.bulletForce, ForceMode2D.Impulse);
            controller.timer = 0;
        }
  
    }

    private bool CanAttack(StateController controller) {
        if (AiManager.Instance.enemiesAttacking.Contains(controller.GetInstanceID())) {
            return true;
        } else {
            return false;
        }
    }

    private void LookAtTarget(StateController controller) {
        Vector3 direction = controller.target.position - controller.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        controller.transform.rotation = Quaternion.Slerp (controller.transform.rotation, Quaternion.Euler (0, 0, angle), controller.currentEnemyStats.turnSpeed * Time.deltaTime);
    }

    private Vector3 RandomOffset(StateController controller) {
        Vector3 randomPosition = Random.insideUnitCircle * 3f;
        Vector3 aimOffset = controller.target.position + randomPosition;
        return (aimOffset - controller.transform.position).normalized;
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