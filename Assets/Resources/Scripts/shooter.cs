using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Transform _holder;
    private Camera _cam;

    void Start() {
        _holder = transform;
        Debug.Log(transform.rotation);
        _cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

    }

    public void Shoot()
    {
        //Controls for mouse (somewhat buggy)
        Vector3 direction = Input.mousePosition - _cam.WorldToScreenPoint(_holder.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * 360 / (2 * Mathf.PI);

        //float xaxis = Input.GetAxis("Horizontal");
        //float yaxis = Input.GetAxis("Vertical");
        //float angle = Mathf.Atan2(yaxis, xaxis) * 360 / (2 * Mathf.PI);

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(BulletPrefab, _holder.position, rotation, _holder);
    }
}
