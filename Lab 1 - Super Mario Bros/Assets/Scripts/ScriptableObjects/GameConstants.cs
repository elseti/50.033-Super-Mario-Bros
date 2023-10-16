
using UnityEngine;


[CreateAssetMenu(fileName =  "GameConstants", menuName =  "ScriptableObjects/GameConstants", order =  1)]
public class GameConstants : ScriptableObject
{
    // lives
    public int maxLives;

    // Mario's movement
    public int speed;
    public int maxSpeed;
    public int upSpeed;
    public int deathImpulse;
    public Vector3 marioStartingPosition1_1;

    public Vector3 marioStartingPosition1_3;

    
    // Goomba's movement
    public float goombaPatrolTime;
    public float goombaMaxOffset;


    // Prof's FSM
    public float flickerInterval;
}

