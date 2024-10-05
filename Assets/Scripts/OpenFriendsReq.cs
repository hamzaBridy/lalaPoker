using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFriendsReq : MonoBehaviour
{
    public GameObject apiCalls;
    private void OnEnable()
    {
        apiCalls.GetComponent<APIcalls>().GetFriendsReqWrapper();

    }


}
