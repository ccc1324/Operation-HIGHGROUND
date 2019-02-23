using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderMovement : MonoBehaviour
{
    /*
     * Movement script used by "Leader" players
     * 
     */
    public float Move_Speed = 700f;
    public float Jump_Force = 35f;
    public float WallJumpForce = 6000f;
    private int _player;
    private Rigidbody2D _rigidbody;

    private bool _grounded = false;
    private bool _touchingWallLeft = false;
    private bool _touchingWallRight = false;
    private bool _normalJump = false;


    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        _grounded = Physics2D.Raycast(position, Vector2.down, 0.6f, 1) ? true : false;
        if (_grounded)
            _normalJump = true;
        _touchingWallLeft = Physics2D.Raycast(position, Vector2.left, 0.6f, 1) ? true : false;
        _touchingWallRight = Physics2D.Raycast(position, Vector2.right, 0.6f, 1) ? true : false;
        if ((Input.GetButtonDown("JumpA_p" + _player)
            || Input.GetButtonDown("JumpB_p" + _player)
            || Input.GetAxis("JumpC_p" + _player) > 0.3))
            Jump();
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal_p" + _player);
        if (Mathf.Abs(movement) > 0.9f)
            Move(movement);
        else
            StopMoving();
    }

    private void Move(float direction)
    {
        if (direction > 0)
            _rigidbody.velocity = new Vector2(Move_Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        else
            _rigidbody.velocity = new Vector2(-Move_Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void StopMoving()
    {
        //_rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_normalJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _normalJump = false;
            return;
        }
        if (_touchingWallLeft)
        {
            _rigidbody.AddForce(new Vector2(WallJumpForce, 0));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force / 1.6f);
            return;
        }
        if (_touchingWallRight)
        {
            _rigidbody.AddForce(new Vector2(-WallJumpForce, 0));
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force / 1.6f);
            return;
        }
    }

    private int PlayerController()
    {
        string name = gameObject.name;
        switch (name)
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
                Debug.LogError("Player object must be name Playerx, with x being the number of the player");
                return 0;
        }
    }
}
