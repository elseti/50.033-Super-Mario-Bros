using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // SO game score
    public IntVariable gameScore;

    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent destroyGoomba;
    public UnityEvent<int> startPowerup;
    public UnityEvent<int> endPowerup;
    public UnityEvent<int> resetPowerup;

    private int score = 0;

    void Start()
    {
        gameScore.Value = 0;
        gameStart.Invoke();
        Time.timeScale = 1.0f;

        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        gameScore.ApplyChange(increment);
        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }
    
    // HUD cleanup
    public void SceneSetup(Scene current, Scene next){
        gameStart.Invoke();
        SetScore(score);
    }

    // Star Powerup
    public void StartPowerup(int powerupType){
        print("starting powerup game manager" + powerupType);
        startPowerup.Invoke(powerupType);
    }

    public void EndPowerup(int powerupType){
        print("ending powerup game manager" + powerupType);
        endPowerup.Invoke(powerupType);
    }

    // only used in StarPowerup reset for now
    public void ResetPowerup(int powerupType){
        print("reseting powerup " + powerupType);
        resetPowerup.Invoke(powerupType);
    }
}