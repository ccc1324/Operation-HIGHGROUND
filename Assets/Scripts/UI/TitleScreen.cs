using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var startbutton = transform.Find("StartButton").gameObject.GetComponent<Button>();
        startbutton.onClick.AddListener(StartGame);
        var controlsbutton = transform.Find("ControlsButton").gameObject.GetComponent<Button>();
        controlsbutton.onClick.AddListener(ControlsScreen);
        var creditsbutton = transform.Find("CreditsButton").gameObject.GetComponent<Button>();
        creditsbutton.onClick.AddListener(CreditsScreen);
        var quitbutton = transform.Find("QuitButton").gameObject.GetComponent<Button>();
        quitbutton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        //Ultra Instinct Shaggy for Smash
    }

    private static void StartGame()
    {
        SceneManager.LoadScene(5);
    }

    private static void ControlsScreen()
    {
        SceneManager.LoadScene(6);
    }

    private static void CreditsScreen()
    {
        //SceneManager.LoadScene(7);
    }

    private static void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
