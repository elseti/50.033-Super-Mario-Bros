using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
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
    public Transform questionBoxes;
    public Transform brickCoinBoxes;

    
    // InputSystem
    private bool moving = false;
    private  bool jumpedState = false;

    // GameManager
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {  
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator.SetBool("onGround", onGroundState);
        
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
        // print("jump normal");
        Jump();
    }

    void OnMove(InputAction.CallbackContext context){
        // if(context.started) print("move started");
        // if(context.canceled) print("move cancelled");
        float move = context.ReadValue<float>(); // read value of axis
        // print($"move value: {move}");
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
            marioAnimator.Play("Mario Die");
            marioDeath.PlayOneShot(marioDeath.clip);            
            alive = false;
        }
    }

    // called in death animation TODO: DELETE LTR
    void GameOverScreen(){
        alive = false;
        Time.timeScale = 0.0f; //freezes time
        gameManager.GameOver();
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
