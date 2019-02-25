using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var startbutton = transform.Find("PlayButton").gameObject.GetComponent<Button>();
        startbutton.onClick.AddListener(Play);
        var controlsbutton = transform.Find("QuitButton").gameObject.GetComponent<Button>();
        controlsbutton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        //Ultra Instinct Shaggy for Smash
    }

    private static void Play()
    {
        SceneManager.LoadScene(1);
    }

    private static void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
