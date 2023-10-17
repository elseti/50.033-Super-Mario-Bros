using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class NewPlayerController : MonoBehaviour 
{
    public GameConstants gameConstants;
    
    public float speed = 25;
    private Rigidbody2D marioBody;
    public float maxSpeed = 35;
    public float upSpeed = 24;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Animator marioAnimator;

    // Audio
    public AudioSource marioAudio;
    public AudioSource marioDeath;

    [System.NonSerialized]
    public bool alive = true;
    public float deathImpulse = 30;

    public Transform gameCamera;    

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8); // bitwise OR between layers; i.e. becomes 1110 0000 i think?
    
    // InputSystem
    private bool moving = false;
    private  bool jumpedState = false;

    

    // UnityEvents invoke
    public UnityEvent gameOver;

    // Powerup variables
    public UnityEvent endMushroomPowerup;
    public UnityEvent endFireFlowerPowerup;
    public UnityEvent fireFlowerShoot;

    private bool mushroomPowerup = false;
    private bool starPowerup = false;
    private bool fireFlowerPowerup = false;
    private int fireFlowerAmmo = 5; // temp 5
    private bool invincible = false;

    void Start()
    {  

        // assigning variable from SO gameConstants
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        upSpeed = gameConstants.upSpeed;
        deathImpulse = gameConstants.deathImpulse;
        // vector of mario startpos

        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);
        
    }

    void Update(){
        if(alive){
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        }

        // fireflower
        if(fireFlowerPowerup && Input.GetKeyDown("z")){
            fireFlowerAmmo --;
            // fireball coroutine
            fireFlowerShoot.Invoke();
            print("fireflower ammo left: "+fireFlowerAmmo);
            if(fireFlowerAmmo == 0){
                EndFireFlowerPowerup();
            }
        }
        
    }

    void FixedUpdate()
    {
        if(alive && moving){
            Move(faceRightState == true ? 1 : -1);
        }

        

    }

    // CHANGE STARTING POSITION WHEN CHANGING SCENE
    public void SetStartingPosition(Scene current, Scene next){
        if(next.name == "World 1-2"){
            this.transform.position = new Vector3(-7.41f, -7.099f, 0f);
        }
    }

    // MOVING ACTIONS
    void Move(int value){
        Vector2 movement = new Vector2(value, 0);
        if(marioBody.velocity.magnitude < maxSpeed){
            marioBody.AddForce(movement * speed);
        }
    }

    public void MoveCheck(int value){
        if(value == 0){
            moving = false;
        }
        else{
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump(){
        if(alive && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;

            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    public void JumpHold(){
        if(alive && jumpedState){
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;
        }
    }

    // Callbacks for Input System Actions
    void OnJump(InputAction.CallbackContext context){
        Jump();
    }

    void OnMove(InputAction.CallbackContext context){
        float move = context.ReadValue<float>(); // read value of axis
        MoveCheck((int) move);
        
    }

    void OnJumpHoldPerformed(InputAction.CallbackContext context){
        // print("jump hold");
        JumpHold();
    }

    // Flip Mario
    void FlipMarioSprite(int value){
        if(value == -1 && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            if(marioBody.velocity.x > 0.1f){
                marioAnimator.SetTrigger("onSkid");
            }
        }

        else if(value == 1 && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            if(marioBody.velocity.x < 0.1f){
                marioAnimator.SetTrigger("onSkid");
            }
        }
    }

    public bool GetMarioFaceRight(){
        return faceRightState;
    }


    // allow jump on ground
    void OnCollisionEnter2D(Collision2D col){
        // if any of the collider's layers is one of the collisionLayerMask, then is true.
        // bitwise operands (col's layer binary and 1110 0000)
        if(((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState){
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
        }

    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy") && alive){
            print("inside trigger enter!!");
            if(starPowerup || invincible){ 
                print("death ignored due to invincibility");
            }
            else if(mushroomPowerup){
                EndMushroomPowerup();
                // if(mushroomLife){ // life --
                //     print("mushroom powerup is used up");
                //     // EndMushroomPowerup();
                //     mushroomLife = false;
                // }
            }
            else{ // if no powerups, die.
                other.transform.Find("stomp collider").gameObject.SetActive(false); // so that dead mario doesn't touch the stomping edge collider
                marioAnimator.Play("Mario Die");
                marioDeath.PlayOneShot(marioDeath.clip);            
                alive = false;
            }  
        }
    }

    // POWERUP METHODS
    public void StartStarPowerup(){
        starPowerup = true;
    }

    public void EndStarPowerup(){
        starPowerup = false;
    }

    // MUSHROOM POWERUP
    public void StartMushroomPowerup(){
        
        // mushroomLife = true;
        mushroomPowerup = true;
        // print("start mushroom controller" + mushroomLife + mushroomPowerup);
    }

    public void EndMushroomPowerup(){
        print("end mushroom powerup controller");
        mushroomPowerup = false;
        WaitInvincible(2f);
        endMushroomPowerup.Invoke();
        
    }

    void WaitInvincible(float seconds){
        invincible = true;
        StartCoroutine(WaitInvincibleCoroutine(seconds));
    }

    private IEnumerator WaitInvincibleCoroutine(float seconds){
        yield return new WaitForSecondsRealtime(seconds);
        print("invincible false");
        invincible = false;
    }

    // FIRE FLOWER POWERUP
    public void StartFireFlowerPowerup(){
        print("start fireflower");
        fireFlowerPowerup = true;
    }

    public void EndFireFlowerPowerup(){
        print("end flower fire ");
        fireFlowerPowerup = false;
        if(!mushroomPowerup && !starPowerup){
            marioAnimator.Play("Mario Idle");
        }
        endFireFlowerPowerup.Invoke();
    }



    // called in death animation TODO: DELETE LTR
    void GameOverScreen(){
        alive = false;
        Time.timeScale = 0.0f; //freezes time
        gameOver.Invoke();
    }


    public void GameRestart(){
        marioBody.transform.position = new Vector3(-7.88f, -5.8f, 0.0f);
        faceRightState = true;
        marioSprite.flipX = false;
        // scoreText.text = "Score: 0";

        // stop all audio
        marioAudio.Stop();

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(-4.927341f, -3.865316f, -5.233578f);

        // Powerup restarts (fireflower and mushroom)
        // EndMushroomPowerup();
        // EndFireFlowerPowerup();
        mushroomPowerup = false;
        fireFlowerPowerup = false;
    }


    // used in animation callbacks
    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse(){
        alive = false;
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }


    

}
