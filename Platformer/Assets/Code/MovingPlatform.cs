using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float spd = 0.015f;
    Rigidbody2D _rigidbody2D;
    public float startPos;
    public float endPos;
    int dir = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (spd == 0) {
            spd = 0.015f;
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(moveY());
    }

    IEnumerator moveY() {
        while (true) {
            if (transform.position.y <= endPos || transform.position.y >= startPos) {
            dir *= -1;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + (dir * spd));
            yield return new WaitForSeconds(.005f);
        }
        
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
