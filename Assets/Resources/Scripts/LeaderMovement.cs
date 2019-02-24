using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeaderMovement : MonoBehaviour
{
    /*
     * Movement script used by "Leader" players
     * 
     */
    public float Move_Speed;
    public float Jump_Force;
    private int _player;
    private Rigidbody2D _rigidbody;

    private bool _grounded = false;
    private bool _touchingWallLeft = false;
    private bool _touchingWallRight = false;
    private bool _normalJump = false;
    private bool _wallJump = false;
    private int _wallJumpCounter = -1;

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        _grounded = Physics2D.BoxCast(position, new Vector2(1, 1), 0, Vector2.down, 0.05f, 1) ? true : false;
        _touchingWallLeft = Physics2D.BoxCast(position, new Vector2(1, 1), 0, Vector2.left, 0.1f, 1) ? true : false;
        _touchingWallRight = Physics2D.BoxCast(position, new Vector2(1, 1), 0, Vector2.right, 0.1f, 1) ? true : false;

        if (_grounded)
        {
            _normalJump = true;
            _wallJump = true;
        }
        if (_touchingWallLeft)
        {
            if (_wallJumpCounter == 1)
                _wallJump = true;
            _wallJumpCounter = 0;
        }
        if (_touchingWallRight)
        {
            if (_wallJumpCounter == 0)
                _wallJump = true;
            _wallJumpCounter = 1;
        }

        if (Input.GetButtonDown("JumpA_p" + _player) || Input.GetButtonDown("JumpB_p" + _player))
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
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_normalJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _normalJump = false;
            return;
        }
        if (_touchingWallLeft && _wallJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _wallJump = false;
            return;
        }
        if (_touchingWallRight && _wallJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _wallJump = false;
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
