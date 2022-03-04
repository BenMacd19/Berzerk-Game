using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{

    public static AiManager Instance { get; private set; }

    [SerializeField] int maxEnemiesAttacking = 2;
    public List<int> enemiesAttacking;
    [Range(1,3)] public int aiLevel = 1;

    void Awake() {
        Instance = this;
    }

    public void AddEnemyAttacking(int enemyId) {
        if (enemiesAttacking.Count == maxEnemiesAttacking || enemiesAttacking.Contains(enemyId)) {
            return;
        }
        enemiesAttacking.Add(enemyId);
    }

    public void RemoveEnemyAttacking(int enemyId) {
        if (!enemiesAttacking.Contains(enemyId)) {
            return;
        }
        enemiesAttacking.Remove(enemyId);
    }

}
