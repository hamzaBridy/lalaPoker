using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyPhotoScript : MonoBehaviour
{
    private Image myPhoto;
    private void Start()
    {
        myPhoto = GetComponent<Image>();
        APIcalls.OnGetStatues += OnEnable;
        AvatarsImageScript.OnPhotoChanged += loadPhoto;
        StartCoroutine(LoadPhoto(myPhoto, ProfileInfo.MyPlayer.user.profile_image_url));
    }
    private void OnEnable()
    {
        myPhoto = GetComponent<Image>();

        StartCoroutine(LoadPhoto(myPhoto, ProfileInfo.MyPlayer.user.profile_image_url));

    }
    private void OnDestroy()
    {
        APIcalls.OnGetStatues -= OnEnable;
        AvatarsImageScript.OnPhotoChanged -= loadPhoto;

    }
    private void loadPhoto(Sprite notused)
    {
        myPhoto = GetComponent<Image>();

        myPhoto.sprite = notused;
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
