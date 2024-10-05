using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenReportPannel : MonoBehaviour
{
    [SerializeField] private GameObject reportPannel, profilePannel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(openReport);

    }

    void openReport()
    {
        reportPannel.SetActive(true);

        profilePannel.SetActive(false);
        reportPannel.GetComponent<ReportButtonScript>().id = profilePannel.GetComponent<ProfilePannel>().id;

    }
}
