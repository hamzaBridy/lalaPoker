using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addIdToTrans : MonoBehaviour
{
    public GameObject FriendIdHolder;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(addId);
    }

    // Update is called once per frame
    void addId()
    {
        transferMoney.id=FriendIdHolder.GetComponent<ProfilePannel>().id;
    }
}
