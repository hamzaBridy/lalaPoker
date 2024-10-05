using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideNoti : MonoBehaviour
{
    public GameObject noti;

    // Update is called once per frame
    void Update()
    {
        if(ProfileInfo.MyPlayer.notifications_count=="0"){
            noti.SetActive(false);
        }
        else{    
            noti.SetActive(true);
        }
    }
}
