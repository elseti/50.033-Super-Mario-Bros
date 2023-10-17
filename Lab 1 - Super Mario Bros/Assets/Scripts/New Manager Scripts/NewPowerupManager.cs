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
    // public UnityEvent endMushroomPowerup; // can implement in playercontoroller
    // public UnityEvent endFireFlowerPowerup;

    // public UnityEvent resetPowerup;
    

    // misc vars
    private bool starDone = false;
    private float starPowerupTime = 10f;
    private bool mushroomDone = false;
    private bool fireFlowerDone = false;

    void Start(){
        marioAnimator = GetComponent<Animator>();
        marioStateController = GetComponent<MarioStateController>();
    }

    void Update(){

    }

    // Invincible Mario (star)
    public void StartStarPowerup(){
        // bgm stuff
        starDone = true;
        WaitStar(starPowerupTime);
    }

    public void EndStarPowerup(){
        print("stardone false again");
        // bgmAudio.Stop();
        // bgmAudio.PlayOneShot(bgmAudio.clip);
        starMarioCollider.enabled = false; // disable big collider again
        endStarPowerup.Invoke();
    }

    // Super Mario (+ 1 life / mushroom)
    public void StartMushroomPowerup(){
        mushroomDone = true;
    }

    public void EndMushroomPowerup(){
        mushroomDone = false;
        starMarioCollider.enabled = false;
        print("powerup manager - mushroom done");
        FlickerMario();

    }


    // Fireflower (shoots fire)
    public void StartFireFlowerPowerup(){

    }

    public void EndFireFlowerPowerup(){

    }


    // Coroutines
    void WaitStar(float seconds){
        StartCoroutine(WaitStarCoroutine(seconds));
    }

    private IEnumerator WaitStarCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        print("coroutine done");
        // starDone = true;
        EndStarPowerup();
    }

    public void RestartPowerup(){
        marioAnimator.Play("Mario Idle");
        EndStarPowerup();
        starDone = false;
        mushroomDone = false;
        fireFlowerDone = false;
        starMarioCollider.enabled = false;
    }

    // invincible mario for 2 secs after super mario
    public void FlickerMario(){
        marioAnimator.Play("Mario Flicker");
        StartCoroutine(WaitFlickerMarioCoroutine(2f));
    }

    private IEnumerator WaitFlickerMarioCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        marioAnimator.Play("Mario Idle");
    }

    

    
}
