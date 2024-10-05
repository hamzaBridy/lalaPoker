using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogInResults : MonoBehaviour
{
    [SerializeField] private GameObject reesults;
    [SerializeField] private TextMeshProUGUI resultsText;
    private void Start()
    {
        APIcalls.onLoginResults += Login;
    }
    private void OnDestroy()
    {
        APIcalls.onLoginResults -= Login;

    }
    void Login(string error)
    {
        reesults.SetActive(true);
        resultsText.text = error;
    }
}
