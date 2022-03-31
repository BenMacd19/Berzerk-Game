using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] GameObject deathEffect;

    public Slider slider;

    public GameManager gameManager;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();
        if (health <= 0) {
            GameObject explosionEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameManager.Invoke("GameOver", 0);
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
    }

}
