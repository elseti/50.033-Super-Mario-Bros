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
        GameManager.instance.endPowerup.AddListener(ResetPowerup);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(starDone){
            StarPowerupEnd();
        }
    }

    public void StartPowerup(int powerupType){
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

    public void EndPowerup(int powerupType){
        // star mario
        if(powerupType == 1){
            StarPowerupEnd();
        }

        else if(powerupType == 2){

        }

        else if(powerupType == 3){

        }

        else if(powerupType == 4){

        }
    }

    public void ResetPowerup(int powerupType){
        // star mario
        if(powerupType == 1){
            StarPowerupReset();
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
        starDone = false;
        print("stardone false again");
        bgmAudio.Stop();
        bgmAudio.PlayOneShot(bgmAudio.clip);
        marioAnimator.Play("Mario Idle");
        starMarioCollider.enabled = false; // disable big collider again
        GameManager.instance.EndPowerup(1);
    }

    // only difference is not calling GameManager's EndPowerup to avoid infinite loop
    public void StarPowerupReset(){
        starDone = false;
        print("stardone false again");
        bgmAudio.Stop();
        bgmAudio.PlayOneShot(bgmAudio.clip);
        marioAnimator.Play("Mario Idle");
        starMarioCollider.enabled = false; // disable big collider again
        StopCoroutine("WaitCoroutine");
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
