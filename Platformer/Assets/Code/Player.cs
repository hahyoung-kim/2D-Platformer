using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 650;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    //GameManager _gameManager;
    public AudioClip hurtSound;
    AudioSource _audioSource;

    public LayerMask whatIsGround;
    public Transform feet;
    bool grounded = false;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        //_gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    IEnumerator FlashRed() {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            _audioSource.PlayOneShot(hurtSound);
            //_gameManager.loseLife(1);
            StartCoroutine(FlashRed());
        }
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
