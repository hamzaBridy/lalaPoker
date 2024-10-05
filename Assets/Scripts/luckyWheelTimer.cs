
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ProfileInfo;

public class luckyWheelTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeRemainingText;
    [SerializeField] APIcalls aPIcallsRef;
    [SerializeField] GameObject timeReamainingTextHolder;
    [SerializeField] Button luckyWheelButton;

    private void Start()
    {
        aPIcallsRef.GetLuckyWheelTimeWrapper();
        aPIcallsRef.OnGetLuckyWheelTime += updateTimer;

    }
    private void OnDestroy()
    {
        aPIcallsRef.OnGetLuckyWheelTime -= updateTimer;

    }
    void updateTimer(ServerResponse response)
    {
        if (luckyWheelButton != null)
        {
            if (response == null)
            {

                luckyWheelButton.interactable = true;
                timeRemainingText.gameObject.SetActive(false);
                timeReamainingTextHolder.gameObject.SetActive(false);
            }
            else if (response != null)
            {
                luckyWheelButton.interactable = false;

                timeRemainingText.gameObject.SetActive(true);
                timeReamainingTextHolder.gameObject.SetActive(true);
                timeRemainingText.text = $"{response.timeLeft.h}: {response.timeLeft.i}: {response.timeLeft.s}";
                StartCoroutine(UpdateTimeEverySecond(response.timeLeft.h, response.timeLeft.i, response.timeLeft.s));

            }
        }
    }
    private IEnumerator UpdateTimeEverySecond(int hours, int minutes, int seconds)
    {
        while (hours > 0 || minutes > 0 || seconds > 0)
        {
            try{
            timeRemainingText.text = $"{hours}: {minutes}: {seconds}";

           

            seconds--;
            if (seconds < 0)
            {
                minutes--;
                seconds = 59;

                if (minutes < 0)
                {
                    hours--;
                    minutes = 59;
                }
            }
            }
            catch{
                Debug.Log("good not");
            }
             yield return new WaitForSeconds(1);
        }

        aPIcallsRef.GetLuckyWheelTimeWrapper();
    }
}
