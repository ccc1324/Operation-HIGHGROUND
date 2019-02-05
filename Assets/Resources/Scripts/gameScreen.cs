using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScreen : MonoBehaviour
{
	public GameObject player;
	private float maxHeight;

	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.Find("Basic Player");
		maxHeight = player.GetComponent<generatePlatforms>().maxHeight;
	}

    // Update is called once per frame
    void Update()
    {
		maxHeight = player.GetComponent<generatePlatforms>().maxHeight;
		transform.position = new Vector3(0,maxHeight,-20);
	}
}
