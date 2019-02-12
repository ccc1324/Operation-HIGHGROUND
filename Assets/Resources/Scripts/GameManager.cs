﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* Manages Overtaking, Health, Death, Camera, and level generation
     * If Overtaking occurs, will set roles and positions of players
     * Will update player's health periodically
     * Death is implemented in the HealthUpdate function
     */

    public Text Player1Health; //temporary
    public Text Player2Health; //temporary

    //health stuff
    public float HealthTickDelay = 1;
    public int HealthTickDamage = 1;
    public int MaxHealth = 20;
    private float _time_since_healthdrain = 0;

    private Camera _camera;
    private LevelManager _level_manager;
    private GameObject[] _players;
    private List<PlayerComponents> _player_components;

    private GameObject _leader;
    private int _leader_num;
    private float _camera_height;
    private float _leader_height;
    private float _total_level_height = 0;


    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        _camera_height = _camera.transform.position.y;
        _level_manager = FindObjectOfType<LevelManager>();
        _players = GameObject.FindGameObjectsWithTag("Player");
        _player_components = new List<PlayerComponents>();
        _leader = GameObject.Find("Fake Leader");
        _leader_num = 0;
        //the point of player_components is we don't want to be calling GetComponent every Update cycle
        //apparently it can make the game laggy, so we want to "stash" those components
        foreach (GameObject player in _players)
        {
            _player_components.Add(new PlayerComponents(
                player,
                GetPlayerNumber(player),
                MaxHealth,
                player.GetComponent<ChaserMovement>(),
                player.GetComponent<LeaderMovement>(),
                player.GetComponent<BasicGun>()));
        }
    }

    private void Update()
    {
        //Overtaking management
        foreach (GameObject player in _players)
        {
            if (player != null)
            {
                //if overtaking occurs
                if (player.transform.position.y > _leader.transform.position.y && player.name != _leader.name)
                {
                    _leader = player;
                    _leader_num = GetPlayerNumber(player);
                    SetRoles();
                    SetPositions();
                    ResetCamera();
                    break;
                }
            }
        }

        //Health Management
        if (Time.time - _time_since_healthdrain >= HealthTickDelay)
        {
            HealthUpdate();
            _time_since_healthdrain = Time.time;
        }

        //Camera Management
        _leader_height = _leader.transform.position.y;
        _camera_height = _camera.transform.position.y;
        if (_leader_height > _camera_height + _camera.orthographicSize * 0.5f) //leader should be >= 3/4 of camera height
            _camera.transform.position = new Vector3(0, _leader_height - _camera.orthographicSize * 0.5f, -10f);

        //Level Management
        if (_leader_height > _level_manager.GetCurrentLevelHeight() + _total_level_height)
        {
            _total_level_height += _level_manager.GetCurrentLevelHeight();
            _level_manager.UpdateCurrentLevel(_total_level_height);
        }
    }

    private void SetRoles()
    {
        foreach (PlayerComponents player in _player_components)
        {
            if (player.Number != _leader_num) //set Chasers
            {
                player.Leader.enabled = false;
                player.Gun.enabled = false;
                player.Chaser.enabled = true;
            }
            else //set Leader
            {
                player.Chaser.enabled = false;
                player.Leader.enabled = true;
                player.Gun.enabled = true;
            }
        }
    }

    private void SetPositions()
    {
        foreach (GameObject player in _players)
        {
            if (player != _leader)
                player.transform.position = _level_manager.GetStartPosition();
            else
                player.transform.position = _level_manager.GetMidPosition();
        }
    }

    private int GetPlayerNumber(GameObject player)
    {
        switch (player.name)
        {
            case "Player1":
                return 1;
            case "Player2":
                return 2;
            case "Player3":
                return 3;
            case "Player4":
                return 4;
            default:
                return 0;
        }
    }

    private void HealthUpdate()
    {
        foreach (PlayerComponents player in _player_components)
        {
            if (player.Number != _leader_num && player.Health > 0)
                player.SetHealth(player.Health - HealthTickDamage);
            if (player.Health <= 0)
                Destroy(player.Reference.gameObject);
        }

        //Update Health Canvas (temporary)
        Player1Health.text = "Player1 Health: " + _player_components[1].Health;
        Player2Health.text = "Player2 Health: " + _player_components[0].Health;
    }

    private void ResetCamera()
    {
        //set camera position relative to Start_Position (where Chasers spawn)
        //offset by half of the size of camera (orthogrraphicSize)
        //offset by thickness of respawn platform
        _camera.transform.position = new Vector3(0, _level_manager.GetStartPosition().y + _camera.orthographicSize - 0.7f, -10f);
    }
}

public class PlayerComponents
{
    public GameObject Reference; //reference to original player object
    public int Number;
    public int Health;
    public ChaserMovement Chaser;
    public LeaderMovement Leader;
    public BasicGun Gun;

    public PlayerComponents(GameObject r, int n, int h, ChaserMovement c, LeaderMovement l, BasicGun g)
    {
        Reference = r;
        Number = n;
        Health = h;
        Chaser = c;
        Leader = l;
        Gun = g;
    }

    public void SetHealth(int h)
    {
        Health = h;
    }
}