using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level CurrentLevel;
    void Start()
    {
        CurrentLevel = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
