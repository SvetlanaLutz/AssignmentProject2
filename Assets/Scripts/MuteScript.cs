using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    private bool isMuted;
    void Start()
    {
    isMuted = false;
    }
        


public void MutePressed()
        {
            isMuted = !isMuted;
            AudioListener.pause = !AudioListener.pause;
        }
}