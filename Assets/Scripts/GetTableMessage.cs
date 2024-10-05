using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTableMessage : MonoBehaviour
{
    public TextMeshProUGUI textHolder;
    void Start()
    {
        textHolder.text=webSocetConnect.UIWebSocket.MainTable.AllTableMessages;
        webSocetConnect.UIWebSocket.MainTable.OnMessageReceived+=addMessage;
    }

    // Update is called once per frame
    void addMessage()
    {
        textHolder.text+=webSocetConnect.UIWebSocket.MainTable.LastMessage;
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnMessageReceived-=addMessage;
    }
}
