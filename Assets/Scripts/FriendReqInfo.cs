using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FriendReqInfo : MonoBehaviour
{
    [SerializeField] private RawImage myImage;
    [SerializeField] private TextMeshProUGUI userNameC;
    [SerializeField] private Button acceptButton, declineButton;
    public string friendShipId;
    public string imageUrl;
    public string userName;
    public APIcalls aPIcalls;
    [SerializeField] private  Sprite defaultSprite;
    private void Start()
    {
        acceptButton.onClick.AddListener(AcceptButton);
        declineButton.onClick.AddListener(DeclineButton);
       
    }
    public void setparam()
    {
        userNameC.text = userName;
        StartCoroutine(LoadPhoto(myImage, imageUrl));
    }
    private void AcceptButton()
    {
        aPIcalls.acceptFriendRequestWrapper(friendShipId);
    }
    private void DeclineButton()
    {
        aPIcalls.declineFriendRequestWrapper(friendShipId);
    }
    private IEnumerator LoadPhoto(RawImage profileImage, string imageUrl)
    {
        if (imageUrl == null || imageUrl == "null")
        {
            profileImage.texture = defaultSprite.texture;
        }
        else
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
}
