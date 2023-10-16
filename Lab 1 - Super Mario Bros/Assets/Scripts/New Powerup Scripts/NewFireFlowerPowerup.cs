using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Attached to the star
public class NewFireFlowerPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource fireFlowerAudio;
    public BoxCollider2D fireFlowerMarioCollider;
    private bool fireFlowermDone = false;
    public UnityEvent startFireFlowerPowerup;

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
        marioAnimator.Play("Super Mario Idle");
        fireFlowerMarioCollider.enabled = true;
        fireFlowerAudio.PlayOneShot(fireFlowerAudio.clip);
        this.gameObject.SetActive(false);

        startFireFlowerPowerup.Invoke();
    }

    public void GameRestart(){
        print("game restart in new fireflower powerup");
        this.gameObject.SetActive(true);
        // bgmAudio.Stop();
        // bgmAudio.PlayOneShot(bgmClip);
    }

}
