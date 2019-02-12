using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Vector3 Start_Position; //Start position should be y-axis of top of bottommost platform (or position you want chasers to spawn)
    public Vector3 Mid_Position; //Mid position should be the position you want Leader to spawn
    public Vector3 End_Position; //End position should be the y-axis of top of topmost platform

    void Start()
    {
        Start_Position.y += transform.position.y;
        Mid_Position.y += transform.position.y;
        End_Position.y += transform.position.y;
    }
}
