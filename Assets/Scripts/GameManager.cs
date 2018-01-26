using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameOverUI;
    public GameObject gameUI;

    public enum State
    {
        Menu,
        Game,
        GameOver,
    }
    public State state;

    public EnemySpawner enemySpawner;

    public void Start()
    {
        state = State.Game;
    }

    public void Update()
    {
        if(state == State.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchState(State.Game);
        }
    }

    public void OnGameOver()
    {
        SwitchState(State.GameOver);
    }

    void SwitchState(State newState)
    {
        menuUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameUI.SetActive(false);

        if(newState == State.Menu)
        {
            menuUI.SetActive(true);
        }
        else if(newState == State.Game)
        {
            gameUI.SetActive(true);
        }
        else
        {
            gameOverUI.SetActive(true);
        }

        state = newState;

        if (newState != State.Game)
        {
            enemySpawner.ClearAll();
        }
    }
}
