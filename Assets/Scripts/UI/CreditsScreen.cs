using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsScreen : MonoBehaviour
{
    public GameObject selectedObj;
    public EventSystem eventSys;

    private bool buttonSelected;

    // Start is called before the first frame update
    void Start()
    {
        var startbutton = transform.Find("BackButton").gameObject.GetComponent<Button>();
        startbutton.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        //Ultra Instinct Shaggy for Smash
        if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            eventSys.SetSelectedGameObject(selectedObj);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    private static void Back()
    {
        SceneManager.LoadScene(0);
    }

}
