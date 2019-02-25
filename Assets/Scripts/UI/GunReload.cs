using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunReload : MonoBehaviour
{
    private Slider s;

    // Start is called before the first frame update
    void Start()
    {
        s = GetComponent<Slider>();
        s.value = 60;
    }

    public float GetValue()
    {
        return s.value;
    }

    public void SetValue(float v)
    {
        s.value = v;
    }

    public void AddValue(float v)
    {
        s.value = s.value + v;
    }

    // Update is called once per frame
    void Update()
    {
        //It's over Anakin, I have the High Ground.
    }
}
