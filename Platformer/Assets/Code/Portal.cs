using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool locked = true;
    // wood number
    public int woodNum = 0;
    // public string levelToLoad;
    // AudioSource _audioSource;
    // public AudioClip lockedSound;
    // public AudioClip unlockedSound;

    private void Start()
    {
       // _audioSource = GetComponent<AudioSource>();
    }

    public bool canUnlock()
    {
        print(PublicVars.hasWood[woodNum]);
        // If the door is unlocked, load the level
        if (!locked)
        {
            return true;
        }
        // If the door is locked, check if the player has the correct key
        
        if (PublicVars.hasWood[woodNum])
        {
            PublicVars.hasWood[woodNum] = false;
            return true;
            //_audioSource.PlayOneShot(unlockedSound);
            //SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            return false;
            //_audioSource.PlayOneShot(lockedSound);
        }
        
    }
}
