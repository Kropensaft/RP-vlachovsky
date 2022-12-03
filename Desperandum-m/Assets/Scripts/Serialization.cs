using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Serialization 
{
    public float health;
    public float fuel;
    public int score;
    


    public float[] position;

    
    public Serialization (Character player)
    {
        health = player.currentHealth;
        fuel = player.currentFuel;
        score = player.score;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }


}
