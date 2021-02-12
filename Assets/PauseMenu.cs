using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPuased = false;
    public GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPuased = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPuased = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Start Scene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }
}