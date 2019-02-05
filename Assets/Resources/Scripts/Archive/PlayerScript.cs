using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    private int _player;
    private Rigidbody2D _rigidbody;
    private Object _bullet;
    private float _velocityX = 0;
    private float _velocityY = 0;
    private Vector2 _velocity;
    private bool _canJump = false;
    private const float refractoryPeriod = 0.5f;
    private float _timeSinceLastFire;

    void Start()
    {
        if (gameObject.name.Equals("Player1"))
            _player = 1;
        else if (gameObject.name.Equals("Player2"))
            _player = 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        _bullet = Resources.Load("BasicBullet");
    }

    void Update()
    {
        _velocity = new Vector2(_velocityX, _velocityY);
        _rigidbody.MovePosition(_rigidbody.position + _velocity* Time.fixedDeltaTime);
        _velocityX = 0;
        _velocityY = 0;
        if (_player == 1)
        {
            Moving(Input.GetAxis("Horizontal_p1"));
            if (Input.GetAxis("Jump_p1") > 0)
                Jump();
            if (Input.GetAxis("Fire_p1") > 0)
            {
                Shoot(Input.GetAxis("AimX_p1"), Input.GetAxis("AimY_p1"));               
            }
                
        }
        else if (_player == 2)
        {
            Moving(Input.GetAxis("Horizontal_p2"));
            if (Input.GetAxis("Jump_p2") > 0)
                Jump();
            if (Input.GetAxis("Fire_p2") > 0)
            {
                Shoot(Input.GetAxis("AimX_p2"), Input.GetAxis("AimY_p2"));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D something)
    {
        if (something.gameObject.GetComponent<Platform>() != null)
        {
            if (_rigidbody.position.y >= something.gameObject.GetComponent<Platform>().getRigidBody().position.y)
                _canJump = true;
        }   
    }

    private void OnCollisionExit2D(Collision2D something)
    {
        if (something.gameObject.GetComponent<Platform>() != null)
        {
                _canJump = false;
        }   
    }

    void Moving(float move) //Moving left and right
    {
        if (move < -0.02f)
        {
            _velocityX = move*2f;             
        }

        else if (move > 0.02f)
        {
            _velocityX = move*2f;
        }
    }

    void Jump() //Jumping
    {
        if (_canJump)
        {
            _velocityY = 10f;
        }
    }

    void Shoot(float x, float y) //Shooting
    {
        float time = Time.time;
        if (time > _timeSinceLastFire + refractoryPeriod)
        {
            _timeSinceLastFire = time;
            Quaternion q = Quaternion.Euler(x, y, 0);
            Vector2 v = (new Vector2(x, y))*20f;
            SpawnBullet(transform.position, q, v);
        }
    }

    void SpawnBullet(Vector2 position, Quaternion rotation, Vector2 velocity)
    {
        //TBA
    }

}
