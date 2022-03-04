using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D other) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2085f);
        Destroy(gameObject);
    }
}
