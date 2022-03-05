using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float health = 100;
    [SerializeField] public float maxHealth= 100;

    [SerializeField] GameObject healthBarUI;
    [SerializeField] Slider slider;

    void Start() {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update() {
        healthBarUI.transform.rotation = Quaternion.Euler(Vector3.up);

        slider.value = CalculateHealth();

        if (health < maxHealth) {
            healthBarUI.SetActive(true);
        }
    }

    float CalculateHealth() {
        return health / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other) {

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
        
    }

    void TakeDamage(int damage) {

        health -= damage;

        // if (health <= 0 ) {
        //     Die();
        // }

    }

    void Die() {
        Destroy(gameObject);
    }

}
