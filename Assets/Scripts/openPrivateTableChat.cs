using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPrivateTableChat : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject chatPan;
   public GameObject friends;
    void Start()
    {
        friendInfo.OnOpenChat+=openCahtPnnel;
    }

    // Update is called once per frame
    void openCahtPnnel()
    {
        chatPan.SetActive(true);
        friends.SetActive(false);
    }
    void OnDestroy()
    {
        friendInfo.OnOpenChat-=openCahtPnnel;
    }
}
