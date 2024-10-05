using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potText;

    private void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt += UpdatePot;
        webSocetConnect.UIWebSocket.MainTable.OnWin += UpdatePot;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart += UpdatePot;
        PlayersManager.OnSingleUser+=UpdatePot;

    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt -= UpdatePot;
        webSocetConnect.UIWebSocket.MainTable.OnWin -= UpdatePot;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart -= UpdatePot;
        PlayersManager.OnSingleUser-=UpdatePot;


    }
    private void UpdatePot()
    {
        potText.text = MoneyConverter.ConvertMoney(long.Parse(GameTable.TotalChips));
    }
}
