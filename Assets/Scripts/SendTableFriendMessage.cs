using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SendTableFriendMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField  message;
    public static string id;
    public GameObject g1;
    public TextMeshProUGUI text;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(send);
    }
    public void send(){
        if(message.text != "")
        {
            //g1.GetComponent<APIcalls>().SendPrivateMessageWrap(id,message.text);
            //       text.text+= "<color=#FF5733>" + ProfileInfo.Player.user_name+"</color> : "+message.text+"\n";
            //       message.text="";
            DateTime currentDate = DateTime.Now;
            string dateConfig = currentDate.ToString("yyyy-MM-dd HH:mm");




            Debug.Log("mama");
            g1.GetComponent<APIcalls>().SendPrivateMessageWrap(id, message.text);
            string newMessage = $"<size={20}>{dateConfig}</size>\n";
            text.text += newMessage + "<color=#FF0000>" + ProfileInfo.Player.user_name + "</color> : " + message.text + "\n";
            message.text = "";

        }
       
    }
}
