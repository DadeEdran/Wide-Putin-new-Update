using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] position;
    public int [] Quiz;
    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        if (player.quiz != null)
        Quiz = new int[player.quiz.Length];
        else
            Quiz = new int[0];

        for (int i = 0; i < Quiz.Length; i++)
            Quiz[i] = player.quiz[i];


    }

}
