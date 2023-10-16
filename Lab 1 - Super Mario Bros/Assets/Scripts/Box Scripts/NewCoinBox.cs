using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCoinBox : MonoBehaviour
{
    public Animator brickBoxAnimator;
    public AudioSource boxAudioSource;
    
    private bool hitDone = false;

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
            brickBoxAnimator.SetTrigger("hitBrickBox");
            hitDone = true;
        }
    }

    public void GameRestart(){
        brickBoxAnimator.SetTrigger("restart");
        hitDone = false;
    }

    
}
