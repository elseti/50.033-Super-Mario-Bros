using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Attached to the star
public class NewStarPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource starAudio;
    public AudioSource bgmAudio;
    public AudioClip bgmClip;
    public BoxCollider2D starMarioCollider;
    private bool starDone = false;
    public UnityEvent startStarPowerup;

    void Awake(){  
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(){
        marioAnimator.Play("Star Mario Idle");
        starMarioCollider.enabled = true;
        starAudio.PlayOneShot(starAudio.clip);
        this.gameObject.SetActive(false);

        startStarPowerup.Invoke();
    }

    public void GameRestart(){
        print("game restart in new star powerup");
        this.gameObject.SetActive(true);
        // bgmAudio.Stop();
        // bgmAudio.PlayOneShot(bgmClip);
    }

}
