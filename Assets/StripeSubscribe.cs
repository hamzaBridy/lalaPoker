using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StripeSubscribe : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string itemId;
    public shopScripts shsc;
    public APIcalls apiHolder;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(doPayment);
    }
    void doPayment()
    {
       // apiHolder.SubscribeWrap(itemId);

#if UNITY_WEBGL
        apiHolder.SubscribeWrap(itemId);
        //    shsc.PurchaseProduct(itemId);
#endif

        //#if UNITY_IOS
        //              //  shsc.PurchaseProduct(itemId);
        //#endif

        //#if UNITY_ANDROID
        //                shsc.PurchaseProduct(itemId);
        //#endif
    }

}
