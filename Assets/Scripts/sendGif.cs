using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendGif : MonoBehaviour
{
    // Start is called before the first frame update
    public void sendGifToUser(){
        webSocetConnect.UIWebSocket.UISendGif(webSocetConnect.UIWebSocket.MainTable.gameId,spriteSpics.currentId,openGif.SendGifPlayerId);
    }
    public void sendGifToAllUsers(){
        webSocetConnect.UIWebSocket.UISendGif(webSocetConnect.UIWebSocket.MainTable.gameId,spriteSpics.currentId,"-1");
    }
}
