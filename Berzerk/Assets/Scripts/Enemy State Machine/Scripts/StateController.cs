using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public int aiLevel;
    [HideInInspector] public int wallLayerMask = 1 << 8;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public float timer;
    
    [Header("AI Level Stats")]
    public EnemyStats[] enemyStats;
    [HideInInspector]public EnemyStats currentEnemyStats;

    [Header("States")]
    public State currentState;
    public State remainState;

    [Header("Enemy")]
    #nullable enable
    [HideInInspector] public Transform? target;
    [HideInInspector] public Transform? firePoint;
    [HideInInspector] public ParticleSystem? muzzleFlash;
    #nullable disable

    void Awake () 
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        rb = GetComponent<Rigidbody2D>();

        target = FindObjectOfType<PlayerMovement>().transform;

        // Check if the enemy has a weapon
        Transform tmp = transform.Find("Weapon");
        if (tmp == null) {
            // No weapon
        } else {
            firePoint = tmp.Find("FirePoint");
            muzzleFlash = firePoint.Find("Enemy Muzzle Flash").gameObject.GetComponent<ParticleSystem>();
        }
    }

    void FixedUpdate()
    {
        // Get current AI level
        this.aiLevel = AiManager.Instance.aiLevel;
        Debug.Log(aiLevel);

        // Set the enemies stats to the AI level
        currentEnemyStats = enemyStats[this.aiLevel - 1];
        currentEnemyStats.SetUpEnemy();
        agent.speed = currentEnemyStats.moveSpeed;

        switch (aiLevel)
        {
            case 1:
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                break;
            case 2:
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
                break;
            case 3:
                agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
                break;
        }

        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState) 
        {
            currentState = nextState;
        }
    }

    void OnDrawGizmos()
    {
        // if (currentState != null) 
        // {
        //     Gizmos.color = currentState.sceneGizmoColor;
        //     Gizmos.DrawWireSphere (transform.position, 2.5f);
        // }

        Debug.DrawRay(transform.position, firePoint.up * 12, Color.red);

        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere (target.transform.position, 2.75f);

    }
    
}