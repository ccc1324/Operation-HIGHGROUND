using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageSelectScreen : MonoBehaviour
{

    Button easylevelbutton;
    Button hardlevelbutton;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("MenuMusic") != null)
            Destroy(GameObject.Find("MM").gameObject);
        easylevelbutton = transform.Find("EasyButton").gameObject.GetComponent<Button>();
        easylevelbutton.onClick.AddListener(EasyLevel);
        hardlevelbutton = transform.Find("HardButton").gameObject.GetComponent<Button>();
        hardlevelbutton.onClick.AddListener(HardLevel);
    }

    // Update is called once per frame
    void Update()
    {
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
