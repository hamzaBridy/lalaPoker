using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchUserScript : MonoBehaviour
{
    [SerializeField] Button searchButton;
    [SerializeField] APIcalls aPIcallsRef;
    [SerializeField] TMP_InputField name;
    [SerializeField] SearchForPlayerScript searchForPlayerScriptRef;
    private void Start()
    {
        searchButton.onClick.AddListener(() => { aPIcallsRef.searchForFriendWraper(name.text); });
        searchButton.onClick.AddListener(() => { searchForPlayerScriptRef.removeAllusers(); });

    }
}
