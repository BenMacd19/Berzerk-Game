using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Stats/SluggerStats")]
public class SluggerStats : EnemyStats
{
    [Header("Slugger Stats")]
    public float sluggerFollowDistance = 30f;
    public float sluggerShootDistance = 20f;
    public float sluggerStopMovingDistance = 10f;
    public float sluggerKeepDistanceDegrees = 60f;
    public float sluggerTurnSpeed = 10f;

    [Header("Bullet")]
    public GameObject sluggerBulletPrefab;
    public float sluggerBulletForce = 20f;
    public float sluggerRateOfFire = 0.25f;

    public override void SetUpEnemy()
    {
        base.followDistance = sluggerFollowDistance;
        base.shootDistance = sluggerShootDistance;
        base.keepDistance = sluggerStopMovingDistance;
        base.keepDistanceDegrees = sluggerKeepDistanceDegrees;
        base.turnSpeed = sluggerTurnSpeed;

        base.bulletPrefab = sluggerBulletPrefab;
        base.bulletForce = sluggerBulletForce;
        base.rateOfFire = sluggerRateOfFire;
    }

}
