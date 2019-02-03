using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_movement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a"))
            _rb.AddForce(new Vector2(-50, 0));
        if (Input.GetKey("d"))
            _rb.AddForce(new Vector2(50, 0));
        if (Input.GetKeyDown("w"))
            _rb.AddForce(new Vector2(0, 200));

    }
}
