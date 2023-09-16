using System.Runtime.InteropServices;

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
    

    // Start is called before the first frame update
    void Start()
    {
       Application.targetFrameRate = 30;
       marioBody = GetComponent<Rigidbody2D>();
       marioSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // flip mario
        if(Input.GetKeyDown("a") || Input.GetKey(KeyCode.LeftArrow) && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if(Input.GetKeyDown("d") || Input.GetKey(KeyCode.RightArrow) && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    // called 50x a sec
    void FixedUpdate()
    {
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
        }

        
        
    }


    // allow jump on ground
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy")){
            // Debug.Log("goomba collision");
            Time.timeScale = 0.0f; //freezes time

            gameOverPanel.SetActive(true); // show gameover screen
        }
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

        // return enemies to starting positions
        foreach (Transform eachChild in enemies.transform){
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

        // reset score to 0
        jumpOverGoomba.score = 0;

        // hide gameover screen
        gameOverPanel.SetActive(false);

        scoreGameOverText.text= "Score: 0";
    }
}
