using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersScript : MonoBehaviour
{
    public GameObject[] TimersIDs;
    [SerializeField] private Sprite checkMark, locked;
   
    private void Start()
    {
        TimerPrefabScript.TimerUpdated += Chose_Timer;
        
    }
    private void OnEnable()
    {
        UpdateToggleAvalibility(ProfileInfo.MyPlayer.vip_status != "");
        Debug.Log(ProfileInfo.MyPlayer.vip_status);
        UpdateToggleImages();
    }
    private void OnDestroy()
    {
        TimerPrefabScript.TimerUpdated -= Chose_Timer;

    }
    private void UpdateToggleAvalibility(bool isVip)
    {
        for (int i = 1; i < TimersIDs.Length; i++)
        {
            if(!isVip)
            {
                TimersIDs[0].GetComponent<TimerPrefabScript>().SetTimerWrapper("1");
            }
            GameObject toggle = TimersIDs[i].transform.Find("Toggled").gameObject;
           if( isVip )
            {
                toggle.SetActive(false);
            }
            else
            {
                toggle.SetActive(true);
                toggle.GetComponent<Image>().sprite = locked;
            }
        }
        LockedTimerUninteractable();


    }

    private void UpdateToggleImages()
    {
        for (int i = 0; i < TimersIDs.Length; i++)
        {
            GameObject toggle = TimersIDs[i].transform.Find("Toggled").gameObject;
            if (i == (int.Parse(ProfileInfo.MyPlayer.user.timer_id) - 1))
            {
                toggle.SetActive(true);
                toggle.GetComponent<Image>().sprite = checkMark;

            }
            else
            {
                if(toggle.GetComponent<Image>().sprite == checkMark)
                {
                    toggle.SetActive(false);

                }
            }
        }
        LockedTimerUninteractable();
    }

    public void Chose_Timer()
    {
        UpdateToggleImages();
    }
    private void LockedTimerUninteractable()
    {
        for (int i = 1; i < TimersIDs.Length; i++)
        {
            GameObject toggle = TimersIDs[i].transform.Find("Toggled").gameObject;
            if (toggle.GetComponent<Image>().sprite == locked)
            {
                TimersIDs[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                TimersIDs[i].GetComponent<Button>().interactable = true;
            }
        }
    }
}
