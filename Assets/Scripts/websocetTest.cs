using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.WebSocket;
using UnityEngine;
using UnityEngine.Networking;
public class websocetTest : MonoBehaviour
{
    string link="wss://websockets.lalapoker.com/game?token=";
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ProfileInfo.Player.token);
        string NewLink=link+ProfileInfo.Player.token;
        Debug.Log("ks 2m 3li ;)");
        var webSocket = new WebSocket(new Uri(NewLink));
        string sss="{\"command\":\"send-message\",\"params\":{\"message\":\"easy\",\"gameId\":\"1\"}}";
        webSocket.OnOpen += OnWebSocketOpen;
        webSocket.OnError += OnError;
        webSocket.OnClosed += OnWebSocketClosed;
        webSocket.OnMessage += OnMessageReceived;
        webSocket.Open();
        webSocket.Send(sss);
        webSocket.Send(sss);
        webSocket.Send(sss);
        webSocket.Send(sss);
        webSocket.Send(sss);
        Debug.Log(sss);
        webSocket.Send(sss);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
private void OnWebSocketOpen(WebSocket webSocket)
{
	Debug.Log("WebSocket is now Open!");
    string sss="{\"command\":\"send-message\",\"params\":{\"message\":\"easy\",\"gameId\":\"1\"}}";
    webSocket.Send(sss);
}
void OnError(WebSocket ws, string error)
{
	Debug.LogError("Error: " + error);
}
private void OnWebSocketClosed(WebSocket webSocket, UInt16 code, string message)
{
	Debug.Log("WebSocket is now Closed!");
}
private void OnMessageReceived(WebSocket webSocket, string message)
{
	Debug.Log("Text Message received from server: " + message);
}
}
