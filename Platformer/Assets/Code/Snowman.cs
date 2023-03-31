using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    //int spd = 100;
    public Sprite[] spriteArray;
    Rigidbody2D _rigidbody2D;
    public GameObject explosion;
    //int ptVal = 1000;
    GameManager _gameManager;
    AudioSource _audioSource;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    int bulletSpd = 300;
    public AudioClip shootSound;
    public AudioClip hitSound;
    SpriteRenderer _renderer;
    float bossHealth = 100;
    int spriteInd = 0;
    public GameObject player;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();

        while (_gameManager.GetLives() > 0) {
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletSpd, 0));
            if(Vector3.Distance(player.transform.position, transform.position) < 10){
                _audioSource.PlayOneShot(shootSound);
            }
            yield return new WaitForSeconds(Random.Range(1,2));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("FireBall")) {
            _audioSource.PlayOneShot(hitSound);
            StartCoroutine(FlashRed());
            BossTakeDmg(2.5f);
        }
    }

    private void BossTakeDmg(float dmg) {
        bossHealth -= dmg;
        if (bossHealth % 12.5f == 0) {
            spriteInd += 1;
            if (spriteInd  >= spriteArray.Length) {
                spriteInd = 0;
            }
            _renderer.sprite = spriteArray[spriteInd]; 
        }
        
        if (bossHealth <= 0) {
            //_gameManager.AddScore(ptVal);
            _gameManager.EnemyDeathAudio();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed() {
        _renderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _renderer.color = Color.white;
    }

    void Update() {
        if (_gameManager.GetLives() <= 0) {
            Destroy(gameObject);
        }
    }
}
