using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isreconnecting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject image;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(webSocetConnect.UIWebSocket.isreconnecting);
        if(webSocetConnect.UIWebSocket.isreconnecting){
            image.SetActive(true);
        }
        else{
            image.SetActive(false);    
        }
    }
}
