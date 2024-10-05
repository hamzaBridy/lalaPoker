using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class transferInfo : MonoBehaviour
{
    public static transes MyTrans;
    public GameObject transPref;
    public GameObject container;
    void Start()
    {
        APIcalls.OnTrans+=addTranses;
    }
    public void addTranses(){
        foreach (trans item in MyTrans.transfers)
        {
            GameObject tP =Instantiate(transPref);
            tP.GetComponent<transVars>().Fname.GetComponent<TextMeshProUGUI>().text=item.sender.user_name;
            tP.GetComponent<transVars>().Sname.GetComponent<TextMeshProUGUI>().text=item.receiver.user_name;
            tP.GetComponent<transVars>().amount.GetComponent<TextMeshProUGUI>().text=MoneyConverter.ConvertMoney(long.Parse(item.amount));
            tP.GetComponent<transVars>().firstImage = item.sender.profile_image_url;
            tP.GetComponent<transVars>().secondImage = item.receiver.profile_image_url;

            DateTime.TryParseExact(item.created_at, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, System.Globalization.DateTimeStyles.None, out DateTime createdAt);
            tP.GetComponent<transVars>().date.GetComponent<TextMeshProUGUI>().text=createdAt.ToString("yy/MM/dd");
            tP.transform.SetParent(container.transform, false);

        }
    }
    void OnDestroy()
    {
        APIcalls.OnTrans-=addTranses;
    }

    
}
