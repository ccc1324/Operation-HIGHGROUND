using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
	public List<Sprite> sprites = new List<Sprite>();
	private SpriteRenderer _sr;

	private Color _invulColor = new Color(1, 1, 1, 0.5f);

    // Start is called before the first frame update
    void Start()
    {
		_sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void changeSprite(string action)
	{
		switch (action)
		{
			case "idle":
				_sr.sprite = sprites[0];
				_sr.color = Color.white;
				break;
			case "invul":
				_sr.sprite = sprites[0];
				_sr.color = _invulColor;
				break;
			case "jump":
				_sr.sprite = sprites[1];
				break;
			case "stun":
				_sr.sprite = sprites[2];
				break;
		}
	}



}
