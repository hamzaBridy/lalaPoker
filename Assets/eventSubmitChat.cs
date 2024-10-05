using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class eventSubmitChat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject submitButton;
    void Start()
    {
        GetComponent<TMP_InputField>().onSubmit.AddListener(submit);
    }

    void submit(string x){
        submitButton.GetComponent<sendMessage>().send();
    }
    
}
