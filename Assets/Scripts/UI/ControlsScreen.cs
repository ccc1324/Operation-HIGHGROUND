using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var startbutton = transform.Find("BackButton").gameObject.GetComponent<Button>();
        startbutton.onClick.AddListener(Back);
        var controlsbutton = transform.Find("NextButton").gameObject.GetComponent<Button>();
        controlsbutton.onClick.AddListener(Next);
    }

    // Update is called once per frame
    void Update()
    {
        //Ultra Instinct Shaggy for Smash
    }

    private static void Back()
    {
        SceneManager.LoadScene(2);
    }

    private static void Next()
    {
        SceneManager.LoadScene(0);
    }

}
