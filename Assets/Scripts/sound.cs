using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
	//public List<Sprite> sprites = new List<Sprite>();
	//private SpriteRenderer _sr;
	//private Color _invulColor = new Color(1, 1, 1, 0.5f);

	private AudioSource _audSource;
	//public List<AudioClip> audioClips;
	public AudioClip jump;
	public AudioClip shoot;
	public AudioClip stun;
	public AudioClip overtake;

	// Start is called before the first frame update
	void Start()
    {
		//_sr = GetComponent<SpriteRenderer>();
		_audSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void playSound(string action)
	{
		switch (action)
		{
			/*
			case "idle":
				_sr.sprite = sprites[0];
				_sr.color = Color.white;
				break;
			case "invul":
				_sr.sprite = sprites[0];
				_sr.color = _invulColor;
				break;
				*/
			case "jump":
				//_sr.sprite = sprites[1];
				_audSource.PlayOneShot(jump, 0.2f);
				break;
			case "shoot":
				_audSource.PlayOneShot(shoot, 0.2f);
				break;
			case "stun":
				//_sr.sprite = sprites[2];
				_audSource.PlayOneShot(stun, 0.2f);
				break;
			case "overtake":
				//_sr.sprite = sprites[3];
				_audSource.PlayOneShot(overtake, 1.1f);
				break;
		}
	}



}
