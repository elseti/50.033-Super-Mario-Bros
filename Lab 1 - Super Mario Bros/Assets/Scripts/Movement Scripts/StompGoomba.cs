using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompGoomba : MonoBehaviour
{

    public Animator goombaAnim;
    public AudioSource goombaAudio;
    private bool starPowerup = false;
    // private bool hasCollided = false;

    // Start is called before the first frame update

    void Awake(){
        GameManager.instance.startPowerup.AddListener(StartPowerup);
        GameManager.instance.endPowerup.AddListener(EndPowerup);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if(starPowerup){
            // TODO: kill goomba
        }
        else{
            if(col.gameObject.CompareTag("Player")){
                print("in player");
                goombaAudio.PlayOneShot(goombaAudio.clip);
                goombaAnim.Play("Goomba Squish");
            }
        }
        
        
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
