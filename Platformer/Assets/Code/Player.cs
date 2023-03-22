using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 650;
    private Rigidbody2D _rigidbody;

    public LayerMask whatIsGround;
    public Transform feet;
    bool grounded = false;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed,_rigidbody.velocity.y);
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position, .3f, whatIsGround);
        if(Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
}
