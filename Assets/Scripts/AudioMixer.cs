using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixer : MonoBehaviour
{
    // Start is called before the first frame update
    
    //Used to adjust the volume of the audio in the main menu
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        AudioMixer.SetFloat("volume", volume);
    }
}

