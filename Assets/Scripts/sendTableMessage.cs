using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sendTableMessage : MonoBehaviour
{
    public GameObject webSocRef;
    public TMP_InputField textHolder;
    public void OnButtonClick()
    {
if(textHolder.text != "")
{
string text = textHolder.text;
        webSocRef.GetComponent<webSocetConnect>().sendMessage("",text);

        textHolder.text="";
}
        

    }
    public void PreChat(string text){
        webSocRef.GetComponent<webSocetConnect>().sendMessage("",text);
    }
}
