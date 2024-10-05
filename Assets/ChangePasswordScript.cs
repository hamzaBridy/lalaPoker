using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordScript : MonoBehaviour
{
    [SerializeField] private APIcalls aPIcallsRef;
    [SerializeField] TMP_InputField newPass, newPassCon;
    [SerializeField] GameObject confirmation;
    [SerializeField] Button changePassButon;
    [SerializeField] TextMeshProUGUI changeREs;

    private void Start()
    {
        changePassButon.onClick.AddListener(changePassword);
        APIcalls.OnEditPassword += passwordChanged;
    }
    private void OnDestroy()
    {
        APIcalls.OnEditPassword -= passwordChanged;
    }
    
    private void changePassword()
    {
        aPIcallsRef.EditMyPasswordWrapper(newPass.text, newPassCon.text);
    }
    void passwordChanged(string results)
    {
        changeREs.text = results;
        confirmation.SetActive(true);
    }
}
