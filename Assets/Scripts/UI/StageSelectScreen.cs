using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class StageSelectScreen : MonoBehaviour
{
    Button easylevelbutton;
    Button hardlevelbutton;

    public GameObject selectedObj;
    public EventSystem eventSys;

    private bool buttonSelected;

    GameObject outline1;
    GameObject outline2;
    GameObject text1;
    GameObject text2;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("MenuMusic") != null)
            Destroy(GameObject.Find("MM").gameObject);
        easylevelbutton = transform.Find("EasyButton").gameObject.GetComponent<Button>();
        easylevelbutton.onClick.AddListener(EasyLevel);
        hardlevelbutton = transform.Find("HardButton").gameObject.GetComponent<Button>();
        hardlevelbutton.onClick.AddListener(HardLevel);
        outline1 = GameObject.Find("EasyLevelBack");
        text1 = GameObject.Find("EasyLevelText");
        outline2 = GameObject.Find("HardLevelBack");
        text2 = GameObject.Find("HardLevelText");
        outline1.SetActive(false);
        text1.SetActive(false);
        outline2.SetActive(false);
        text2.SetActive(false);

        eventSys.SetSelectedGameObject(selectedObj);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            eventSys.SetSelectedGameObject(selectedObj);
            buttonSelected = true;
        }
        if (Input.GetAxis("Horizontal_p1") < -0.9f)
        {
            outline1.SetActive(true);
            text1.SetActive(true);
            outline2.SetActive(false);
            text2.SetActive(false);
        }
        else if (Input.GetAxis("Horizontal_p1") > 0.9f)
        {
            outline2.SetActive(true);
            text2.SetActive(true);
            outline1.SetActive(false);
            text1.SetActive(false);
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    private static void EasyLevel()
    {
        SceneManager.LoadScene(4);
    }

    private static void HardLevel()
    {
        SceneManager.LoadScene(1);
    }
}
