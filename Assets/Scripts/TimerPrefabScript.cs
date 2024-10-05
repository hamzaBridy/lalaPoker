using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimerPrefabScript : MonoBehaviour
{
    public void SetTimerWrapper(string timerId)
    {
        StartCoroutine(SetTimer(timerId));
    }
    public static Action TimerUpdated;
    IEnumerator SetTimer(string TimerID)
    {
        WWWForm form = new WWWForm();
        form.AddField("timer_id", TimerID);


        UnityWebRequest SetTimerRequest = UnityWebRequest.Post(APIcalls.Host + "/api/set-timer", form);


        SetTimerRequest.SetRequestHeader("Authorization", "Bearer " +ProfileInfo.Player.token);

        SetTimerRequest.SetRequestHeader("Accept", "application/json");


        yield return SetTimerRequest.SendWebRequest();

        //error handling sth sth
        if (SetTimerRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SetTimerRequest.error);
            Debug.Log(SetTimerRequest.downloadHandler.text);
        }
        else
        {
            ProfileInfo.MyPlayer.user.timer_id = TimerID;
            TimerUpdated?.Invoke();
        }
    }
}
