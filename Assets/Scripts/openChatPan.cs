using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChatPan : MonoBehaviour
{
    public GameObject chatPan;
    public GameObject friendScroll;
    void Start()
    {
        friendInfo.OnOpenChat+=openCahtPnnel;
    }

    // Update is called once per frame
    void openCahtPnnel()
    {
        chatPan.SetActive(true);
        friendScroll.SetActive(false);
    }
    void OnDestroy()
    {
        friendInfo.OnOpenChat-=openCahtPnnel;
    }
}
