using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewStompGoomba : MonoBehaviour
{

    public Animator goombaAnim;
    public AudioSource goombaAudio;
    private bool starPowerup = false;
    
    public UnityEvent increaseScore;
    
    // private bool hasCollided = false;

    // Start is called before the first frame update

    void Awake(){
        // GameManager.instance.startPowerup.AddListener(StartPowerup);
        // GameManager.instance.endPowerup.AddListener(EndPowerup);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        print("collided in goomba");
        if(starPowerup){
            goombaAudio.PlayOneShot(goombaAudio.clip);
            goombaAudio.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            goombaAnim.Play("Goomba Topple");
        }
        else{
            if(col.gameObject.CompareTag("Player")){
                print("in player");
                goombaAudio.PlayOneShot(goombaAudio.clip);
                goombaAnim.Play("Goomba Squish");
            }
        }
        increaseScore.Invoke();
    }

    public void StartPowerup(int powerupType){
        // star powerup
        if(powerupType == 1){
            starPowerup = true;
        }
    }

    public void EndPowerup(int powerupType){
        // star powerup
        if(powerupType == 1){
            starPowerup = false;
        }
    }
}
