using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrivateChateManager : MonoBehaviour
{
    public GameObject ApiRef;
    public TextMeshProUGUI text;
    public static string PrivateMessages="";
    public static string CurrentChateId;
    void Start()
    {
        ApiRef.GetComponent<APIcalls>().OnGetMessages+=addMessages;
        webSocetConnect.UIWebSocket.OnGetMessages+=addMessage;
    }
    public void addMessages(){
        text.text=PrivateMessages;
    }
    public void addMessage(){
        if(webSocetConnect.UIWebSocket.MessageData.sender_id==CurrentChateId){
        DateTime currentDate = DateTime.Now;
        string dateConfig= currentDate.ToString("yyyy-MM-dd HH:mm");
        string tmptext= "<color=red>"+webSocetConnect.UIWebSocket.MessageData.sender_name+"</color> : "+webSocetConnect.UIWebSocket.MessageData.message+"\n";
        string newMessage = $"<size={20}>{dateConfig}</size>\n{tmptext}";
        text.text+=newMessage;
        }
    }
    void OnDestroy()
    {
        ApiRef.GetComponent<APIcalls>().OnGetMessages-=addMessages;
        webSocetConnect.UIWebSocket.OnGetMessages-=addMessage;
        CurrentChateId="-1";
    }
    void OnEnable()
    {
        text.text="";
    }
    void OnDisable()
    {
        CurrentChateId="-1";
    }
}
