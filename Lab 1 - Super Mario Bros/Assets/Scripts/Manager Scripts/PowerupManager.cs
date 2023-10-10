using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the star
public class PowerupManager : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource bgmAudio;
    public AudioClip starBgm;
    // public AudioSource starAudio;
    public BoxCollider2D starMarioCollider;
    public float starPowerupTime = 15f;
    private bool starDone = false;

    void Awake(){
        GameManager.instance.startPowerup.AddListener(StartPowerup);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(starDone){
            starDone = false;
            StarPowerupEnd();
        }
    }

    public void StartPowerup(int powerupType){
        print("Power up type: " + powerupType);

        // star mario
        if(powerupType == 1){
            
            StarPowerupStart();
        }

        else if(powerupType == 2){

        }

        else if(powerupType == 3){

        }

        else if(powerupType == 4){

        }
    }


    // STAR MARIO POWERUP
    public void StarPowerupStart(){
        bgmAudio.Stop();
        bgmAudio.PlayOneShot(starBgm);
        Wait(starPowerupTime);
    }

    public void StarPowerupEnd(){
        print("stardone false again");
        bgmAudio.Stop();
        bgmAudio.PlayOneShot(bgmAudio.clip);
        marioAnimator.Play("Mario Idle");
        starMarioCollider.enabled = false; // disable big collider again
        GameManager.instance.EndPowerup(1);
    }

    void Wait(float seconds){
        StartCoroutine(WaitCoroutine(seconds));
    }

    private IEnumerator WaitCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        print("coroutine done");
        starDone = true;
    }

}
