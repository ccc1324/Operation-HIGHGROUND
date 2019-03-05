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
    public GameObject Forcefield;
    private int _player;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Transform _transform;
    private Vector3 _facingRight = new Vector3(0, 0, 0);
    private Vector3 _facingLeft = new Vector3(0, 180, 0);

	public float iFrameTime;
    private bool _invincible = false;

    private bool _grounded = false;
    private bool _touchingWallLeft = false;
    private bool _touchingWallRight = false;
    private bool _normalJump = false;
    private int _wallJump = 0;
    private int _wallJumpCounter = -1;
    private bool _airJump = false;
    private bool _stunned = false;

	private sound _sound;

    //Boxcast Debug Stuff
    //public Text Ground;
    //public Text Left;
    //public Text Right;

    void Start()
    {
        Forcefield.SetActive(false);
        _player = PlayerController();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
		_sound = GetComponent<sound>();
	}

    private void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y - 0.35f);
        _grounded = Physics2D.BoxCast(position, new Vector2(1.2f, 0.001f), 0, Vector2.down, 1.00f, 1) ? true : false;
        _animator.SetBool("Grounded", _grounded);
        _touchingWallLeft = Physics2D.BoxCast(position, new Vector2(0.001f, 1.7f), 0, Vector2.left, 0.9f, 1) ? true : false;
        _touchingWallRight = Physics2D.BoxCast(position, new Vector2(0.001f, 1.7f), 0, Vector2.right, 0.9f, 1) ? true : false;
        _animator.SetBool("TouchingWall", _touchingWallLeft || _touchingWallRight);

        //Boscast Debug Stuff
        //Ground.text = _grounded.ToString();
        //Left.text = _touchingWallLeft.ToString();
        //Right.text = _touchingWallRight.ToString();

        if (_grounded)
        {
            _normalJump = true;
            _airJump = true;
            RenewWallJumps();
        }
        if (_touchingWallLeft)
        {
            if (_wallJumpCounter == 1 || _wallJumpCounter == -1)
                RenewWallJumps();
            _wallJumpCounter = 0;
        }
        if (_touchingWallRight)
        {
            if (_wallJumpCounter == 0 || _wallJumpCounter == -1)
                RenewWallJumps();
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
        _animator.SetBool("Moving", true);
    }

    private void StopMoving()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _animator.SetBool("Moving", false);
    }

    private void Jump()
    {
        if (_stunned)
            return;
		//Play sound if able to jump
		if (_normalJump || _airJump || ((_touchingWallLeft || _touchingWallRight) && _wallJump>0))
			_sound.playSound("jump");

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
            _animator.SetFloat("Walljumps", _wallJump);
            return;
        }
        if (_touchingWallRight && _wallJump > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jump_Force);
            _wallJump -= 1;
            _animator.SetFloat("Walljumps", _wallJump);
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
			_stunned = true;
			Invoke("ParalyzeHeal", stunDuration);
            _animator.SetBool("Stunned", true);
            _sound.playSound("stun");
		}
    }

    public void ParalyzeHeal()
    {
        _stunned = false;
        _animator.SetBool("Stunned", false);
        _invincible = true;
        Forcefield.SetActive(true);
        Forcefield.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        Invoke("endIFrames", iFrameTime);
	}

	private void endIFrames()
	{
        _invincible = false;
        Forcefield.SetActive(false);
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

    private void RenewWallJumps()
    {
        _wallJump = 2;
        _animator.SetFloat("Walljumps", 2);
    }
}
