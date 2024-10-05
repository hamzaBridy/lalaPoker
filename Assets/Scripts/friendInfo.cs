using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;
public class friendInfo : MonoBehaviour
{
    [SerializeField] private RawImage myImage;
    [SerializeField] private TextMeshProUGUI userNameC, lastSeenC;
    [SerializeField] private Button joinButton, chatButton,inviteButton;
    public string friendShipId;
    public string friendId;
    public string imageUrl, lastSeen;
    public string friendGameId;
    public string userName;
    public bool isOnline = false;
    public APIcalls aPIcalls;
    public webSocetConnect webSocetConnectRef;
    [SerializeField] private FriendStatuesScript friendStatuesScript;
    [SerializeField] private Sprite defaultSprite;
    public static  event Action OnOpenChat;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => aPIcalls.getStatuesWrapper(friendId, true));
        joinButton.onClick.AddListener(JoinButton);
        chatButton.onClick.AddListener(ChatButton);
        inviteButton.onClick.AddListener(InviteButton);

    }
    private void JoinButton()
    {
        if (friendGameId != null)
            webSocetConnectRef.joinGameWrapper(friendGameId);

    }
    private void ChatButton()
    {
        aPIcalls.getprivatechatWrapper(friendId);
        sendMessage.id=friendId;
        SendTableFriendMessage.id=friendId;
        PrivateChateManager.CurrentChateId=friendId;
        PrivateTableChat.CurrentChateId=friendId;
        OnOpenChat?.Invoke();

    }
    private void InviteButton()
    {

        webSocetConnect.UIWebSocket.InviteToLobby(webSocetConnect.UIWebSocket.MainTable.gameId, friendId);
    }
    public void setparam()
    {
        userNameC.text = userName;
        lastSeenC.text = LastSeenFormatter.FormatLastSeen(lastSeen);
        friendStatuesScript.SetStatues(isOnline);
        ManageButtons();
        if(isOnline)
        {
            lastSeenC.gameObject.SetActive(false);
        }
        else
        {
            lastSeenC.gameObject.SetActive(true);

        }
        StartCoroutine(LoadPhoto(myImage, imageUrl));
    }
    private void ManageButtons()
    {
       

        if (friendGameId != "")
        {
            joinButton.gameObject.SetActive(true);
        }
        else
        {
            joinButton.gameObject.SetActive(false);
        }
        
        if(isOnline)
        {
            chatButton.gameObject.SetActive(true);
            inviteButton.gameObject.SetActive(true);
        }
        else
        {
            inviteButton.gameObject.SetActive(false);
        }
        if (GameVariables.isOnTableScene && isOnline)
        {
            if(!GameVariables.isSpectating) 
            inviteButton.gameObject.SetActive(true);
        }
        else
        {
            inviteButton.gameObject.SetActive(false);
        }
        if (friendGameId != "" && webSocetConnect.UIWebSocket.MainTable!= null)
        if(friendGameId == webSocetConnect.UIWebSocket.MainTable.gameId)
        {
            joinButton.gameObject.SetActive(false);
            chatButton.gameObject.SetActive(false);
            inviteButton.gameObject.SetActive(false);
        }
       

    }
    private IEnumerator LoadPhoto(RawImage profileImage, string imageUrl)
    {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D photoTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite photoSprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f));

                profileImage.texture = photoSprite.texture;

            }
        
    }

}
