using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class registerResultsScript : MonoBehaviour
{
    [SerializeField] private GameObject reesults;
    [SerializeField] private TextMeshProUGUI resultsText;
    private void Start()
    {
        APIcalls.onRegister += registred;
    }
    private void OnDestroy()
    {
        APIcalls.onRegister -= registred;

    }
    void registred(string error)
    {
        reesults.SetActive(true);
        resultsText.text = error;
    }

}
