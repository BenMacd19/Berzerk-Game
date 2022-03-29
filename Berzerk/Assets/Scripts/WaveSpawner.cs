using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    
    [System.Serializable]
    public class Wave 
    {
        public string name;
        public GameObject[] enemies;
        public int numEnemies;
        public float spawnRate;
    }

    public int waveNum;
    public Wave[] waves;
    private int nextWave = 0;
    
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1;

    public SpawnState state = SpawnState.COUNTING;

    public float spawnRadius;

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
                StartCoroutine(SpawnWave(waves[nextWave]));
            } 
        } else {
            waveCountdown -= Time.deltaTime;
        }      
    }

    void WaveCompleted() {

        waveNum ++;

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            return;
        } else {
            nextWave++;
        }
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

    IEnumerator SpawnWave(Wave wave) {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < waveNum; i++) {
            SpawnEnemy(wave.enemies[Random.Range(0, wave.enemies.Length)]);
            yield return new WaitForSecondsRealtime(wave.spawnRate);
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
