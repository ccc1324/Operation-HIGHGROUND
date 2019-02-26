using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChaserMovement : MonoBehaviour
{
    /*
     * Movement script used by "Chaser" players
     * Is able to be stunned
     * Can jump in midair
     */

    public float Move_Speed;
    public float Jump_Force;
    private int _player;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 _facingRight = new Vector3(0, 0, 0);
    private Vector3 _facingLeft = new Vector3(0, 180, 0);
    //private Animator _animator;
    private animations _animator;

    //Color stuff
    private SpriteRenderer _playerSprite;
	private Color _normColor;
	private Color _stunColor;
	public int iFrameTime = 2;
    private bool _invincible = false;

    private bool _grounded = false;
    private bool _touchingWallLeft = false;
    private bool _touchingWallRight = false;
    private bool _normalJump = false;
    private int _wallJump = 0;
    private int _wallJumpCounter = -1;
    private bool _airJump = false;
    private bool _stunned = false;

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        //_animator = GetComponent<Animator>();
        _animator = GetComponent<animations>();

		//More color stuff
		_playerSprite = GetComponent<SpriteRenderer>();
		_normColor = _playerSprite.color;
		_stunColor = new Color(_normColor.r, _normColor.g, _normColor.b, 0.2f);
	}

    private void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        _grounded = Physics2D.BoxCast(position, new Vector2(0.5f, 0.001f), 0, Vector2.down, 1f, 1) ? true : false;
        _touchingWallLeft = Physics2D.BoxCast(position, new Vector2(0.001f, 0.3f), 0, Vector2.left, 0.5f, 1) ? true : false;
        _touchingWallRight = Physics2D.BoxCast(position, new Vector2(0.001f, 0.3f), 0, Vector2.right, 0.5f, 1) ? true : false;

        if (_grounded)
        {
            _normalJump = true;
            _airJump = true;
            _wallJump = 2;
        }
        if (_touchingWallLeft)
        {
            if (_wallJumpCounter == 1 || _wallJumpCounter == -1)
                _wallJump = 2;
            _wallJumpCounter = 0;
        }
        if (_touchingWallRight)
        {
            if (_wallJumpCounter == 0 || _wallJumpCounter == -1)
                _wallJump = 2;
            _wallJumpCounter = 1;
        }
        if (Input.GetButtonDown("JumpA_p" + _player) || Input.GetButtonDown("JumpB_p" + _player) && !_stunned)
            Jump();          
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
        {
            _rigidbody.velocity = new Vector2(Move_Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
            _transform.eulerAngles = _facingRight;
        }
        else
        {
            _rigidbody.velocity = new Vector2(-Move_Speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
            _transform.eulerAngles = _facingLeft;
        }
        //_animator.SetBool("Running", true);
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
        if (_touchingWallLeft && _wallJump > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _wallJump -= 1;
            return;
        }
        if (_touchingWallRight && _wallJump > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _wallJump -= 1;
            return;
        }
        if (_airJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _airJump = false;
            return;
        }
    }

	public void Stun(float stunDuration)
    {
		if (!_stunned && !_invincible) //don't want people to be perma-stunned, and also makes it so that ParalyzeHeal won't be called randomly
		{
            _animator.changeSprite("stun");
			_stunned = true;
			_playerSprite.color = _stunColor;
			Invoke("ParalyzeHeal", stunDuration);
		}
    }

    public void ParalyzeHeal()
    {
        _animator.changeSprite("invul");
        _stunned = false;
		_invincible = true;
		_playerSprite.color = Color.gray;
		Invoke("endIFrames", iFrameTime);
	}

	private void endIFrames()
	{
        _animator.changeSprite("idle");
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
