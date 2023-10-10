using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the star
public class StarPowerup : MonoBehaviour
{

    public Animator marioAnimator;
    public AudioSource starAudio;
    public BoxCollider2D starMarioCollider;
    private bool starDone = false;

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
        starMarioCollider.enabled = true;
        starAudio.PlayOneShot(starAudio.clip);
        GameManager.instance.StartPowerup(1); // call star powerup in GameManager
        this.gameObject.SetActive(false);
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
