using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
	private float maxHeightAchieved;
    private GameObject gameManager;

	void Start()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
        _rigidbody = GetComponent<Rigidbody2D>();
		maxHeightAchieved = 0;
    }

    void Update()
    {
        maxHeightAchieved = GameManager.instance.maxHeightAchieved;
		if (_rigidbody.position.y < maxHeightAchieved - 7)
		{
			//Debug.Log("flag1");
			Destroy(gameObject);
		}
    }

    public Rigidbody2D getRigidBody()
    {
        return _rigidbody;
    }
}
