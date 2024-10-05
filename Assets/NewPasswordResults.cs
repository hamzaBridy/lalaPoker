using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewPassResults : MonoBehaviour
{
    [SerializeField] private GameObject reesults;
    [SerializeField] private TextMeshProUGUI resultsText;
    private void Start()
    {
        APIcalls.OnNewPassError += registred;
    }
    private void OnDestroy()
    {
        APIcalls.OnNewPassError -= registred;

    }
    void registred(string error)
    {
        reesults.SetActive(true);
        resultsText.text = error;
    }

}
