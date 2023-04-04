using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 650;
    int bulletSpeed = 600;
    private Rigidbody2D _rigidbody;

    private Animator _animator; 

    private SpriteRenderer _renderer;
    GameManager _gameManager;
    public AudioClip hurtSound;
    public AudioClip shootSound;
    public AudioClip lockedSound;
    public AudioClip pickupSound;
    AudioSource _audioSource;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public string currLvl = "Level1";
    public string nextLvl = "Level2";
    public AudioClip goalSound;

    public LayerMask whatIsGround;
    public Transform feet;
    bool grounded = false;
    private SaveGround _safeGround;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator> ();
        _renderer = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _safeGround = GameObject.FindGameObjectWithTag("Player").GetComponent<SaveGround>();
    }

    IEnumerator FlashRed() {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //print("trigger " + other.gameObject.name + " " + other.gameObject.tag);
        if (other.CompareTag("Enemy") || other.CompareTag("Boss")) {
            _audioSource.PlayOneShot(hurtSound);
            _gameManager.loseLife(1);
            StartCoroutine(FlashRed());
        }
        else if (other.CompareTag("Kill")){
            _gameManager.loseLife(_gameManager.GetLives()); // lose all lives
        }
        else if (other.CompareTag("Med")){
            _audioSource.PlayOneShot(pickupSound);
            Destroy(other.gameObject);
            _gameManager.incrLife(10);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            if (other.GetComponent<Portal>().canUnlock()) {
                _audioSource.PlayOneShot(goalSound);
                _gameManager.Fade();
                SceneManager.LoadScene(nextLvl);
            } else {
                _audioSource.PlayOneShot(lockedSound);
            }
            
        } else if (other.gameObject.CompareTag("Wood"))
        {
            // Wood name is "WoodX" where X is the wood number
            int woodNum = Int32.Parse(other.gameObject.name.Substring(4));
            Destroy(other.gameObject);
            _gameManager.incrWood();
            PublicVars.hasWood[woodNum] = true;
            // play sound
            _audioSource.PlayOneShot(pickupSound);
        } else if (other.CompareTag("Teleport")){
            transform.position = other.GetComponent<Teleport>().getTeleportDest();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ice"){
            
            speed = 10;
            print("ice " + speed);
        }
        else if (other.gameObject.tag == "Mud"){
            speed = 2;
            print("mud " + speed);
        }
        else{
            speed = 5;
            print("normal " + speed);
        }
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed,_rigidbody.velocity.y);

        float xScale = transform.localScale.x;
        if ((xSpeed < 0 && xScale > 0) || (xSpeed > 0 && xScale < 1))
        {
            transform.localScale *= new Vector2(-1,1);
        }
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position, .3f, whatIsGround);
        _animator.SetBool("Grounded", grounded);
        
        if(Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetButtonDown("Fire1")){
            //_animator.SetTrigger("Wand");
             _animator.SetBool("Shooting", true);
            _audioSource.PlayOneShot(shootSound);
            // making a copy of bullet
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            float xScale = transform.localScale.x;
            if (xScale >= 0) {
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
            } else {
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletSpeed, 0));
            }
        } 
        else{
            _animator.SetBool("Shooting", false);
            //_animator.ResetTrigger("Wand");
        }
    }
}
