using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewPowerupManager : MonoBehaviour
{
    private Animator marioAnimator;
    private MarioStateController marioStateController;
    public BoxCollider2D starMarioCollider;

    // UnityEvents 
    public UnityEvent endStarPowerup;
    // public UnityEvent resetPowerup;
    

    // misc vars
    private bool starDone = false;
    private float starPowerupTime = 10f;

    void Start(){
        marioAnimator = GetComponent<Animator>();
        marioStateController = GetComponent<MarioStateController>();
    }

    void Update(){

    }

    // Invincible Mario (star)
    public void StartStarPowerup(){
        // bgm stuff
        Wait(starPowerupTime);
    }

    public void EndStarPowerup(){
        print("stardone false again");
        // bgmAudio.Stop();
        // bgmAudio.PlayOneShot(bgmAudio.clip);
        marioAnimator.Play("Mario Idle");
        starMarioCollider.enabled = false; // disable big collider again
        endStarPowerup.Invoke();
    }

    // Super Mario (+ 1 life / mushroom)
    public void StartMushroomPowerup(){
        
    }


    // Coroutines
    void Wait(float seconds){
        StartCoroutine(WaitCoroutine(seconds));
    }

    private IEnumerator WaitCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        print("coroutine done");
        // starDone = true;
        EndStarPowerup();
    }

    public void RestartPowerup(){
        EndStarPowerup();
    }

    
}
