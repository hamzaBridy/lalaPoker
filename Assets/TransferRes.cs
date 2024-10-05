using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransferRes : MonoBehaviour
{
    [SerializeField] GameObject resultsPanel;
    [SerializeField] TextMeshProUGUI reultsText;
    private void Start()
    {
        APIcalls.OnTransDone += GotResults;

    }
    private void OnEnable()
    {
        resultsPanel.SetActive(false);

    }
    private void OnDestroy()
    {
        APIcalls.OnTransDone -= GotResults;

    }
    void GotResults(string results)
    {
        Debug.Log(results);
        reultsText.text = results;
        resultsPanel.SetActive(true);

    }
}
