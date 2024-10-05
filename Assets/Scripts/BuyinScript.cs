using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyinScript : MonoBehaviour
{
        public Slider buyinslider;

        public TextMeshProUGUI buyInMax, buyInMin, slidrValue, chips;

        public GameObject buyinButton,buyChipsButton;
        public Toggle autoReBuy, buyInAtMax;
        public Transform dealer;
        
    private void Awake()
    {
        
    }
    void Start()
        {
            autoReBuy.onValueChanged.AddListener(autoReBuyValueChanged);
            buyInAtMax.onValueChanged.AddListener(buyInAtMaxValueChanged);
            buyInAtMaxValueChanged(buyInAtMax);
            autoReBuyValueChanged(autoReBuy);
            buyinslider.onValueChanged.AddListener(delegate { OnSliderValueChanged(GameVariables.gameType); });
            OnSliderValueChanged(GameVariables.gameType);
            buyinslider.value = buyinslider.minValue;

        }
    private void OnEnable()
    {   float x = dealer.localPosition.x;
        float y = dealer.localPosition.y;
        dealer.localPosition = new Vector3(x, y, 0f);
    }
    private void OnDisable()
    {
        float x = dealer.localPosition.x;
        float y = dealer.localPosition.y;
        dealer.localPosition = new Vector3(x, y, -1f);
    }
    private void autoReBuyValueChanged(bool isOn)
        {
            if (isOn)
        {
            GameVariables.isAutoRebuy = "1";
        }
        else
        {
            GameVariables.isAutoRebuy = "0";
        }
        }
        private void buyInAtMaxValueChanged(bool isOn)
        {
            if (isOn)
        {
            if (ProfileInfo.MyPlayer.chips >= buyinslider.maxValue)
            {
                buyinslider.value = buyinslider.maxValue;
            }
            else
            {
                buyinslider.value = ProfileInfo.MyPlayer.chips;
            }
            GameVariables.isBuyInMax = "1";
        }
        else
        {
            GameVariables.isBuyInMax = "0";
        }
        }
        void Update()
        {
            slidrValue.text = buyinslider.value.ToString("N0");
            chips.text = ProfileInfo.MyPlayer.chips.ToString("N0");
        if (buyinslider.value > ProfileInfo.MyPlayer.chips)
        {
            buyinButton.SetActive(false);
            buyChipsButton.SetActive(true);
        }
        else
        {
            buyinButton.SetActive(true);
            buyChipsButton.SetActive(false);
        }
        }
         public void plus()
    {
        buyinslider.value += 50000000f;

    }
    public void minus()
    {
        buyinslider.value -= 50000000f;

    }
        private void OnSliderValueChanged(string gametype)
    {
       // string stakes;

        switch (gametype)
        {
            case "1":
                buyinslider.minValue = 300000000;
                buyinslider.maxValue = 1500000000;
                buyInMax.text = "1.5b";
                buyInMin.text = "300m";
                //stakes = "$5m/10m";
                break;
            case "2":
                buyinslider.minValue = 3000000000;
                buyinslider.maxValue = 15000000000;
                buyInMax.text = "15b";
                buyInMin.text = "3b";
                //buyIn = "$3b";
                //stakes = "$50m/100m";
                break;
            case "3":
                buyinslider.minValue = 30000000000;
                buyinslider.maxValue = 150000000000;
                buyInMax.text = "150b";
                buyInMin.text = "30b";
                //buyIn = "$30b";
                //stakes = "$500m/1b";
                break;
            case "4":
                buyinslider.minValue = 100000000000;
                buyinslider.maxValue = 500000000000;
                buyInMax.text = "500b";
                buyInMin.text = "100b";
                //buyIn = "$100b";
               // stakes = "$2b/4b";
                break;
            case "5":
                buyinslider.minValue = 500000000000;
                buyinslider.maxValue = 2500000000000;
                buyInMax.text = "2.5t";
                buyInMin.text = "500b";
                //buyIn = "$500b";
                //stakes = "$10b/20b";
                break;
            case "6":
                buyinslider.minValue = 3000000000000;
                buyinslider.maxValue = 15000000000000;
                buyInMax.text = "15t";
                buyInMin.text = "3t";
                //buyIn = "$3t";
                //stakes = "$50b/100b";
                break;
            case "7":
                buyinslider.minValue = 60000000000000;
                buyinslider.maxValue = 300000000000000;
                buyInMax.text = "300t";
                buyInMin.text = "60t";
                //buyIn = "$60t";
                //stakes = "$100b/200b";
                break;
        }

    }
   
 public void buyIn()
    {
        int originalSeatWanted =0;
        if (GameVariables.gameSize == "9")
        {

            originalSeatWanted = ((GameVariables.seatNumWatned) + 9) % 9;
            if (originalSeatWanted == 0)
            {
                originalSeatWanted = 9;
            }
        }
        if (GameVariables.gameSize == "5")
        {
            originalSeatWanted = ((GameVariables.seatNumWatned) + 5) % 5;
            if (originalSeatWanted == 0)
            {
                originalSeatWanted = 5;
            }
        }
        string buyinAmount = buyinslider.value.ToString("0.########################"); // Convert to string with up to 9 decimal places
   
        webSocetConnect.UIWebSocket.UIBuyIn(originalSeatWanted.ToString(), GameVariables.isAutoRebuy,buyinAmount, webSocetConnect.UIWebSocket.MainTable.gameId);
    }
}
