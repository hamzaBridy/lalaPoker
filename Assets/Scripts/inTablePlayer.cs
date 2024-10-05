using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class inTablePlayer 
{
    public bool isMainPlayer;
    public List<string> cards;
    public string GameId;
    public string image;
    public string name;
    public string position;
    public string stack;
    public string userId;
    public void IsCheck(){

    }
    public void IsFold(){

    }
    public void IsCall(){

    }
    public void IsRaise(string amount){

    }
    public void setCards(string Card1,string Card2){

    }
    //1:28:29 PM.759 Received: {"data":{"gameId":"85cc09f8-3b7f-4745-8bb8-b8d238c7c290","playerData":"
    //[{"image":"","name":"guest6","position":"1","stack":"300","userId":6}]"},"type":"NewUserJoinedGame"}
}
