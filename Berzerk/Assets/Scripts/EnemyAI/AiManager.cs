using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{

    public static AiManager Instance { get; private set; }

    [SerializeField] int maxEnemiesAttacking = 2;
    int maxEnemiesAttackingCopy;
    public List<int> enemiesAttacking;
    [Range(1,3)] public int aiLevel = 1;  

    void Awake() {
        Instance = this;
    }

    void Start() {
        maxEnemiesAttackingCopy = maxEnemiesAttacking;
    }

    void Update() {
        
        if (maxEnemiesAttackingCopy != maxEnemiesAttacking) {
            RemoveAllEnemiesAttacking();
        }

        maxEnemiesAttackingCopy = maxEnemiesAttacking;
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

    void RemoveAllEnemiesAttacking() {
        enemiesAttacking.Clear();
    }

}
