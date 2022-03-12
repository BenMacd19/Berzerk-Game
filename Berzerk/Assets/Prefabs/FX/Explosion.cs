using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    PointEffector2D explosionForce;
    Collider2D explosionCollider;

    void Awake() {
        explosionForce = GetComponent<PointEffector2D>();
        explosionCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        this.StartCoroutine(DisableForce(explosionForce));
        CinemachineShake.Instance.ShakeCamera(7.5f, 0.2f);
    }

    IEnumerator DisableForce(PointEffector2D explosionForce) {
        yield return new WaitForSeconds(0.1f);
        explosionCollider.enabled = false;
        explosionForce.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
