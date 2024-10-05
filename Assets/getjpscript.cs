using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class getjpscript : MonoBehaviour
{
    private string gameType;
    private TextMeshProUGUI myLjp;
    [SerializeField] private webSocetConnect webSocetConnectRef;

    private void Start()
    {
         myLjp = GetComponent<TextMeshProUGUI>();
        gameType = GameVariables.gameType;
        getJP();
        webSocetConnect.UIWebSocket.OnJackpotsUpdated+= getJP;
    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.OnJackpotsUpdated -= getJP;

    }
    void getJP()
    {
        switch (gameType)
        {
            case "1":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_1);
                break;
            case "2":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_2);
                break;
            case "3":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_3);
                break;
            case "4":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_4);
                break;
            case "5":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_5);
                break;
            case "6":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_6);
                break;
            case "7":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.small_pot_7);
                break;
            default:
                Debug.Log("Invalid game type.");
                myLjp.text = "N/A"; // Default text for invalid cases
                break;
        }


    }
}
