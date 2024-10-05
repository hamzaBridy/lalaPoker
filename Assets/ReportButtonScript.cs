using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReportButtonScript : MonoBehaviour
{
    [SerializeField] private APIcalls apicallsRef;
    [SerializeField] private TMP_Dropdown MyDropDown;
    [SerializeField] private TMP_InputField ReportText;
    [SerializeField] private GameObject reportDone,reportPannel;
    [SerializeField] private Button ReportButton;
    public string id;

    private void Start()
    {
        ReportButton.onClick.AddListener(Report);
        APIcalls.OnReportDone += ReportDone;
    }
    private void OnDestroy()
    {
        APIcalls.OnReportDone -= ReportDone;

    }
    private void Report()
    {
        apicallsRef.ReportWrapper(webSocetConnect.UIWebSocket.MainTable.gameId, id, MyDropDown.options[MyDropDown.value].text, ReportText.text);
    }
    private void ReportDone()
    {
        reportDone.SetActive(true);
        reportPannel.SetActive(false);
        ReportText.text = string.Empty;
    }
}
