using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 650;
    int bulletSpeed = 600;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    GameManager _gameManager;
    public AudioClip hurtSound;
    public AudioClip shootSound;
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
        if (other.CompareTag("Enemy") || other.CompareTag("Boss")) {
            _audioSource.PlayOneShot(hurtSound);
            _gameManager.loseLife(1);
            StartCoroutine(FlashRed());
        }
        else if (other.CompareTag("Kill")){
            _gameManager.loseLife(1);
            _safeGround.WarpToSafeGround();
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            _audioSource.PlayOneShot(goalSound);
            _gameManager.Fade(nextLvl);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ice"){
            speed = 10;
        }
        else if (other.gameObject.tag == "Mud"){
            speed = 2;
        }
        else{
            speed = 5;
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
        if (Input.GetButtonDown("Fire1")){
            _audioSource.PlayOneShot(shootSound);
            // making a copy of bullet
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed, 0));
        }
    }
}
