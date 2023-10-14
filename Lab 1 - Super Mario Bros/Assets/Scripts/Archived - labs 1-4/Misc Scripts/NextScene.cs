using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextSceneName;
    
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            
            // loads scene asynchronously in the background - better performance than LoadScene.
            // 2 modes - single and additive
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        }
    }
}
