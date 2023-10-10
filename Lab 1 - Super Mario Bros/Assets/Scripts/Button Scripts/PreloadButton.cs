using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PreloadButton : MonoBehaviour
{
    public CanvasGroup preloadCanvas;
    public IntVariable gameScore;
    public TextMeshProUGUI highScore;

    void Start()
    {
        Time.timeScale = 0.0f;
        highScore.text = gameScore.previousHighestValue.ToString("D6");
        FadeOut();
    }

    void Update()
    {
        
    }

    public void BackToMainMenuButton(){
        print("go to main menu");
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    void FadeIn(){
        StartCoroutine(IncreaseAlphaImageCoroutine(preloadCanvas, 0.05f));
    }
    
    void FadeOut(){
        StartCoroutine(DecreaseAlphaImageCoroutine(preloadCanvas, 0.005f));   
    }

    private IEnumerator DecreaseAlphaImageCoroutine(CanvasGroup canvas, float multiplier){
        yield return new WaitForSecondsRealtime(2);

        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            canvas.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }


        canvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private IEnumerator IncreaseAlphaImageCoroutine(CanvasGroup canvas, float multiplier){
        for (float alpha = 1f; alpha >= -0.05f; alpha -= 0.05f)
        {
            canvas.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    void Wait(float seconds){
        StartCoroutine(WaitCoroutine(seconds));
    }

    private IEnumerator WaitCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
    }
}
