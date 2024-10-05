using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getpaymentSc : MonoBehaviour
{
    public APIcalls apiHolder;
    private bool inited=false;
    public GameObject container;
    public GameObject cashPrefab;
    public shopScripts shsc;
    [SerializeField] private Sprite[] cashImages;
    private int cashImageCount = 0;
    void Start()
    {
        if(!inited){
            inited=true;
            addPays();
        }
        
    }

    // Update is called once per frame
    void addPays()
    {
        cashImageCount = 0;
        foreach (paymentItem item in paymentVars.payments.items){
            GameObject newPref = Instantiate(cashPrefab);
            newPref.GetComponent<paymentButtonSpec>().chipsAmount=MoneyConverter.ConvertMoney(long.Parse(item.chips_amount));
            #if UNITY_WEBGL
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().itemId=item.stripe_product_id;
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().cashImage.sprite = cashImages[cashImageCount];

#endif

#if UNITY_IOS
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().itemId=item.apple_product_id;
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().cashImage.sprite = cashImages[cashImageCount];

#endif

#if UNITY_ANDROID
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().itemId=item.google_product_id;
                newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().cashImage.sprite = cashImages[cashImageCount];

#endif

            newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().shsc=shsc;
            newPref.GetComponent<paymentButtonSpec>().buttonclick.GetComponent<paymentSpec>().apiHolder=apiHolder;
            newPref.GetComponent<paymentButtonSpec>().priceAmount="$"+ float.Parse(item.price);
            newPref.transform.SetParent(container.transform);
            newPref.transform.localPosition = Vector3.zero;
            newPref.transform.localScale = Vector3.one;
            cashImageCount++;
        }
    }
}
