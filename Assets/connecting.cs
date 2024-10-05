using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class connecting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(webSocetConnect.UIWebSocket.isreconnecting){
            SceneManager.LoadSceneAsync("Main Scene", LoadSceneMode.Single);
        }
    }
}
