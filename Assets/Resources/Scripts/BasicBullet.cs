using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    /*
     * This script can be attached to a bullet object
     * Sets the velovity of bullet (= it's rotation * Public Speed)
     * OnTriggerEnter2D currently set to stun Chasers
     */
    private Rigidbody2D _rb;
    public float Speed;
    public float Stun_Duration; //in seconds

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        float rotation = transform.rotation.eulerAngles.z / 180 * Mathf.PI;
        _rb.velocity = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)) * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChaserMovement movement = collision.GetComponent<ChaserMovement>(); //find unique movement component to Chasers
        if (movement != null && movement.isActiveAndEnabled) //if object hit is a Chaser (not first position player)
        {
            movement.Stun(Stun_Duration);
            Destroy(gameObject);
        }
        
    }

}
