using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void ReloadGame()
    {
        GameController.score = 0;
        Enemy.isMoveToFire = false;
        Enemy.isAlpha = true;
        Enemy.isDead = false;
        Arrow.isColor = true;
        GameController.isFirstScore = true;
        GameController.isTakeOneHeartAway = false;
        Bow.isArrow = true;
        Bow.isMouseDown = true;
        Bow.nameColor = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
