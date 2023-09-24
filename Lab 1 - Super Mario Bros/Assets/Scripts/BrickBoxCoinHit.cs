using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBoxCoinHit : MonoBehaviour
{
    public Animator brickBoxAnimator;
    public AudioSource boxAudioSource;
    public bool hitDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(){
        if(!hitDone){
            boxAudioSource.PlayOneShot(boxAudioSource.clip);
            print("hit brick box");
            brickBoxAnimator.SetTrigger("hitBrickBox");
            hitDone = true;
        }

    }
}
