using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the star
public class StarPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource starAudio;
    public AudioSource bgmAudio;
    public AudioClip bgmClip;
    public BoxCollider2D starMarioCollider;
    private bool starDone = false;

    void Awake(){  
        GameManager.instance.resetPowerup.AddListener(ResetStar);
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
        GameManager.instance.StartPowerup(1); // call star powerup in GameManager
        this.gameObject.SetActive(false);
    }

    void ResetStar(int powerupType){
        if(powerupType == 1){
            this.gameObject.SetActive(true);
            bgmAudio.Stop();
            bgmAudio.PlayOneShot(bgmClip);
        }
    }

}
