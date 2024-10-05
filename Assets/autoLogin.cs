using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static APIcalls;

public class autoLogin : MonoBehaviour
{
    // Start is called before the first frame update
    public APIcalls apiHolder;
    [SerializeField] private GameObject loadingScene;
    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "myInfo.json");
        Debug.Log("isCalled");
        if (File.Exists(filePath))
        {
            // Debug.Log("isin");
            loadingScene.SetActive(true);
            string json = File.ReadAllText(filePath);
            userInfo ui = JsonUtility.FromJson<userInfo>(json);
            apiHolder.AutoLogin(ui.Token);
        }
        else
        {
            loadingScene.SetActive(false);
        }
    }

    // Update is called once per frame
    
}
