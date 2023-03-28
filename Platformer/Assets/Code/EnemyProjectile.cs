using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireBall")){
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.CompareTag("Kill") || other.CompareTag("Player")){
            Destroy(gameObject);
        } 

    }

    void Update() {
        
    }
}
