using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float health = 100;
    [SerializeField] public float maxHealth= 100;

    [SerializeField] GameObject healthBarUI;
    Canvas healthBarUICanvas;
    [SerializeField] Slider slider;

    [SerializeField] GameObject deathEffect;

    void Awake() {
        healthBarUICanvas = healthBarUI.GetComponent<Canvas>();
    }

    void Start() {
        maxHealth += (WaveSystem.Instance.waveNum * WaveSystem.Instance.increaseHealthBy);
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update() {
        healthBarUI.transform.rotation = Quaternion.Euler(Vector3.up);

        slider.value = CalculateHealth();

        if (health < maxHealth) {
            healthBarUICanvas.enabled = true;
        }
    }

    float CalculateHealth() {
        return health / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other) {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        Explosion explosion = other.GetComponent<Explosion>();

        if (damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }

        if (explosion != null) {
            TakeDamage(explosion.damage);
        }
        
    }

    void TakeDamage(int damage) {

        health -= damage;

        if (health <= 0 ) {
            Die();
        }

    }

    void Die() {
        AiManager.Instance.RemoveEnemyAttacking(GetComponent<StateController>().GetInstanceID());
        GameObject explosionEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
