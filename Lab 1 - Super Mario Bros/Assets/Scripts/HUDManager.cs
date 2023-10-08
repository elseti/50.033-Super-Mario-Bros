using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : Singleton<HUDManager>
{
    // HUD - Heads-up Display

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreGameOverText;
    public GameObject gameOverPanel;

    void Awake(){
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }

    void Start()
    {
        
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

    public void SetScore(int score){
        scoreText.text = "Score: " + score;
        scoreGameOverText.text = "Score: " + score;
    }

    public void GameOver(){
        gameOverPanel.SetActive(true);
    }
}
