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
    public string nextLvl = "Level2";
    public AudioClip goalSound;

    public LayerMask whatIsGround;
    public Transform feet;
    bool grounded = false;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
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
        else if (other.gameObject.CompareTag("Goal"))
        {
            _audioSource.PlayOneShot(goalSound);
            SceneManager.LoadScene(nextLvl);
        }
    }

    private void OnColliderEnter2D(Collider2D other) {
        if (other.CompareTag("Ice")){
            speed = 8;
        }
        else if (other.CompareTag("Mud")){
            speed = 3;
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
