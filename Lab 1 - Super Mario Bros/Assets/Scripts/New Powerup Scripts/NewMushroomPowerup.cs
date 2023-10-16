using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Attached to the star
public class NewMushroomPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource mushroomAudio;
    public BoxCollider2D mushroomMarioCollider;
    private bool mushroomDone = false;
    public UnityEvent startMushroomPowerup;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(){
        print("colided with mushroom");
        // todo - check if star mario is playing
        marioAnimator.Play("Super Mario Idle");
        mushroomMarioCollider.enabled = true;
        mushroomAudio.PlayOneShot(mushroomAudio.clip);
        this.gameObject.SetActive(false);

        startMushroomPowerup.Invoke();
    }

    public void GameRestart(){
        print("game restart in new mushroom powerup");
        this.gameObject.SetActive(true);
        // bgmAudio.Stop();
        // bgmAudio.PlayOneShot(bgmClip);
    }

}
