using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject pic;
    GameObject txt;
    
    void Start()
    {
        if (this.gameObject.name == "EasyButton")
        {
            pic = GameObject.Find("EasyLevelBack");
            txt = GameObject.Find("EasyLevelText");
        }
        else if (this.gameObject.name == "HardButton")
        {
            pic = GameObject.Find("HardLevelBack");
            txt = GameObject.Find("HardLevelText");
        }
        pic.SetActive(false);
        txt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Insert meme here
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        pic.SetActive(true);
        txt.SetActive(true);
    }

    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        pic.SetActive(false);
        txt.SetActive(false);
    }
}
