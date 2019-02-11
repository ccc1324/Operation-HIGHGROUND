using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public float maxHeightAchieved;
	public float loadChunckThreshold = 5;
	private int leftRight = -1;
	public GameObject platPrefab;
	public GameObject chunkArray;// = new GameObject[3];
    private GameObject gameManager;
    public Level CurrentLevel;
    //Game gameScript = GetComponent<Game>();
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
        CurrentLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        maxHeightAchieved = GameManager.instance.maxHeightAchieved;
        Instantiate(platPrefab, new Vector3(-4, -1, 0), new Quaternion(0,0,0,0));
		chunkArray = (GameObject)Resources.Load("prefabs/platChunk1", typeof(GameObject));
	}

    // Update is called once per frame
    void Update()
    {
		if (GameManager.instance.maxHeightAchieved > maxHeightAchieved)
			maxHeightAchieved = GameManager.instance.maxHeightAchieved;
		if (maxHeightAchieved > loadChunckThreshold)
			generate();

	}

	void generate()
	{
		Instantiate(platPrefab, new Vector3(-4*leftRight, loadChunckThreshold+5, 0), new Quaternion(0, 0, 0, 0));
		loadChunckThreshold = loadChunckThreshold + 5;
		leftRight = leftRight * -1;
	}
}
