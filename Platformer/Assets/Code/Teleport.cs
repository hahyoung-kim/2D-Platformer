using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    
    //public GameObject player;
    public Transform teleportDest;

    void Start() {
    }

    public Vector2 getTeleportDest() {
        return teleportDest.transform.position;
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     print("collide");
    //     if (other.gameObject.tag == "Player")
    //     {
    //         print("player");
    //         player.transform.position = teleportDest.transform.position;
    //     }
    // }

}
