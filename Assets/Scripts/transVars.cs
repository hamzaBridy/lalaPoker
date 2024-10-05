using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class transVars : MonoBehaviour
{
    public Image FirstImage;
    public Image SecondImage;
    public GameObject Fname;
    public GameObject Sname;
    public GameObject amount;
    public GameObject date;
    public string firstImage , secondImage;
    private void Start()
    {
        StartCoroutine(LoadPhoto(FirstImage, firstImage));
        StartCoroutine(LoadPhoto(SecondImage, secondImage));

    }
    private IEnumerator LoadPhoto(Image profileImage, string imageUrl)
    {
        if (imageUrl != "")
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
}
