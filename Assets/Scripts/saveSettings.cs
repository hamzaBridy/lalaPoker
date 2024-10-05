using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class saveSettings : MonoBehaviour
{
    public GameObject chat;
    public GameObject chatBubbles;
    public GameObject sound;
    public GameObject video;
    public GameObject HandStr;
    public GameObject Viber;
    
    public void save(){
        playerSettings.SaveSettings(chat.GetComponent<Toggle>().isOn,chatBubbles.GetComponent<Toggle>().isOn,sound.GetComponent<Toggle>().isOn,video.GetComponent<Toggle>().isOn,HandStr.GetComponent<Toggle>().isOn,Viber.GetComponent<Toggle>().isOn  );
    }
}
