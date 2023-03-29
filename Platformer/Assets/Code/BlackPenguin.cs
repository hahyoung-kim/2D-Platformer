using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPenguin : MonoBehaviour
{
    GameManager _gameManager;
    AudioSource _audioSource;
    public AudioClip hitSound;
    
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireBall")){
            //_gameManager.AddScore(pointValue);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
