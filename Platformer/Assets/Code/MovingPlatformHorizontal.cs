using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHorizontal : MonoBehaviour
{
    public float spd = 0.1f;
    Rigidbody2D _rigidbody2D;
    public float startPos;
    public float endPos;
    int dir = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (spd == 0) {
            spd = 0.1f;
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //StartCoroutine(moveX());
    }

    // IEnumerator moveX() {
    //     while (true) {
    //         if (transform.position.x <= endPos || transform.position.x >= startPos) {
    //         dir *= -1;
    //         }
    //         transform.position = new Vector2(transform.position.x + (dir * spd), transform.position.y);
    //         yield return new WaitForSeconds(.005f);
    //     }
        
    // }

    void FixedUpdate() {
        if (transform.position.x <= endPos || transform.position.x >= startPos) {
            dir *= -1;
        }
        transform.position = new Vector2(transform.position.x + (dir * spd), transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(gameObject.transform,true);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.parent = null;
    }

}
