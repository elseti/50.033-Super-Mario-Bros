using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxHit : MonoBehaviour
{

    public Animator questionBoxAnimator;
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
            print("hit question box");
            questionBoxAnimator.SetTrigger("hitQuestionBox");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // change rigidbody to static so it doesn't move
            hitDone = true;
        }

    }
}
