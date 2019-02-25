using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaserMovementBroken : MonoBehaviour
{
    /*
     * Movement script used by "Chaser" players
     * Is able to be stunned
     * Can jump in midair
     */

    public float Move_Speed = 700f;
    public float Jump_Force = 1500f;
    private int _player;
    private Rigidbody2D _rigidbody;
	private Collider2D _collider;

	//Color stuff
	private SpriteRenderer _playerSprite;
	private Color _normColor;
	private Color _stunColor;
	private bool _invincible = false;
	public int iFrames = 2;

	private int _jumps;
    private bool _stunned = false;
	private int wallJumpModifier = 0;

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();
        _jumps = 0;

		//More color stuff
		_playerSprite = GetComponent<SpriteRenderer>();
		_normColor = _playerSprite.color;
		_stunColor = new Color(_normColor.r, _normColor.g, _normColor.b, 0.2f);
	}

    private void Update()
    {
        if ((Input.GetButtonDown("JumpA_p" + _player) || Input.GetButtonDown("JumpB_p" + _player)) && !_stunned)
        {
            Jump();          
        }
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal_p" + _player);
        if (Mathf.Abs(movement) > 0.9f && !_stunned)
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
        if (_jumps > 0)
        {
			if (wallJumpModifier != 0)
				_rigidbody.velocity = new Vector2(wallJumpModifier*Jump_Force, Jump_Force);
			else
			{
				_rigidbody.velocity = (new Vector2(0, Jump_Force));
				if (_jumps == 1)
					_jumps--;
			}
        }
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
		GameObject collidedWith = collision.gameObject;

		if (collidedWith.GetComponent<Platform>() != null)
		{
			if (collision.GetContact(0).normal == new Vector2(-1, 0))
			{
				wallJumpModifier = -1;
			}
			else if (collision.GetContact(0).normal == new Vector2(1, 0))
			{
				wallJumpModifier = 1;
			}

			else _jumps = 2;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Platform>() != null)
		{
			_jumps = 1;
			wallJumpModifier = 0;
		}
	}

	public void Stun(float stunDuration)
    {
		if (!_stunned && !_invincible) //don't want people to be perma-stunned, and also makes it so that ParalyzeHeal won't be called randomly
		{
			Debug.Log("stunned");
			_stunned = true;
			_playerSprite.color = _stunColor;
			Invoke("ParalyzeHeal", stunDuration);
		}
    }

    public void ParalyzeHeal()
    {
		_stunned = false;
		_invincible = true;
		_playerSprite.color = Color.gray;
		Invoke("endIFrames", iFrames);
	}

	private void endIFrames()
	{
		_invincible = false;
		_playerSprite.color = _normColor;
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
