using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;

    private RaycastHit2D groundHit;

    private Collider2D collider;

    private void Start(){
        collider = GetComponent<Collider2D>();
    }

    public bool IsGrounded(){
        groundHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if(groundHit.collider != null){
            return true;
        }
        else{
            return false;
        }
    }
}
