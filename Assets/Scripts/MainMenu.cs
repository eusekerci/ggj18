using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public enum SceneState
    {
        Start,
        Credits,
        Quit
    }

    public SceneState State;

    public Text CreditsText;
    public Text MainMenuText;
    private bool creditsOn;

    public Transform robot1;
    public Transform robot2;

    private int SceneNumber;
    public AudioManager AudioManager;

    void Start ()
    {
        State = SceneState.Start;
        creditsOn = false;
        SceneNumber = 0;
	}
	
	void Update ()
    {
        if (creditsOn)
        {
            if (Input.anyKeyDown)
            {
                creditsOn = false;
                CreditsText.gameObject.SetActive(false);
                MainMenuText.gameObject.SetActive(true);
                robot1.gameObject.SetActive(true);
                robot2.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (State == SceneState.Start)
                {
                    AudioManager.PlayGameMusic();
                    SceneManager.LoadScene(1);
                }
                else if (State == SceneState.Credits)
                {
                    CreditsText.gameObject.SetActive(true);
                    MainMenuText.gameObject.SetActive(false);
                    robot1.gameObject.SetActive(false);
                    robot2.gameObject.SetActive(false);
                    creditsOn = true;
                }
                else if (State == SceneState.Quit)
                {
                    Application.Quit();
                }
            }

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SceneNumber = (SceneNumber - 1) % 3;
                if (SceneNumber < 0)
                    SceneNumber += 3;
                State = (SceneState)SceneNumber;

            }
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SceneNumber = (SceneNumber + 1) % 3;
                State = (SceneState)SceneNumber;
            }

            if (State == SceneState.Start)
            {
                robot1.position = new Vector3(-3.27f, 1.18f, 0f);
                robot2.position = new Vector3(3.27f, 1.18f, 0f);
            }
            else if (State == SceneState.Credits)
            {
                robot1.position = new Vector3(-2.36f, 0.17f, 0f);
                robot2.position = new Vector3(2.36f, 0.17f, 0f);
            }
            else if (State == SceneState.Quit)
            {
                robot1.position = new Vector3(-1.54f, -0.9f, 0f);
                robot2.position = new Vector3(1.54f, -0.9f, 0f);
            }
        }


	}
}
