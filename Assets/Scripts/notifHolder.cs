using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class notifHolder : MonoBehaviour
{
    public GameObject text;
    public Image ProfileImage;
    public string imageUrl;
    public void SetParam()
    {
        StartCoroutine(LoadPhoto(ProfileImage, imageUrl));
    }
    private IEnumerator LoadPhoto(Image profileImage, string imageUrl)
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

            profileImage.sprite = photoSprite;

        }

    }
}
