using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public SpawnState state = SpawnState.COUNTING;
    
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    [Header("Wave")]
    public int waveNum;   
    public GameObject[] enemies;
    public int numEnemies;

    public float spawnRate;
    public float spawnRadius;

    private float searchCountdown = 1;

    void Start() 
    {
        waveCountdown = timeBetweenWaves;    
    }

    void Update() {
        if (state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                WaveCompleted();
            } else {
                return;
            }
        }

        if (waveCountdown <= 0) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave());
            } 
        } else {
            waveCountdown -= Time.deltaTime;
        }      
    }

    void WaveCompleted() {

        waveNum ++;

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

    }

    bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            searchCountdown = 1f; 
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        } 
        return true;
    }

    IEnumerator SpawnWave() {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < waveNum; i++) {
            SpawnEnemy(enemies[Random.Range(0, enemies.Length)]);
            yield return new WaitForSecondsRealtime(spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(GameObject enemy) {
        Instantiate(enemy, Random.insideUnitCircle * spawnRadius, transform.rotation);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

}
