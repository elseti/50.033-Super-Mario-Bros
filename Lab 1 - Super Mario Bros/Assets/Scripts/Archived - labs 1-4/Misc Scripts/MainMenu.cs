using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject blackPanel;
    public AudioSource audioSource;
    public IntVariable gameScore;
    public TextMeshProUGUI highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = gameScore.previousHighestValue.ToString("D6");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton(){
        audioSource.PlayOneShot(audioSource.clip);
        blackPanel.SetActive(true);
        Fade();
        
    }

    public void ResetButton(){
        gameScore.ResetHighestValue();
        highScore.text = gameScore.previousHighestValue.ToString("D6");
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    void Fade(){
        StartCoroutine(IncreaseAlphaImageCoroutine(blackPanel, 0.1f));
    }

    private IEnumerator DecreaseAlphaImageCoroutine(GameObject gameObject, float multiplier){
        Color color = blackPanel.transform.GetComponent<Image>().color;
        while(color.a > 0){
            color.a -= 0.005f;
            blackPanel.transform.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f * multiplier);
        }
    }

    private IEnumerator IncreaseAlphaImageCoroutine(GameObject gameObject, float multiplier){
        Color color = blackPanel.transform.GetComponent<Image>().color;
        while(color.a < 1){
            color.a += 0.005f;
            blackPanel.transform.GetComponent<Image>().color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForSeconds(0.01f * multiplier);
        }
        // load world 1-1
        SceneManager.LoadSceneAsync("World 1-1", LoadSceneMode.Single);
        Wait(2f);
    }

    void Wait(float seconds){
        StartCoroutine(WaitCoroutine(seconds));
    }

    private IEnumerator WaitCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
    }
}
