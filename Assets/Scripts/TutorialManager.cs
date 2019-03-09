using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Player1;
    public Camera Camera;
    private float _leader_height;
    private float _camera_height;

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
    }
}
