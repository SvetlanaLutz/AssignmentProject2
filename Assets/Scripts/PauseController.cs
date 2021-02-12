using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public void PauseGame() 
    //Pausing the game
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ResumeGame() 
    //Resuming the game
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
