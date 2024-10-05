using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SearchForPlayerPrefab : MonoBehaviour
{
    [SerializeField] private RawImage myImage;
    [SerializeField] private TextMeshProUGUI userNameC, addButtonText;
    [SerializeField] private Button addButton;
    public string id;
    public string imageUrl;
    public string userName;
    public APIcalls aPIcalls;
    [SerializeField] private Sprite defaultSprite;
    public bool isFriendReqSent = false;

    private void Start()
    {
        addButton.onClick.AddListener(() => { aPIcalls.SendFriendRequestWrapper(id);});
        if(addButtonText != null)
        {
            addButton.onClick.AddListener(() => { addButtonText.text = "Request sent"; });

        }
    }
    public void setparam()
    {
        userNameC.text = userName;
        ButtonText();
        StartCoroutine(LoadPhoto(myImage, imageUrl));
    }
    private void ButtonText()
    {
        if(!isFriendReqSent)
        {
            addButtonText.text = "Add Friend";
        }
        else
        {
            addButtonText.text = "Request sent";
        }

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
