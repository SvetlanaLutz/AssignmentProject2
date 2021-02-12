using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuTwo : MonoBehaviour {

    public void PlayGame ()
    {
        Enemy.nameColor = null;
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}