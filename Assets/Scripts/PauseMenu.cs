using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        //Resuming the game
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //This is a code of pausing the game using esc key(We didn't use it)
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    public void PauseGame()
    //Pausing the game
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    //
    public void ResumeGame()
    //Resuming the game
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    //Load to the start scene
    {
        SceneManager.LoadScene("Start scene");
    }
}
