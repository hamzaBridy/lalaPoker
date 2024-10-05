using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGif : MonoBehaviour
{
    // Start is called before the first frame update
    
    JplayerData parentPlayer;
    public static event Action OnOpenGifMenu;
    public static string SendGifPlayerId;
    
    void Start()
    {
        parentPlayer = transform.parent.GetComponent<PlayerGameId>().player;
    }

    // Update is called once per frame
    public void onClickFun(){
        SendGifPlayerId=parentPlayer.userId;
        OnOpenGifMenu?.Invoke();
    } 
}
