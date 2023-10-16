using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewAnimationIncreaseScore : MonoBehaviour
{
    // put in gameobject with the animator

    public UnityEvent increaseScore;
    
    public void AnimationIncreaseScore(){
        increaseScore.Invoke();
    }
}
