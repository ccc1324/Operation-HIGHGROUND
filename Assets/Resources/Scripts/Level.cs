using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Vector3 Start_Position;
    public Vector3 Mid_Position;
    public Vector3 End_Position;

    void Start()
    {
        Start_Position.y += transform.position.y;
        Mid_Position.y += transform.position.y;
        End_Position.y += transform.position.y;
    }
}
