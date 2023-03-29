using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int lives = 3;
    // public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI livesUI;
    public TextMeshProUGUI reduceHealthUI;
    public string currLvl = "Level1";
    public string gameOverLevel= "Level1";
    public GameObject explosion;
    public Image black;
    public Animator animator;
    

    private void Awake()
    {
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        reduceHealthUI.gameObject.SetActive(false);  
    }

    private void Start()
    {        
        // scoreUI.text = "score: " + score;
        livesUI.text = "Lives: " + lives;  
    }

    public void loseLife(int lostLife){
        lives -= lostLife;

        // scoreUI.text = "score: " + score;
        livesUI.text = "Lives: " + lives;
        if (lives<=0){
            StartCoroutine(PlayerDeath());
        }
        ReduceHealthText();

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

    public void ReduceHealthText(){
        reduceHealthUI.gameObject.SetActive(true);
        Invoke("SetInactive", .5f);
    }

    public void SetInactive(){
        reduceHealthUI.gameObject.SetActive(false);
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
