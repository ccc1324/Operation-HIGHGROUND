using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float LevelBuffer = 2; //height player needs to jump to get to next level


    public GameObject[] _levels;
    private GameObject _oldLevel;
    private GameObject _currentLevel;
    private Level _current_level_info;
    private System.Random _rng = new System.Random();
    private int _current_level_num;


    private void Start()
    {
        //_levels = Resources.LoadAll("Levels/BasicLevel") as GameObject[];
        _current_level_num = _rng.Next(0, _levels.Length);
        _currentLevel = _levels[_current_level_num];
        _current_level_info = _currentLevel.GetComponent<Level>();
        Instantiate(_currentLevel, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
    }

    //Called by GameManager when leader reaches end of current level
    //Instantiates a new level and archives the old one
    public void UpdateCurrentLevel(float total_height)
    {
        int nextLevel;
        _oldLevel = _currentLevel;

        do
        {
            nextLevel = _rng.Next(0, _levels.Length);
        } while (false); //use the one below when we have levels to randomize
        //while (nextLevel != _current_level_num);

        _current_level_num = nextLevel;
        _currentLevel = _levels[nextLevel];
        _current_level_info = _currentLevel.GetComponent<Level>();
        Instantiate(_currentLevel, new Vector3(0, total_height + LevelBuffer, 0), new Quaternion(0, 0, 0, 1));
    }

    //returns height of current level = End - Start heights
    public float GetCurrentLevelHeight()
    {
        return _current_level_info.End_Position.y - _current_level_info.Start_Position.y;
    }

    //returns start position of current level
    public Vector3 GetStartPosition()
    {
        return _current_level_info.Start_Position;
    }

    //returns mid position of current level
    public Vector3 GetMidPosition()
    {
        return _current_level_info.Mid_Position;
    }
}
