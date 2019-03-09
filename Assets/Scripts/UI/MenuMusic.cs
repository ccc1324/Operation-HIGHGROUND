using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{

    GameObject[] musics;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("MusicTag");
        if (musics.Length > 0)
        {
            if (musics.Length > 1)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //It's rough, it's coarse, it's irritating, and it gets everywhere
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TestSceneC" || SceneManager.GetActiveScene().name == "EasyLevel")
            Destroy(this.gameObject);
    }
}
