using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStats : ScriptableObject
{

    [HideInInspector] public float followDistance;
    [HideInInspector] public float explodeDistance;
    [HideInInspector] public GameObject explodeEffect;
    [HideInInspector] public float shootDistance;
    [HideInInspector] public float keepDistance;
    [HideInInspector] public float keepDistanceDegrees;
    [HideInInspector] public float turnSpeed;

    [HideInInspector] public GameObject bulletPrefab; 
    [HideInInspector] public float bulletForce;
    [HideInInspector] public float rateOfFire;

    public virtual void SetUpEnemy() {

    }

}
