using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Facebook.Unity;
using System;
public class getChips : MonoBehaviour
{
    public static bool isGrad=false;
    void Start()
    {
        APIcalls.onMyStatus+=updateChips;
        GetComponent<TextMeshProUGUI>().text= "$" + ProfileInfo.MyPlayer.chips.ToString("N0");
        webSocetConnect.UIWebSocket.OnChipsUpdated+=updateChips;
    }
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "$" + ProfileInfo.MyPlayer.chips.ToString("N0");
    }

    // Update is called once per frame
    void updateChips()
    {
        if(isGrad){
            
            UpdateMoney.updateText(GetComponent<TextMeshProUGUI>(),ProfileInfo.MyPlayer.chips.ToString());
            // GetComponent<TextMeshProUGUI>().text=ProfileInfo.MyPlayer.chips.ToString();
        }
        else{
            GetComponent<TextMeshProUGUI>().text = "$" + ProfileInfo.MyPlayer.chips.ToString("N0");
        }
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.OnChipsUpdated-=updateChips;
        APIcalls.onMyStatus-=updateChips;
    }
}
