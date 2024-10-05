using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class eventSubmitChatTable : MonoBehaviour
{
    public GameObject submitButton;
    void Start()
    {
        GetComponent<TMP_InputField>().onSubmit.AddListener(submit);
    }

    void submit(string x){
        Debug.Log("vevo");
        submitButton.GetComponent<sendTableMessage>().OnButtonClick();
    }
}
