using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {
        this.StartCoroutine(DisableForce(GetComponent<PointEffector2D>()));
        CinemachineShake.Instance.ShakeCamera(7.5f, 0.2f);
    }

    IEnumerator DisableForce(PointEffector2D explosionForce) {
        yield return new WaitForSeconds(0.1f);
        explosionForce.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
