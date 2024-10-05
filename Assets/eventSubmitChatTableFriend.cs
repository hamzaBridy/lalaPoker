using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class eventSubmitChatTableFriend : MonoBehaviour
{
    public GameObject submitButton;
    void Start()
    {
        GetComponent<TMP_InputField>().onSubmit.AddListener(submit);
    }

    void submit(string x){
        submitButton.GetComponent<SendTableFriendMessage>().send();
    }
}
