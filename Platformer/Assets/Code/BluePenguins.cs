using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePenguins : MonoBehaviour
{
    //GameManager _gameManager;
    AudioSource _audioSource;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public int bulletSpd = 300;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public float secsMin = 1;
    public float secsMax = 1;
    //public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_gameManager = GameObject.FindObjectOfType<GameManager>();
        StartCoroutine(throwSnowBalls());
        
    }

    IEnumerator throwSnowBalls() {
        //while (_gameManager.GetLives() > 0) {
        while (true) {
            GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-bulletSpd, 0));
            _audioSource.PlayOneShot(shootSound);
            yield return new WaitForSeconds(Random.Range(secsMin, secsMax));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireBall")){
            //_gameManager.AddScore(pointValue);
            //Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
