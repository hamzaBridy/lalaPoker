using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class dogMenuScript : MonoBehaviour
{
    [SerializeField] Image dogTimer;
    [SerializeField] GameObject MainDog;
    [SerializeField] GameObject[] allDog;
    [SerializeField] APIcalls aPIcallsRef;
    private void Start()
    {
        // aPIcallsRef.OnGETCollectiblesProgress += updateDog;
        APIcalls.onSpItReady += setDogs;
    }
    void OnEnable()
    {
        aPIcallsRef.GetSpItWrapper();
    }
    private void OnDestroy()
    {
        APIcalls.onSpItReady-=setDogs;
        // aPIcallsRef.OnGETCollectiblesProgress -= updateDog;

    }
    void setDogs(){
        int dogIndex =0;
        foreach (GameObject item in allDog)
        {
            item.SetActive(false);
        }
        foreach (ProfileInfo.Item item in ProfileInfo.SpItems.items)
        {
            Debug.Log(item.id);
            Debug.Log(item.status);
            if(item.status=="active"){
                MainDog.GetComponent<DogPrefabScript>().isActive=true;
                MainDog.GetComponent<DogPrefabScript>().id=item.id;
                MainDog.GetComponent<DogPrefabScript>().isAvalible=true;
                dogTimer.fillAmount=item.progress;
                StartCoroutine(LoadPhoto(MainDog.GetComponent<Image>(),"https://api.lalapoker.com/"+item.image));
            }
            
                allDog[dogIndex].SetActive(true);
                if(item.progress>=100){
                allDog[dogIndex].GetComponent<DogPrefabScript>().isAvalible=true;
                allDog[dogIndex].GetComponent<DogPrefabScript>().id=item.id;
                allDog[dogIndex].GetComponent<Button>().interactable=true;
                }
                else{
                allDog[dogIndex].GetComponent<DogPrefabScript>().isAvalible=false;
                allDog[dogIndex].GetComponent<DogPrefabScript>().id=item.id;
                allDog[dogIndex].GetComponent<Button>().interactable=false;
                }
                StartCoroutine(LoadPhoto(allDog[dogIndex].transform.Find("Dog").GetComponent<Image>(),"https://api.lalapoker.com/"+item.image));
                allDog[dogIndex].GetComponent<DogPrefabScript>().id=item.id;
                allDog[dogIndex].GetComponent<DogPrefabScript>().isActive=false;

                dogIndex+=1;

            
            
            
        }
    }

    private IEnumerator LoadPhoto(Image itemImage, string imageUrl)
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

            itemImage.sprite = photoSprite;

        }

    }

    void updateDog(float fillAmount)
    {
        dogTimer.fillAmount = fillAmount;
    }
}
