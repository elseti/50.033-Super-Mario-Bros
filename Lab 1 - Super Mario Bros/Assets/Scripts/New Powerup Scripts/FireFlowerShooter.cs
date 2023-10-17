using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerShooter : MonoBehaviour
{
    public int maxPrefabInScene = 5; // just max it out...
    public float impulseForce = 10;
    public float degree = 45;
    public GameObject attackPrefab;
    private Transform mario;

    // a scriptable object updated by PlayerMovement / PlayerController to store current Mario's facing
    private bool marioFaceRight;

    void Start(){
        mario = this.gameObject.transform;
    }

    public void ShootFireFlower()
    {
        GameObject[] instantiatedPrefabsInScene = GameObject.FindGameObjectsWithTag("Fireball");
        if (instantiatedPrefabsInScene.Length < maxPrefabInScene)
        {
            // instantiate it where controller (mario) is
            GameObject x = Instantiate(attackPrefab, mario.position, Quaternion.identity);

            // Get the Rigidbody component of the instantiated object
            Rigidbody2D rb = x.GetComponent<Rigidbody2D>();
            // Check if the Rigidbody component exists
            if (rb != null)
            {
                // compute direction vector
                // Vector2 direction = CalculateDirection(degree, marioFaceRight.Value);
                Vector2 direction = CalculateDirection(degree, GetMarioFaceRight());
                // Apply a rightward impulse force to the object
                rb.AddForce(direction * impulseForce, ForceMode2D.Impulse);
            }

        }

    }

    public bool GetMarioFaceRight(){
        marioFaceRight = GetComponent<NewPlayerController>().GetMarioFaceRight();
        print("mario face = "+marioFaceRight);
        return marioFaceRight;
    }

    public Vector2 CalculateDirection(float degrees, bool isFacingRight)
    {
        // Convert degrees to radians
        float radians = degrees * Mathf.Deg2Rad;

        // Calculate the direction vector
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        // If the object is facing left, invert the x-component of the direction
        if (!isFacingRight)
        {
            x = -x;
        }

        return new Vector2(x, y);
    }
}
