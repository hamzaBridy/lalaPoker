using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class spitemset : MonoBehaviour
{
    public List<Sprite> spitemsList;
    JplayerData parentPlayer;
    public GameObject spimage;
    void Start()
    {
        parentPlayer = transform.parent.GetComponent<PlayerGameId>().player;
        if(parentPlayer.collectibleId!="-1"){
            spimage.SetActive(true);
            spimage.GetComponent<Image>().sprite=spitemsList[int.Parse(parentPlayer.collectibleId)];
            StartCoroutine(LoadPhoto(spimage.GetComponent<Image>(),"https://api.lalapoker.com/"+parentPlayer.collectibleimage));
        }
        else{
            spimage.SetActive(false);
        }
        
    }

    private IEnumerator LoadPhoto(Image Image, string imageUrl)
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

            Image.sprite = photoSprite;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
