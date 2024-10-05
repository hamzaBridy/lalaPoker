using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingEnableChat : MonoBehaviour
{
    void Update()
    {
        if(playerSettings.settings.chat){
            GetComponent<Button>().interactable=true;
        }
        else{
            GetComponent<Button>().interactable=false;
            
        }
    }
}
