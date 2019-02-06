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
    public float Move_Speed = 1f;
    public float Move_Constant = 100f;
    public float Jump_Force = 100f;
    public float Jump_Cooldown = 0.5f;
    private int _player;
    private Rigidbody2D _rigidbody;

    private bool _canJump = false;
    private bool _stunned = false;

    void Start()
    {
        _player = PlayerController();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal_p" + _player);
        if (Mathf.Abs(movement) > 0.9f && !_stunned)
            Move(movement);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump_p" + _player))
            Jump();

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

    private void Move(float direction)
    {
        float currentSpeed = _rigidbody.velocity.x;
        float newSpeed = Mathf.Abs(currentSpeed - Move_Speed) / Move_Constant;
        if (direction > 0)
            _rigidbody.AddForce(new Vector2(newSpeed, 0));
        else
            _rigidbody.AddForce(new Vector2(-newSpeed, 0));
    }

    private void StopMoving()
    {
        float velocityY = _rigidbody.velocity.y;
        Vector2 v2 = new Vector2(0, velocityY);
        _rigidbody.velocity = v2;
    }

    private void Jump()
    {
        if (_canJump)
        {
            _rigidbody.AddForce(new Vector2(0, Jump_Force));
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

}
