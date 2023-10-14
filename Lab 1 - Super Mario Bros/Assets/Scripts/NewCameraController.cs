using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    private Transform player; // mario's Transform
    public Transform endLimit; // GameObject indicating end of map
    private float offset; // initial x-offset between camera and mario
    private float startX; // smallest x-coordinate of the camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;
    private Vector3 startPosition = new Vector3(-4.927341f, -4f, -5.233578f);

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft =  Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth  =  Mathf.Abs(bottomLeft.x  -  this.transform.position.x); // this = camera's position
        offset  =  this.transform.position.x  -  player.position.x;
        startX  =  this.transform.position.x;
        endX  =  endLimit.transform.position.x  -  viewportHalfWidth;
    
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX =  player.position.x  +  offset;
        // check if desiredX is within startX and endX
        if (desiredX  >  startX  &&  desiredX  <  endX)
        this.transform.position  =  new  Vector3(desiredX, this.transform.position.y, this.transform.position.z);
    }

    public void GameRestart()
    {
        // reset camera position
        print("reset cam pos");
        transform.position = startPosition;
    }
}
