using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    /*This script should be attached to every Level/Chunk to have it interact with LevelManager
     * IMPORTANT
     * The position of every level/chunk should be set to 0, 0, 0
     * Resapwn points should be from lowest to highest (y-position) (or things WILL break)
     */
    public Vector3 Start_Position; //Start of level (y-position of top of bottommost platform)
    public Vector3 Mid_Position; //Mid position should be the position you want Leader to spawn
    public Vector3 End_Position; //End of levle (x-position of top of topmost platform)
    public List<Vector3> Respawnpoints; //Position of respawn points

    void Start()
    {
        Start_Position.y += transform.position.y;
        Mid_Position.y += transform.position.y;
        End_Position.y += transform.position.y;
    }
}
