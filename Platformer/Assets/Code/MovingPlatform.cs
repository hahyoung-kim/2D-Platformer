using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    int spd;
    Rigidbody2D _rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        spd = 100;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(0, spd));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -3.6f) {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0, spd));
        } else if (transform.position.y >= 5.8f) {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(0, -spd));
        }
    }
}
