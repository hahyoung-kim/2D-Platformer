using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    float speed = 3f;
    public GameObject player;
    public bool evil = false;
    GameManager _gameManager;
    public GameObject explosion;
    
    // Use this for initialization
    void Start () {
        if (evil) {
            speed = 5f;
        }
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        //StartCoroutine(FollowPlayer());
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        float xScale = transform.localScale.x;
        if (_gameManager.GetLives() > 0) {
            if ((xScale > 0 && player.transform.position.x > transform.position.x) || (player.transform.position.x < transform.position.x && xScale < 1))
            {
                transform.localScale *= new Vector2(-1,1);
            }
            if(Vector3.Distance(player.transform.position, transform.position) < 20){
                Vector3 displacement = player.transform.position -transform.position;
                displacement = displacement.normalized;
                if (Vector2.Distance (player.transform.position, transform.position) > 1.0f) {
                    transform.position += (displacement * speed * Time.deltaTime);
                }
            }
        }
        //_animator.SetFloat("Speed", Mathf.Abs(xSpeed));
    }

    
    // Update is called once per frame
    void Update () {
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireBall") && !evil){
            //_gameManager.AddScore(pointValue);
            _gameManager.EnemyDeathAudio();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
