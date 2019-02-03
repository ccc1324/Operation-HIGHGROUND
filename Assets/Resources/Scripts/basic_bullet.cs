using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_bullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float Speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        float rotation = transform.rotation.eulerAngles.z / 180 * Mathf.PI; //rotation in radians
        Debug.Log(transform.rotation.eulerAngles.z);
        //Debug.Log(Mathf.Cos(rotation));
        _rb.velocity = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)) * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        basic_bullet movement = collision.GetComponent<basic_bullet>(); //find unique movement component to runners
        if (movement != null) //if object hit is a runner (not first position player)
        {
            //do something (stun opponent)
            //Destroy(gameObject);
        }
        
    }

}
