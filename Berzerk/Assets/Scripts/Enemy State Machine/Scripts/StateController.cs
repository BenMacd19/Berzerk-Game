using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StateController : MonoBehaviour {

    [HideInInspector] public IAstarAI ai;
    [HideInInspector] public int aiLevel;
    [HideInInspector] public int wallLayerMask = 1 << 8;
    [HideInInspector] public Health enemyHealth;
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
        ai = GetComponent<IAstarAI>();
        target = FindObjectOfType<Player>().transform;

        // Check if the enemy has a weapon
        Transform tmp = transform.Find("Weapon");
        if (tmp == null) {
            // No weapon
        } else {
            firePoint = tmp.Find("FirePoint");
            muzzleFlash = firePoint.Find("Enemy Muzzle Flash").gameObject.GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        // Get current AI level
        this.aiLevel = AiManager.Instance.aiLevel;

        // Set the enemies stats to the AI level
        currentEnemyStats = enemyStats[this.aiLevel - 1];
        currentEnemyStats.SetUpEnemy();

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
        if (currentState != null) 
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere (transform.position, 2.5f);
        }
    }
    
}