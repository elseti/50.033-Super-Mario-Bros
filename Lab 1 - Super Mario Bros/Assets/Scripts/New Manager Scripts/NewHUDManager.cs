using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewHUDManager : MonoBehaviour
{
    // HUD - Heads-up Display

    // SO game score
    public IntVariable highScore;
    public IntVariable gameScore;
    public TextMeshProUGUI highscoreText;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreGameOverText;
    public GameObject gameOverPanel;
    public GameObject preloadPanel;

    void Awake(){
        // GameManager.instance.gameStart.AddListener(GameStart);
        // GameManager.instance.gameOver.AddListener(GameOver);
        // GameManager.instance.gameRestart.AddListener(GameStart);
        // GameManager.instance.scoreChange.AddListener(SetScore);
    }

    void Start()
    {
        preloadPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart(){
        scoreGameOverText.text = "Score: 0";
        scoreText.text = "Score: 0";
        gameOverPanel.SetActive(false);
    }

    // not sure if simple / int event listener...
    public void SetScore(){
        scoreText.text = "Score: " + gameScore.Value;
        scoreGameOverText.text = "Score: " + gameScore.Value;
    }

    public void GameOver(){
        highscoreText.text = "High Score: " + highScore.previousHighestValue.ToString("D6"); // D6 -6 zero digits
        gameOverPanel.SetActive(true);
    }
}
