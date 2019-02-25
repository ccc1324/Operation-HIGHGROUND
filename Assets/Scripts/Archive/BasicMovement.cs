using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("w"))
            _rb.AddForce(new Vector2(0, 1700));
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("a"))
            _rb.velocity = new Vector2(-700 * Time.fixedDeltaTime, _rb.velocity.y);
        else if (Input.GetKey("d"))
            _rb.velocity = new Vector2(700 * Time.fixedDeltaTime, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
}
