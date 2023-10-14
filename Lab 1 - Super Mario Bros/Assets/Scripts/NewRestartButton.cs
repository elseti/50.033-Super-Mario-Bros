using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewRestartButton : MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;

    public void ButtonClick(){
        gameRestart.Invoke();
    }
      
}
