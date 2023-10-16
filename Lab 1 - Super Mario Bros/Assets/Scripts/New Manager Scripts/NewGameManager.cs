using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    // SO game score
    public IntVariable highScore;
    public IntVariable gameScore;

    // events
    public UnityEvent gameStart;
    public UnityEvent updateScore;
    private int score = 0;

    void Start()
    {
        gameScore.Value = 0; // idt i shud do dis
        score = 0;
        
        Time.timeScale = 1.0f;

        gameStart.Invoke();
        updateScore.Invoke();

        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;
    }

    void Update()
    {

    }

    // HUD cleanup
    public void SceneSetup(Scene current, Scene next){
        gameStart.Invoke();
        // SetScore(score);
    }

    public void PauseGame(){
        Time.timeScale = 0.0f;
    }

    public void ResumeGame(){
        Time.timeScale = 1.0f;
    }

    public void GameOver(){
        Time.timeScale = 0.0f;
    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        // SetScore(score);
        // gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void RequestPowerupEffect(IPowerup i){
        // TODO
    }

    public void IncreaseScore(int increment){
        score += increment;
        gameScore.ApplyChange(increment); 
        highScore.SetValue(gameScore.Value);
        updateScore.Invoke();
    }

    public void SetScore(){
        updateScore.Invoke();
    }


    /// ******* ///
    // public void IncreaseScore(int increment)
    // {
    //     score += increment;
    //     gameScore.ApplyChange(increment);
    //     SetScore(score);
    // }

    // public void SetScore(int score)
    // {
    //     scoreChange.Invoke(score);
    // }

    // public void GameOver()
    // {
    //     Time.timeScale = 0.0f;
    //     gameOver.Invoke();
    // }
    
    

    // // Star Powerup
    // public void StartPowerup(int powerupType){
    //     print("starting powerup game manager" + powerupType);
    //     startPowerup.Invoke(powerupType);
    // }

    // public void EndPowerup(int powerupType){
    //     print("ending powerup game manager" + powerupType);
    //     endPowerup.Invoke(powerupType);
    // }

    // // only used in StarPowerup reset for now
    // public void ResetPowerup(int powerupType){
    //     print("reseting powerup " + powerupType);
    //     resetPowerup.Invoke(powerupType);
    // }
}