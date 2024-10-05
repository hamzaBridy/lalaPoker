using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AvatarsImageScript : MonoBehaviour
{
    private Sprite spriteWanted;
    private void Start()
    {
        spriteWanted =  this.GetComponent<Image>().sprite;
        this.GetComponent<Button>().onClick.AddListener(() => { StartCoroutine(EditMyPhoto(spriteWanted)); });
    }
    public static event Action<Sprite>OnPhotoChanged;
    IEnumerator EditMyPhoto(Sprite photoBytess)
    {
        Texture2D photoTexture = new Texture2D(photoBytess.texture.width, photoBytess.texture.height, TextureFormat.RGB24, false);
        photoTexture.SetPixels(photoBytess.texture.GetPixels());

        byte[] photoBytes = photoTexture.EncodeToJPG();
        string photoBase64 = Convert.ToBase64String(photoBytes);

        WWWForm form = new WWWForm();
        form.AddField("profile_image", photoBase64);
        UnityWebRequest www = UnityWebRequest.Post(APIcalls.Host + "/api/user/", form);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            JObject jsonObject = JObject.Parse(www.downloadHandler.text);
            ProfileInfo.MyPlayer.user.profile_image_url=(string)jsonObject["profile_image_url"];
            OnPhotoChanged?.Invoke(photoBytess);
        }
    }
   
}
