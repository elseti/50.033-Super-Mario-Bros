using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewEnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    public UnityEvent damagePlayer;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);


    void Start()
    {
        // SO variables
        enemyPatroltime = gameConstants.goombaPatrolTime;
        maxOffset = gameConstants.goombaMaxOffset;

        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x; // get the starting position
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        // Debug.Log(other.gameObject.name);
    }

    public void GameRestart()
    {
        this.gameObject.SetActive(true);
        GetComponent<Animator>().Play("Goomba Walk");
        GetComponent<PolygonCollider2D>().enabled = true; // not really necessary but oh well
        transform.Find("stomp collider").gameObject.SetActive(true);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
        
    }
    
    // event used after squish animation
    public void DestroyGoomba(){ // or hide it
        damagePlayer.Invoke();
        this.gameObject.SetActive(false);
    }
 

}