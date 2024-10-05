using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class transferMoney : MonoBehaviour
{
    public TMP_InputField text;
    [SerializeField] TMP_Dropdown duplicationDrop;
    public GameObject apiHolder;
    public static string id = "";

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(transfer);
    }

    void transfer()
    {
        string inputValue = text.text;
        string dropdownValue = duplicationDrop.options[duplicationDrop.value].text;

        long multiplier = 1; // Default multiplier
        switch (dropdownValue)
        {
            case "K":
                multiplier = 1000;
                break;
            case "M":
                multiplier = 1000000;
                break;
            case "B":
                multiplier = 1000000000;
                break;
            case "T":
                multiplier = 1000000000000;
                break;
            case "QT":
                multiplier = 1000000000000000;
                break;
            default:
                break;
        }

        long result = long.Parse(inputValue) * multiplier;
        string resultString = result.ToString();
        apiHolder.GetComponent<APIcalls>().TransferMoneyWrap(id, resultString);
        
    }
}
