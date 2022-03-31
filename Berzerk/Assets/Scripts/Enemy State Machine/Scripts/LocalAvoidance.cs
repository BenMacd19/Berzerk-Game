using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAvoidance : MonoBehaviour
{

    Collider2D myCollider;
    Rigidbody2D rb;
    [SerializeField] float avoidanceRadius;
    //Vector3 avg;

    private void Awake() {
        myCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        List<Vector3> positions = new List<Vector3>();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
    
        for (var i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].tag == "Enemy" && hitColliders[i] != myCollider) {
                positions.Add(hitColliders[i].transform.position);
            } 
        }
        Debug.Log(positions.Count);
        if (positions.Count == 0) return;

        Vector3 avg = GetMeanVector(positions);
        Vector3 direction = transform.position - avg;
        rb.AddForce(direction * 5f);
    }

    Vector3 GetMeanVector(List<Vector3> positions) {
        if (positions.Count == 0) {
            return Vector3.zero;
        }
        
        float x = 0f;
        float y = 0f;
        float z = 0f;

        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
        //Gizmos.DrawWireCube(avg, new Vector3(0.5f,0.5f,0.5f));
    }

}
