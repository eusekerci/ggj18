using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource MainMenuMusic;
    public AudioSource GameMusic;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayGameMusic()
    {
        MainMenuMusic.mute = true;
        GameMusic.mute = false;
    }
}
