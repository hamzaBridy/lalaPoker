using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onclickVip : MonoBehaviour
{
    public shopScripts funHolder;
    public int index;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clickme);
    }

    // Update is called once per frame
    void clickme()
    {
        if(index==1){
        #if UNITY_ANDROID 
        funHolder.PurchaseProduct("diamondvip");
        #endif
        #if UNITY_IOS
        funHolder.PurchaseProduct("diamond");
        #endif
        }
        else if(index==2){
        #if UNITY_ANDROID 
        funHolder.PurchaseProduct("goldvip");
        #endif
        #if UNITY_IOS
        funHolder.PurchaseProduct("gold");
        #endif
        }
        else if(index==3){
        #if UNITY_ANDROID 
        funHolder.PurchaseProduct("silvervip");
        #endif
        #if UNITY_IOS
        funHolder.PurchaseProduct("silver");
        #endif
        }
    }
}
