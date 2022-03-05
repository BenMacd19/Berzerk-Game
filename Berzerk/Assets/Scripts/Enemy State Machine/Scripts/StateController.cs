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
    public Transform target;
    public Transform firePoint;

    void Awake () 
    {
        ai = GetComponent<IAstarAI>();
        enemyHealth = GetComponent<Health>();
    }

    void Update()
    {
        this.aiLevel = AiManager.Instance.aiLevel;
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