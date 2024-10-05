using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBot : MonoBehaviour
{
    public GameObject chatB;
    void Start()
    {
        checkB();
        webSocetConnect.UIWebSocket.MainTable.ChangeDone+=checkB;
    }

    // Update is called once per frame
    void checkB()
    {
        if(ProfileInfo.Player.isSit){
            chatB.SetActive(true);
        }
        else{
            chatB.SetActive(false);
        }
    }
    void OnEnable()
    {
        checkB();
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.ChangeDone-=checkB;
    }
}
