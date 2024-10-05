using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class fastNotePrefab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textinfoC;
    [SerializeField] Button acceptButton, declineButton;
    [SerializeField] RawImage profileImage;
    public string imageUrl;
    public APIcalls aPIcallsRef;
    public string friendShipId;
    public bool isFriendReq = false;
    public string textinfo;
    public void SetParam()
    {
        textinfoC.text = textinfo;
        StartCoroutine(LoadPhoto(profileImage, imageUrl));
        if (isFriendReq )
        {
            acceptButton.gameObject.SetActive(true);
            acceptButton.onClick.AddListener(() => { aPIcallsRef.acceptFriendRequestWrapper(friendShipId); });
            declineButton.gameObject.SetActive(true);
            declineButton.onClick.AddListener(() => { aPIcallsRef.declineFriendRequestWrapper(friendShipId); });

        }

    }
    private void Start()
    {
        APIcalls.OnFriendRequestProcessed += destroySelf;
        StartCoroutine(Destroy());
    }
    private void OnDestroy()
    {
        APIcalls.OnFriendRequestProcessed -= destroySelf;

    }
    public void destroySelf(int dosentmatter)
    {
        Destroy(gameObject);

    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
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
