using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject StartLevel;
    public GameObject[] Levels; //load all levels here

    private GameManager _game_manager;
    private GameObject _oldLevel;
    private GameObject _currentLevel;
    private Level _current_level_info;
    private System.Random _rng = new System.Random();
    private int _current_level_num = -1; //used for randomization of levels


    private void Start()
    {

    }

    //Called by GameManager when leader reaches end of current level
    //Instantiates a new level and archives the old one
    public void UpdateCurrentLevel(float total_height)
    {
        //will only run at start
        if (_current_level_num == -1)
        {
            _current_level_num = 99;
            _currentLevel = StartLevel;
            _current_level_info = _currentLevel.GetComponent<Level>();
            Instantiate(_currentLevel, new Vector3(0, total_height, 0), new Quaternion(0, 0, 0, 1));
            return;
        }

        int nextLevel;
        _oldLevel = _currentLevel;

        do
        {
            nextLevel = _rng.Next(0, Levels.Length);
        } while (nextLevel == _current_level_num);

        _current_level_num = nextLevel;
        _currentLevel = Levels[nextLevel];
        _current_level_info = _currentLevel.GetComponent<Level>();
        Instantiate(_currentLevel, new Vector3(0, total_height, 0), new Quaternion(0, 0, 0, 1));
    }

    //returns height of current level = End - Start heights
    public float GetCurrentLevelHeight()
    {
        return _current_level_info.End_Position.y - _current_level_info.Start_Position.y;
    }

    //return respawn points of current level
    public List<Vector3> GetRespawnPoints()
    {
        return _current_level_info.Respawnpoints;
    }
}
