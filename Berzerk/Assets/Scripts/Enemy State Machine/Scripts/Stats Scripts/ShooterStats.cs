using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Stats/ShooterStats")]
public class ShooterStats : EnemyStats
{
    [Header("Shooter Stats")]
    public float shooterFollowDistance = 30f;
    public float shooterShootDistance = 20f;
    public float shooterKeepDistance = 10f;
    public float shooterKeepDistanceDegrees = 60f;
    public float shooterTurnSpeed = 10f;

    [Header("Bullet")]
    public GameObject shooterBulletPrefab;
    public float shooterBulletForce = 20f;
    public float shooterRateOfFire = 0.25f;

    public override void SetUpEnemy()
    {
        base.followDistance = shooterFollowDistance;
        base.shootDistance = shooterShootDistance;
        base.keepDistance = shooterKeepDistance;
        base.keepDistanceDegrees = shooterKeepDistanceDegrees;
        base.turnSpeed = shooterTurnSpeed;

        base.bulletPrefab = shooterBulletPrefab;
        base.bulletForce = shooterBulletForce;
        base.rateOfFire = shooterRateOfFire;
    }

}
