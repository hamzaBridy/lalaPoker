using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditUserName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myUserName;
    [SerializeField] TMP_InputField myInputField;
    [SerializeField] GameObject confirmButton, editButton,imageandtimersPannel,mainMenu;
    [SerializeField] APIcalls aPIcallsRef;
    [SerializeField] GameObject nameTaken;

    private void Start()
    {
        APIcalls.OnEditMyProfile += nameChanged;
        APIcalls.OnEditMyProfileError += nameAlreadyTaken;
        if (nameTaken != null)
        {
            nameTaken.SetActive(false);
        }
        myInputField.gameObject.SetActive(false);
        myUserName.gameObject.SetActive(true);
        confirmButton.SetActive(false);
        if (GetComponent<ProfilePannel>().id == ProfileInfo.Player.id)

        {
            editButton.SetActive(true);
        }
    }
    private void OnEnable()
    {
        myInputField.gameObject.SetActive(false);
        myUserName.gameObject.SetActive(true);
        confirmButton.SetActive(false);

        if (nameTaken != null)
        {
            nameTaken.SetActive(false);
        }
        if (GetComponent<ProfilePannel>().id == ProfileInfo.Player.id)

        {
            editButton.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        APIcalls.OnEditMyProfile -= nameChanged;
        APIcalls.OnEditMyProfileError -= nameAlreadyTaken;

    }
    public void EditButton()
    {
        if(GetComponent<ProfilePannel>().id == ProfileInfo.Player.id) 
        
        {
            if(myUserName.text.StartsWith("guest"))
            {
               
                confirmButton.SetActive(true);
                editButton.SetActive(false);
            }
            else
            {
                myInputField.text = myUserName.text;
                myUserName.gameObject.SetActive(false);
                myInputField.gameObject.SetActive(true);
                confirmButton.SetActive(true);
                editButton.SetActive(false);
            }
           
        }
        


    }
    public void ConfirmButton()
    {
        if(myInputField.text != myUserName.text)
        {
        if (GetComponent<ProfilePannel>().id == ProfileInfo.Player.id)

        {
            aPIcallsRef.EditMyProfileWrapper(myInputField.text);
           
        }
        }
        if(myInputField.text == myUserName.text)
        
        {
             nameChanged();
        }
    }
    
    void nameChanged()
    {
        Debug.Log("Checked");
        myUserName.text = myInputField.text;
        myInputField.gameObject.SetActive(false);
        myUserName.gameObject.SetActive(true);
        confirmButton.SetActive(false);
        editButton.SetActive(true);
        mainMenu.SetActive(true);
        imageandtimersPannel.SetActive(false);
        if (nameTaken != null)
        {
            nameTaken.SetActive(false);
        }
    }
    void nameAlreadyTaken()
    {
        Debug.Log("nameTaken");
        myInputField.gameObject.SetActive(true);
        myUserName.gameObject.SetActive(false);
        confirmButton.SetActive(true);
        editButton.SetActive(false);
        if (nameTaken != null)
        {
            nameTaken.SetActive(true);
        }
    }
}
