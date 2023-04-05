using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int lives = 3;
    public int currWood = 0;
    public int currLvlWoods = 1;
    public int totalWoodsCollected = 3;
    public int totalGameWoods = 4;
    // public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI reduceHealthUI;
    public TextMeshProUGUI shootUI;
    public TextMeshProUGUI woodUI;
    public string currLvl = "Level1";
    public string gameOverLevel= "Level1";
    public GameObject explosion;
    public Image black;
    public Animator animator;
    AudioSource _audioSource;
    public AudioClip hitSound;
    public AudioClip teleSound;
    private bool canShoot = true;
    

    private void Awake()
    {
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        reduceHealthUI.gameObject.SetActive(false);  
        if (currLvl == "Level1") {
            canShoot = false;
            shootUI.gameObject.SetActive(false);  
        }
    }

    private void Start()
    {        
        // scoreUI.text = "score: " + score;
        _audioSource = GetComponent<AudioSource>();
        livesUI.text = "Lives: " + lives;  
        reduceHealthUI.text = "-1";
        woodUI.text = "Collected for Current Level: " + currWood + "/" + currLvlWoods + "\nTotal      Collected: " + totalWoodsCollected + "/" + totalGameWoods;  
    }

    public void loseLife(int lostLife){
        lives -= lostLife;

        // scoreUI.text = "score: " + score;
        livesUI.text = "Lives: " + lives;
        if (lives<=0){
            StartCoroutine(PlayerDeath());
        }
        ReduceHealthText("-" + lostLife);

    }

    public void incrLife(int add){
        lives += add;
        livesUI.text = "Lives: " + lives;
        ReduceHealthText("+" + add);
    }

    public void incrWood() {
        currWood += 1;
        totalWoodsCollected += 1;
        woodUI.text = "Collected for Current Level: " + currWood + "/" + currLvlWoods + "\nTotal      Collected: " + totalWoodsCollected + "/" + totalGameWoods;  
    }

    IEnumerator PlayerDeath() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        Destroy(player);
        animator.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a==1);
        SceneManager.LoadScene(gameOverLevel);
    }

    public IEnumerator Fade(){
        animator.SetBool("Fade", true);
        yield return new WaitUntil(()=>black.color.a==1);
    }

    public int GetLives() {
        return lives;
    }

    public void ReduceHealthText(string text){
        reduceHealthUI.text = text;
        reduceHealthUI.gameObject.SetActive(true);
        Invoke("SetInactive", .5f);
    }

    public void ShootText(){
        shootUI.gameObject.SetActive(true);
    }

    public void SetInactive(){
        reduceHealthUI.gameObject.SetActive(false);
    }

    public void EnemyDeathAudio() {
        _audioSource.PlayOneShot(hitSound);
    }

    public void TeleportAudio() {
        _audioSource.PlayOneShot(teleSound);
    }

    public void PickUpWand() {
        canShoot = true;
    }

    public bool PlayerShoot() {
        return canShoot;
    }

    public void Update()
    {
        // quit game is esc
#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
}
