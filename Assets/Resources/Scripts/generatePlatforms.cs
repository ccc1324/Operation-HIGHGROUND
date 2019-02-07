using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatePlatforms : MonoBehaviour
{
	private Transform _tr;
	public float maxHeight;
	private int newPlatformThreshold;
	private int platformX = -5;
	public GameObject platPrefab;
	// Start is called before the first frame update
	void Start()
    {
        platPrefab = (GameObject)Resources.Load("prefabs/SmallPlat", typeof(GameObject));
        _tr = GetComponent<Transform>();
		maxHeight = _tr.position.y;
		newPlatformThreshold = 0;
	}

    // Update is called once per frame
    void Update()
    {
		if (_tr.position.y > maxHeight)
			maxHeight = _tr.position.y;
		if (maxHeight > newPlatformThreshold)
			generate();
	}

	void generate()
	{
		Instantiate(platPrefab, new Vector3(platformX, newPlatformThreshold + 8, 0), new Quaternion(0,0,0,0));
		newPlatformThreshold = newPlatformThreshold + 4;
		platformX = platformX * -1;
	}
}
