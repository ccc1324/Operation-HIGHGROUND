using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject[] DoubleJumpIcons;
    public GameObject[] GunIcons;
    public GameObject[] ReloadIcons;
    public Camera Camera;
    private float _leader_height;
    private float _camera_height;
    private bool _leader_set = false;
    private int _leader = 0;

    void Start()
    {

    }

    void Update()
    {
        //Camera & Background Management
        _leader_height = Player1.transform.position.y;
        _camera_height = Camera.transform.position.y;
        if (_leader_height > _camera_height + Camera.orthographicSize * 0.5f) //leader height should be <= 3/4 of camera height
            Camera.transform.position = new Vector3(0, _leader_height - Camera.orthographicSize * 0.5f, -10f);
        if (_leader_height < _camera_height) //leader height should be >= 1/2 of camera height
            Camera.transform.position = new Vector3(0, _leader_height, -10f);

        if (Player1.transform.position.y > 52 || Player2.transform.position.y > 52) //begin leader tutorial section
        {
            _leader_set = true;
        }

        //leader chaser tutorial
        if (_leader_set && _leader != 1 && Player1.transform.position.y > Player2.transform.position.y)
        {
            Player1.GetComponent<LeaderMovement>().enabled = true;
            Player1.GetComponent<BasicGun>().enabled = true;
            Player1.GetComponent<ChaserMovement>().enabled = false;
            DoubleJumpIcons[0].SetActive(false);
            GunIcons[0].SetActive(true);
            ReloadIcons[0].SetActive(true);
            Player2.GetComponent<LeaderMovement>().enabled = false;
            Player2.GetComponent<BasicGun>().enabled = false;
            Player2.GetComponent<ChaserMovement>().enabled = true;
            DoubleJumpIcons[1].SetActive(true);
            GunIcons[1].SetActive(false);
            ReloadIcons[1].SetActive(false);
            _leader = 1;
        }
        else if (_leader_set && _leader != 2 && Player1.transform.position.y < Player2.transform.position.y)
        {
            Player2.GetComponent<LeaderMovement>().enabled = true;
            Player2.GetComponent<BasicGun>().enabled = true;
            Player2.GetComponent<ChaserMovement>().enabled = false;
            DoubleJumpIcons[1].SetActive(false);
            GunIcons[1].SetActive(true);
            ReloadIcons[1].SetActive(true);
            Player1.GetComponent<LeaderMovement>().enabled = false;
            Player1.GetComponent<BasicGun>().enabled = false;
            Player1.GetComponent<ChaserMovement>().enabled = true;
            DoubleJumpIcons[0].SetActive(true);
            GunIcons[0].SetActive(false);
            ReloadIcons[0].SetActive(false);
            _leader = 2;
        }

    }
}
