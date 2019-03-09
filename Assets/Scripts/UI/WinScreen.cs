using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class WinScreen : MonoBehaviour
{
    GameObject video;
    GameObject image;
    Button button1;
    Button button2;
    float time_since_start;
    float time_until_video_ends;

    public GameObject selectedObj;
    public EventSystem eventSys;

    private bool buttonSelected;
    private bool canUseButtons;
    // Start is called before the first frame update
    void Start()
    {
        video = GameObject.FindGameObjectWithTag("video");
        if (video.name == "WinVideo1")
            time_until_video_ends = 12;
        else if (video.name == "WinVideo2")
            time_until_video_ends = 9;
        image = GameObject.FindGameObjectWithTag("im");
        button1 = transform.Find("PlayButton").gameObject.GetComponent<Button>();
        button1.onClick.AddListener(Play);
        button2 = transform.Find("QuitButton").gameObject.GetComponent<Button>();
        button2.onClick.AddListener(Quit);
        image.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        canUseButtons = false;
        time_since_start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseButtons)
        {
            if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
            {
                eventSys.SetSelectedGameObject(selectedObj);
                buttonSelected = true;
            }
        }
        if (video.gameObject != null && (Input.GetButtonDown("StartButton1") || Input.GetButtonDown("StartButton2") || Time.time - time_since_start > time_until_video_ends))
        {
            Destroy(video.gameObject);
            image.SetActive(true);
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            canUseButtons = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    private static void Play()
    {
        SceneManager.LoadScene(5);
    }

    private static void Quit()
    {
        SceneManager.LoadScene(0);
    }

}
