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
    public float Jump_Force = 1500f;
    private int _player;
    private Rigidbody2D _rigidbody;

    private int _jumps;
	private int _wallJumpModifier;
    

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumps = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("JumpA_p" + _player) || Input.GetButtonDown("JumpB_p" + _player))
        {
            Jump();
        }
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
		if (_wallJumpModifier != 0)
			_rigidbody.AddForce(new Vector2(Jump_Force * _wallJumpModifier, Jump_Force));
		else if (_jumps > 0)
        {
            _rigidbody.AddForce(new Vector2(0, Jump_Force));
			if (_jumps == 1)
				_jumps--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
		GameObject collidedWith = collision.gameObject;

		if (collidedWith.GetComponent<Platform>() != null)
		{
			if (collision.GetContact(0).normal == new Vector2(-1, 0))
			{
				_wallJumpModifier = -1;
			}
			else if (collision.GetContact(0).normal == new Vector2(1, 0))
			{
				_wallJumpModifier = 1;
			}

			else _jumps = 1;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		_jumps = 0;
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
