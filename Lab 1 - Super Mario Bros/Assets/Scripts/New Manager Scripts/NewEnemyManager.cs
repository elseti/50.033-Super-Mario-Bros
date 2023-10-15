using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyManager : MonoBehaviour
{
    void Awake(){
        // GameManager.instance.gameRestart.AddListener(GameRestart);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<NewEnemyController>().GameRestart();
        }

    }

}