using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaserMovement : MonoBehaviour
{
    /*
     * Movement script used by "Chaser" players
     * Is able to be stunned
     * Can jump in midair
     */

    public float Move_Speed = 1000f;
    public float Jump_Force = 1700f;
    private int _player;
    private Rigidbody2D _rigidbody;

    private bool _canJump = false;
    private bool _stunned = false;

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log(_stunned);
        if (Input.GetButtonDown("JumpA_p" + _player) || Input.GetButtonDown("JumpB_p" + _player) && !_stunned)
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
        _rigidbody.AddForce(new Vector2(0, Jump_Force));           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            if (_rigidbody.position.y >= collision.gameObject.GetComponent<Platform>().getRigidBody().position.y)
            {
                _canJump = true;     
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            _canJump = false;           
        }
    }

    public void Stun(float stunDuration)
    {
        _stunned = true;
        Invoke("ParalyzeHeal", stunDuration);
    }

    public void ParalyzeHeal()
    {
        _stunned = false;
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
