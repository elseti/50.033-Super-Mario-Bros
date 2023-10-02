using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompGoomba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        print("collided");
        if(col.gameObject.CompareTag("Player")){
            print("in player");
            GetComponent<Animator>().Play("Goomba Squish");
        }
        
    }
}
