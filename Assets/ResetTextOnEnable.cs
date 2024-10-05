using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetTextOnEnable : MonoBehaviour
{
   [SerializeField] private TMP_InputField m_TextMeshProUGUI;

    private void OnEnable()
    {
        m_TextMeshProUGUI.text = "";
    }
}
