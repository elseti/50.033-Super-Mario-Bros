using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    public AudioSource bgmAudio;
    private Image image;

    public GameObject pauseImage;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseImage.SetActive(true);
            bgmAudio.Pause();
            image.sprite = playIcon;
        }
        else
        {
            bgmAudio.UnPause();
            pauseImage.SetActive(false);
            image.sprite = pauseIcon;
        }
    }
}
