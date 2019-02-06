using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static Game game;
    public GameObject[] players;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        //Finds the player in 1st place
        float max_height = float.MinValue;
        int leader_index = -1;
        int index = 0;
        foreach (GameObject p in players)
        {
            if (p.GetComponent<Rigidbody2D>().position.y > max_height)
            {
                max_height = p.GetComponent<Rigidbody2D>().position.y;
                leader_index = index;
            }
            index++;
        }

        //Activates the leadermovement script and deactivates the chasermovement script for the first place player
        players[leader_index].GetComponent<LeaderMovement>().enabled = true;
        players[leader_index].GetComponent<BasicGun>().enabled = true;
        players[leader_index].GetComponent<ChaserMovement>().enabled = false;

        //Activates the chasermovement script and deactivates the leadermovement script for all the other players
        foreach (GameObject p in players)
        {
            if (!p.Equals(players[leader_index]))
            {
                p.GetComponent<LeaderMovement>().enabled = false;
                p.GetComponent<BasicGun>().enabled = false;
                p.GetComponent<ChaserMovement>().enabled = true;
            }
        }

    }


}
