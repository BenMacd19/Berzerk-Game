using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Stats/FollowerStats")]
public class FollowerStats : EnemyStats
{
    [Header("Follower Stats")]
    public float followerFollowDistance = 30f;
    public float followerExplodeDistance = 2.5f;
    public GameObject followerExplodeEffect;
    public float followerTurnSpeed = 5f;

    public override void SetUpEnemy()
    {
        base.followDistance = followerFollowDistance;
        base.explodeDistance = followerExplodeDistance;
        base.explodeEffect = followerExplodeEffect;
        base.turnSpeed = followerTurnSpeed;
    }

}
