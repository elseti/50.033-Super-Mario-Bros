using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the star
public class StarPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource starAudio;
    public BoxCollider2D starMarioCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(){
        marioAnimator.Play("Star Mario Idle");
        this.gameObject.SetActive(false);
        starMarioCollider.enabled = true;
        starAudio.PlayOneShot(starAudio.clip);
        // be invincible for a time
    }
}
