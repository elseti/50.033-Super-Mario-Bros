using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreGameOverText;

    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;

    public GameObject gameOverPanel;

    public Animator marioAnimator;

    // Audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;

    [System.NonSerialized]
    public bool alive = true;
    public float deathImpulse = 30;

    public Transform gameCamera;    

    int collisionLayerMask = (1 << 6) | (1 << 7) | (1 << 8); // bitwise OR between layers; i.e. becomes 1110 0000 i think?

    // Question Box animator
    // public Animator questionBoxAnimator;
    // public Rigidbody2D questionBoxRigidbody;
    public Transform questionBoxes;
    public Transform brickCoinBoxes;


    // Start is called before the first frame update
    void Start()
    {
       Application.targetFrameRate = 30;
       marioBody = GetComponent<Rigidbody2D>();
       marioSprite = GetComponent<SpriteRenderer>();

       marioAnimator.SetBool("onGround", onGroundState);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(alive){
            // flip mario
            if(Input.GetKeyDown("a") || Input.GetKey(KeyCode.LeftArrow) && faceRightState){
                faceRightState = false;
                marioSprite.flipX = true;
                
                if(marioBody.velocity.x > 0.1f){
                    marioAnimator.SetTrigger("onSkid");
                }
            }

            if(Input.GetKeyDown("d") || Input.GetKey(KeyCode.RightArrow) && !faceRightState){
                faceRightState = true;
                marioSprite.flipX = false;

                if(marioBody.velocity.x < -0.1f){
                    marioAnimator.SetTrigger("onSkid");
                }
            }

            marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        }
        
    }

    // called 50x a sec
    void FixedUpdate()
    {
        if(alive){
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            // moving
            if(Mathf.Abs(moveHorizontal) > 0){
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if(marioBody.velocity.magnitude < maxSpeed){
                    marioBody.AddForce(movement * speed);
                }
                
            }

            // stop when key is lifted
            if(Input.GetKeyUp("a") || Input.GetKeyUp("d")){
                marioBody.velocity = Vector2.zero;
            }

            // jump
            if(Input.GetKeyDown("space") && onGroundState){
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                marioAnimator.SetBool("onGround", onGroundState);
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
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    // called in death animation
    void GameOverScreen(){
        alive = false;
        Time.timeScale = 0.0f; //freezes time
        gameOverPanel.SetActive(true); // show gameover screen
    }

    // it is private by default, don't forget to public it.
    public void RestartButtonCallback(int input){
        ResetGame();
        Time.timeScale = 1.0f;
    }

    private void ResetGame(){
        marioBody.transform.position = new Vector3(-7.88f, -5.8f, 0.0f);
        faceRightState = true;
        marioSprite.flipX = false;
        scoreText.text = "Score: 0";

        // stop all audio
        marioAudio.Stop();

        // return enemies to starting positions
        foreach (Transform eachChild in enemies.transform){
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        // reset score to 0
        jumpOverGoomba.score = 0;
        scoreGameOverText.text= "Score: 0";

        // hide gameover screen
        gameOverPanel.SetActive(false);

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
