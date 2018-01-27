using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameOverUI;
    public GameObject gameUI;
    public Text scoreText;
    public Text gameOverText;

    public int highScore;
    

    public enum State
    {
        Menu,
        Game,
        GameOver,
    }
    public State state;

    public EnemySpawner enemySpawner;

    public GameObject borderPrefab;

    public void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        state = State.Game;

        float xMid = (Utils.xMin + Utils.xMax) * 0.5f;
        float xScale = Utils.xMax - Utils.xMin;

        float yMid = (Utils.yMin + Utils.yMax) * 0.5f;
        float yScale = Utils.yMax - Utils.yMin;

        GameObject border_0 = GameObject.Instantiate(borderPrefab);
        border_0.name = "Border_0";
        border_0.transform.position = new Vector3(xMid, Utils.yMin);
        border_0.transform.localScale = new Vector3(xScale, 1, 1);

        GameObject border_1 = GameObject.Instantiate(borderPrefab);
        border_1.name = "Border_1";
        border_1.transform.position = new Vector3(xMid, Utils.yMax);
        border_1.transform.localScale = new Vector3(xScale, 1, 1);

        GameObject border_2 = GameObject.Instantiate(borderPrefab);
        border_2.name = "Border_2";
        border_2.transform.position = new Vector3(Utils.xMin, yMid);
        border_2.transform.localScale = new Vector3(1, yScale, 1);

        GameObject border_3 = GameObject.Instantiate(borderPrefab);
        border_3.name = "Border_3";
        border_3.transform.position = new Vector3(Utils.xMax, yMid);
        border_3.transform.localScale = new Vector3(1, yScale, 1);
    }

    public void Update()
    {
        if(state == State.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
            SwitchState(State.Game);
        }
        scoreText.text = "" + Mathf.RoundToInt(enemySpawner.totalScoreCollected * 10);
    }

    public void OnGameOver()
    {
        int playerScore = Mathf.RoundToInt(enemySpawner.totalScoreCollected * 10);
        int prevHighScore = highScore;

        gameOverText.text = "Your score is " + playerScore + "\n";
        if (playerScore > highScore)
        {
            gameOverText.text += "New High Score!\n";
            gameOverText.text += "You still suck, though.\n";
            highScore = playerScore;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
        else
        {
            gameOverText.text += "Noobs. You suck.\n";
        }
        gameOverText.text += "Press Space to Restart";

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
