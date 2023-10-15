using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

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

    // Question Box animator
    // public Transform questionBoxes;
    // public Transform brickCoinBoxes;
    // public Transform questionStarBoxes;


    
    // InputSystem
    private bool moving = false;
    private  bool jumpedState = false;

    private bool starPowerup = false;


    void Awake(){
        // subscription
        // GameManager.instance.gameRestart.AddListener(GameRestart);
        // GameManager.instance.startPowerup.AddListener(StartPowerup);
        // GameManager.instance.endPowerup.AddListener(EndPowerup);
    }


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

        // SceneManager.activeSceneChanged += SetStartingPosition; - if using same mario singleton
        
    }

    void Update(){
        if(alive){
            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
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
            if(!starPowerup){ 
                other.transform.Find("stomp collider").gameObject.SetActive(false); // so that dead mario doesn't touch the stomping edge collider
                marioAnimator.Play("Mario Die");
                marioDeath.PlayOneShot(marioDeath.clip);            
                alive = false;
            }
            else{ // if star, ignore enemy 
                print("enemy ignored due to star powerup");
            }  
        }
    }

    // POWERUP METHODS
    public void StartPowerup(int powerupType){
        // star mario
        if(powerupType == 1){
            starPowerup = true;
        }

        else if(powerupType == 2){

        }

        else if(powerupType == 3){

        }
        
        else if(powerupType == 4){

        }
    }

    public void EndPowerup(int powerupType){
        // star mario
        if(powerupType == 1){
            starPowerup = false;
        }

        else if(powerupType == 2){

        }

        else if(powerupType == 3){

        }
        
        else if(powerupType == 4){

        }
    }
    

    // called in death animation TODO: DELETE LTR
    void GameOverScreen(){
        alive = false;
        Time.timeScale = 0.0f; //freezes time
        // gameManager.GameOver();
        GameManager.instance.GameOver();
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

    /*
        // restart each questionBox from the parent gameobject
        foreach(Transform qb in questionBoxes){
            qb.GetComponent<Animator>().SetTrigger("restart");
            Transform qbObj = qb.Find("question-box-spring/question_box_1");
            qbObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            qbObj.GetComponent<QuestionBoxHit>().hitDone = false;
        }

        // restart each brickBox with coin
        foreach(Transform bcb in brickCoinBoxes){
            bcb.GetComponent<Animator>().SetTrigger("restart");
            Transform bcbObj = bcb.Find("brick-box-spring/brick_box_1");
            bcbObj.GetComponent<BrickBoxCoinHit>().hitDone = false;
        }

        // i probably need to make an actual boxes manager...
        foreach(Transform bs in questionStarBoxes){
            bs.GetComponent<Animator>().SetTrigger("restart");
            Transform bsObj = bs.Find("question-box-spring/question_box_1");
            bsObj.GetComponent<QuestionBoxStar>().hitDone = false;
        }

        // reset powerups
        GameManager.instance.EndPowerup(1);
        GameManager.instance.ResetPowerup(1);
        // 2, 3, etc...
    */
        // no need to restart brickBox without coin cuz no script it's just existing
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
