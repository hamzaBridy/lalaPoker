using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class notificationsHandel : MonoBehaviour
{
    public GameObject notiPref;
    public static NotificationsGroup NGrop; 
    public GameObject container;
    void Start()
    {
        APIcalls.OnNotifi+=GetNotifications;
    }
    
    void OnDestroy()
    {
        APIcalls.OnNotifi-=GetNotifications;
    }
    public void GetNotifications(){
        foreach (OneNoti item in NGrop.NoGrop)
        {
            GameObject pref=Instantiate(notiPref);
            pref.GetComponent<notifHolder>().text.GetComponent<TextMeshProUGUI>().text=item.message;
            pref.GetComponent<notifHolder>().imageUrl = item.image;
            pref.GetComponent<notifHolder>().SetParam();
            pref.transform.SetParent(container.transform, false);
        }
    }
    
}
