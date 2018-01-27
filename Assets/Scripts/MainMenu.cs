using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public enum SceneState
    {
        MainMenu,
        Game
    }

    public SceneState State;
    public AudioSource MainMenuMusic;
    public AudioSource GameMusic;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start ()
    {
        State = SceneState.MainMenu;
	}
	
	void Update ()
    {
		if(State == SceneState.MainMenu)
        {
            MainMenuMusic.mute = false;
            GameMusic.mute = true;
        }
        else
        {
            MainMenuMusic.mute = true;
            GameMusic.mute = false;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            State = SceneState.Game;
            SceneManager.LoadScene(1);
        }
	}
}
