using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notContainerScript : MonoBehaviour
{
    [SerializeField] GameObject notePrefab;
    [SerializeField] APIcalls aPIcallsRef;

    private void Start()
    {
        webSocetConnect.UIWebSocket.OnGetMessages += gotmsg;
        webSocetConnect.UIWebSocket.OnFriendRequestSent += GotFriendReq;
        webSocetConnect.UIWebSocket.OnWinRoyalFlushLobby += SomeoneWonRF;


    }

    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.OnGetMessages -= gotmsg;
        webSocetConnect.UIWebSocket.OnFriendRequestSent -= GotFriendReq;
        webSocetConnect.UIWebSocket.OnWinRoyalFlushLobby -= SomeoneWonRF;

    }
    void gotmsg()
    {
        Debug.Log("GotPrivateMsg");
        if(webSocetConnect.UIWebSocket.MessageData.sender_id!=PrivateChateManager.CurrentChateId){
        GameObject msgPrefab = Instantiate(notePrefab, transform);

        msgPrefab.GetComponent<fastNotePrefab>().isFriendReq = false;
        msgPrefab.GetComponent<fastNotePrefab>().textinfo = webSocetConnect.UIWebSocket.MessageData.sender_name + "has send you a Massage";
        msgPrefab.GetComponent<fastNotePrefab>().aPIcallsRef = aPIcallsRef;

            msgPrefab.GetComponent<fastNotePrefab>().SetParam();
        }
    }
    void GotFriendReq()
    {
        Debug.Log("GotFriendInstan");
        GameObject reqPrefab = Instantiate(notePrefab,transform);
        reqPrefab.GetComponent<fastNotePrefab>().aPIcallsRef = aPIcallsRef;
        reqPrefab.GetComponent<fastNotePrefab>().isFriendReq = true;
        reqPrefab.GetComponent<fastNotePrefab>().textinfo = webSocetConnect.UIWebSocket.Requestinfo.senderName + "has send you a Friend Request";
        reqPrefab.GetComponent<fastNotePrefab>().friendShipId = webSocetConnect.UIWebSocket.Requestinfo.friendshipId;
        reqPrefab.GetComponent<fastNotePrefab>().imageUrl = webSocetConnect.UIWebSocket.Requestinfo.senderImage;

        reqPrefab.GetComponent<fastNotePrefab>().SetParam();
    }
    void SomeoneWonRF(string imageURL, string userName, string gameId)
    {
        Debug.Log("WonRF");
        if(!GameVariables.isOnTableScene)
        {
            GameObject reqPrefab = Instantiate(notePrefab, transform);
            reqPrefab.GetComponent<fastNotePrefab>().aPIcallsRef = aPIcallsRef;
            reqPrefab.GetComponent<fastNotePrefab>().imageUrl = imageURL;
            reqPrefab.GetComponent<fastNotePrefab>().isFriendReq = false;
            reqPrefab.GetComponent<fastNotePrefab>().textinfo = userName + "has Won a ROYAL FLUSH";
            reqPrefab.GetComponent<fastNotePrefab>().SetParam();
        }

        else if (webSocetConnect.UIWebSocket.MainTable != null)
        {
            if (gameId != webSocetConnect.UIWebSocket.MainTable.gameId)
            {
                GameObject reqPrefab = Instantiate(notePrefab, transform);
                reqPrefab.GetComponent<fastNotePrefab>().aPIcallsRef = aPIcallsRef;
                reqPrefab.GetComponent<fastNotePrefab>().isFriendReq = false;
                reqPrefab.GetComponent<fastNotePrefab>().textinfo = userName + "has Won a ROYAL FLUSH";
                reqPrefab.GetComponent<fastNotePrefab>().SetParam();
            }
        }
    }
}
