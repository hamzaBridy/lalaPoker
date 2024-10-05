using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class searchEnterEnabler : MonoBehaviour
{
    public Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onSubmit.AddListener(subFSc);
        // GetComponent<TMP_InputField>().onSubmit.(() => { searchForPlayerScriptRef.removeAllusers(); });
        
    }

    // Update is called once per frame
    void subFSc(string x)
    {
        myButton.onClick.Invoke();
    }
}
