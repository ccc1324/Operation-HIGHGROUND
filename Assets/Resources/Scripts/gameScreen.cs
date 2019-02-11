using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameScreen : MonoBehaviour
{
	public GameObject mainGame;
	private float maxHeight;

	// Start is called before the first frame update
	void Start()
    {
        mainGame = GameObject.Find("MainGame"); //Temporary
        maxHeight = mainGame.GetComponent<LevelManager>().maxHeightAchieved;
    }

    // Update is called once per frame
    void Update()
    {
		//player = Game.game.GetLeader();
        maxHeight = mainGame.GetComponent<LevelManager>().maxHeightAchieved;
		transform.position = new Vector3(0,maxHeight,-20);
	}
}
