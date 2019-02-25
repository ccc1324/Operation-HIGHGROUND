using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbars : MonoBehaviour
{

    private Slider s;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Slider>();
        s.value = 100;
    }

    public void SetValue(float v)
    {
        s.value = v;
    }

    // Update is called once per frame
    void Update()
    {
        //It's over Anakin, I have the High Ground.
    }
}
