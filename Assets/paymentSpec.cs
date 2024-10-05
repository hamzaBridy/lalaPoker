using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paymentSpec : MonoBehaviour
{
    // Start is called before the first frame update
    public string itemId;
    public shopScripts shsc;
    public APIcalls apiHolder;
    public Image cashImage;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(doPayment);
    }
    void doPayment(){
        #if UNITY_WEBGL
                apiHolder.SendCheckOutWrap(itemId);
            //    shsc.PurchaseProduct(itemId);
            #endif

            #if UNITY_IOS
                shsc.PurchaseProduct(itemId);
            #endif

            #if UNITY_ANDROID 
                shsc.PurchaseProduct(itemId);
            #endif
    }

}
