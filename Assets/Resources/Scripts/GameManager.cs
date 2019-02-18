using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* Manages Overtaking, Health, Death, Camera, "Respawn", and level generation
     * If Overtaking occurs, will set roles and positions of players
     * Will update player's health periodically
     * Death is implemented in the HealthUpdate function
     */

    public Text Player1Health; //temporary
    public Text Player2Health; //temporary

    public float OvertakeTime = 0.5f;
    private float _time = 0;

    //health stuff
    public float HealthTickDelay = 2;
    public int HealthTickDamage = 2;
    public int MaxHealth = 200;
    public int OffScreenDamage = 20;
    private float _time_since_healthdrain = 0;

    private Camera _camera;
    private LevelManager _level_manager;
    private GameObject[] _players;
    private List<PlayerComponents> _player_components;

    private GameObject _leader;
    private int _leader_num;
    private float _camera_height;
    private float _leader_height;

    //level stuff
    public float LevelBuffer = 0; //height player needs to jump to get to next level
    private float _total_level_height = 0;
    private List<Vector3> _respawn_points = new List<Vector3>();


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
        int i = -1;
        foreach (GameObject player in _players)
        {
            _player_components.Add(new PlayerComponents(player, GetPlayerNumber(player), MaxHealth));
            player.transform.position = new Vector3(i, 3, 0);
            i++;
        }
        _level_manager.UpdateCurrentLevel(0);
        _respawn_points.AddRange(_level_manager.GetRespawnPoints());
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
                    SetPositions(player.transform.position);
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
        if (_leader_height > _camera_height + _camera.orthographicSize * 0.5f) //leader height should be <= 3/4 of camera height
            _camera.transform.position = new Vector3(0, _leader_height - _camera.orthographicSize * 0.5f, -10f);

        //Level Management
        if (_leader_height > _level_manager.GetCurrentLevelHeight() + _total_level_height - _camera.orthographicSize / 1.5)
        {
            _total_level_height += _level_manager.GetCurrentLevelHeight() + LevelBuffer;
            _level_manager.UpdateCurrentLevel(_total_level_height);
            //add new respawn points
            foreach (Vector3 respawnpoint in _level_manager.GetRespawnPoints())
                _respawn_points.Add(new Vector3(respawnpoint.x, respawnpoint.y + _total_level_height, 0));
        }
        
        //Respawn Management (Chasers falling off screen) (want this to be below camera management)
        foreach (PlayerComponents player in _player_components)
        {
            if (player.Reference != null)
            {
                if (player.Reference.transform.position.y < _camera_height - _camera.orthographicSize)
                {
                    player.Rb.velocity = new Vector3(0, 0, 0);
                    player.SetHealth(player.Health - OffScreenDamage);
                    player.Reference.transform.position = FindClosetRespawnPoint(_leader_height - _camera.orthographicSize / 2);
                }
            }
        }
    }

    private void SetPositions(Vector3 leaderPosition)
    {
        Vector3 oldLeaderPosition = leaderPosition;
        Vector3 oldCameraPosition = _camera.transform.position;
        Vector3 newLeaderPosition = FindClosetRespawnPoint(leaderPosition.y + _camera.orthographicSize / 2);
        Vector3 newCameraPosition = new Vector3(0, newLeaderPosition.y - _camera.orthographicSize / 3, -10);
        Vector3 newChaserPosition = FindClosetRespawnPoint(newLeaderPosition.y - _camera.orthographicSize / 2);
        foreach (GameObject player in _players)
        {
            if (player != _leader)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                player.transform.position = newChaserPosition;
                //eventually add some fancy animation with this
            }
        }
        _time = Time.time;
        DisableRoles();
        _leader.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        _leader.GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(Lerp(oldLeaderPosition, newLeaderPosition, oldCameraPosition, newCameraPosition));
    }

    private IEnumerator Lerp(Vector3 oldLeaderPosition, Vector3 newLeaderPosition, Vector3 oldCameraPosition, Vector3 newCameraPosition)
    {
        float time = 0;
        while (time <= OvertakeTime)
        {
            time = Mathf.Abs(Time.time - _time);
            _leader.transform.position = Vector3.Lerp(oldLeaderPosition, newLeaderPosition, time / OvertakeTime);
            _camera.transform.position = Vector3.Lerp(oldCameraPosition, newCameraPosition, time / OvertakeTime);
            yield return null;
        }
        _leader.transform.position = newLeaderPosition;
        _camera.transform.position = newCameraPosition;
        SetRoles();
        _leader.GetComponent<Rigidbody2D>().simulated = true;
    }

    private void DisableRoles()
    {
        foreach (PlayerComponents player in _player_components)
        {
            player.Chaser.enabled = false;
            player.Leader.enabled = false;
            player.Gun.enabled = false;
            player.Invincible = true;
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
                player.Invincible = false;
            }
            else //set Leader
            {
                player.Chaser.enabled = false;
                player.Leader.enabled = true;
                player.Gun.enabled = true;
                player.Invincible = false;
            }
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
        Player1Health.text = "Player1 Health: " + _player_components[0].Health;
        Player2Health.text = "Player2 Health: " + _player_components[1].Health;
    }

    //Helper function that finds closest respawn point to a certain location
    private Vector3 FindClosetRespawnPoint(float maxHeight)
    {
        for (int i = _respawn_points.Count - 1; i >= 0; i--)
            if (_respawn_points[i].y < maxHeight)
                return _respawn_points[i];
        Debug.LogError("No Respawn Points?");
        return new Vector3();
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
}

public class PlayerComponents
{
    public GameObject Reference; //reference to original player object
    public Rigidbody2D Rb;
    public int Number;
    public int Health;
    public ChaserMovement Chaser;
    public LeaderMovement Leader;
    public BasicGun Gun;
    public bool Invincible = false;

    public PlayerComponents(GameObject r, int n, int h)
    {
        Reference = r;
        Rb = r.GetComponent<Rigidbody2D>();
        Number = n;
        Health = h;
        Chaser = r.GetComponent<ChaserMovement>();
        Leader = r.GetComponent<LeaderMovement>();
        Gun = r.GetComponent<BasicGun>();
    }

    public void SetHealth(int h)
    {
        if (!Invincible)
        Health = h;
    }
}