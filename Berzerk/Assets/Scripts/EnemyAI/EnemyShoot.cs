using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    Rigidbody2D rb;
    CircleCollider2D shootRadius;

    [SerializeField] Transform target;
    [SerializeField] Transform firePoint;
    [SerializeField] float turnSpeed = 5;

    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float rateOfFire = 0.25f;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        shootRadius = GetComponent<CircleCollider2D>();
    }

    // Whenever the player is inside the radius of the enemy
    // the enemy will look at the player
    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            LookAtPlayer();
        }
    }

    // When the player enters the radius of the enemy then enemy will
    // repeate the fire function at the firerate specified above
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            InvokeRepeating("Fire", 0.25f, rateOfFire);
        }
    }

    // When the player leaves the enemy radius the enemy
    // stops shooting at the player
    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
           CancelInvoke("Fire");
        }  
    }

    // Instantiates a new bullet object to be fired
    void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Rotates the Enemy toward the players direction
    // Rotation is smoothed using "Quaternion.Slerp"
    void LookAtPlayer() {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, angle), turnSpeed * Time.deltaTime);
    }

}
