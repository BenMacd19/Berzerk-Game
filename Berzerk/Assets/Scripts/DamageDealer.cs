using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 10;
    [SerializeField] GameObject hitEffect;
    
    public int GetDamage() {
        return damage;
    }

    public void Hit() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2085f);
        Destroy(gameObject);
    }

}
