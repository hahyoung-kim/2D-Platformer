using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame(){
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame(){
    #if !UNITY_WEBGL
        Application.Quit();
    #else
        Application.OpenURL("about:blank");
    #endif
    }

    public void RestartGame(){
        SceneManager.LoadScene("Title");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HTP-pg1");
    }

    public void Previous(){
        SceneManager.LoadScene("HTP-pg1");
    }

    public void Next(){
        SceneManager.LoadScene("HTP-pg2");
    }

    public void Back(){
        SceneManager.LoadScene("Title");
    }
}
