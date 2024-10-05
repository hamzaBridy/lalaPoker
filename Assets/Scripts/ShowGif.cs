using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowGif : MonoBehaviour
{
    public GameObject gifHolder;
    public GameObject prefab;
    public float frameTime = 0.1f;
    public GameObject playerManager;
    public GameObject FrontBage;
    void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnGifSend+=showGif;
    }

    // Update is called once per frame
    public void showGif()
    {
        Transform reciverTransform=transform;
        Transform senderTransform=transform;
        List<GameObject> allPlayers=playerManager.GetComponent<PlayersManager>().allPlayers;
        GameGifSent gifData=webSocetConnect.UIWebSocket.MainTable.gifInfo;
        if(gifData.receiverId!="-1"){
        foreach(GameObject player in allPlayers){
            if(player.GetComponent<PlayerGameId>().player.userId==gifData.senderId){
                senderTransform=player.GetComponent<PlayerGameId>().GifDestination.transform;
            }
            if(player.GetComponent<PlayerGameId>().player.userId==gifData.receiverId){
                reciverTransform=player.GetComponent<PlayerGameId>().GifDestination.transform;
            }
        }
        StartCoroutine(PlayAnimation(gifHolder.GetComponent<GIFsList>().allGifLists[int.Parse(gifData.gifId)],senderTransform,reciverTransform));
        }
        else{
            foreach(GameObject player in allPlayers){
            if(player.GetComponent<PlayerGameId>().player.userId==gifData.senderId){
                senderTransform=player.GetComponent<PlayerGameId>().GifDestination.transform;
            }
        }
            foreach(GameObject player in allPlayers){
            
                reciverTransform=player.GetComponent<PlayerGameId>().GifDestination.transform;
                StartCoroutine(PlayAnimation(gifHolder.GetComponent<GIFsList>().allGifLists[int.Parse(gifData.gifId)],senderTransform,reciverTransform));
        

        }

    }
    }

    IEnumerator PlayAnimation(List<Sprite> spriteList, Transform t1, Transform t2)
    {
        
        prefab.GetComponent<Image>().sprite=spriteList[0];
        GameObject animatedObject = Instantiate(prefab, t1.position, Quaternion.identity);
        animatedObject.transform.position = new Vector3(animatedObject.transform.position.x, animatedObject.transform.position.y, -2);
        Image spriteRenderer = animatedObject.GetComponent<Image>();
        animatedObject.SetActive(true);
        animatedObject.GetComponent<spriteSpics>().id = 0;
        animatedObject.transform.SetParent(FrontBage.transform, false);

        float totalAnimationTime = 1.5f; // Total time to complete the animation
        float timeElapsed = 0f;
        int spriteIndex = 0;

        while (timeElapsed < totalAnimationTime)
        {
            // Update position with ease out effect
            float lerpFactor = EaseOut(timeElapsed / totalAnimationTime);
            if(t2 != null)
            {
                animatedObject.transform.position = Vector3.Lerp(t1.position, t2.position, lerpFactor);

            }
            else
            {
                Destroy(animatedObject);
                yield break;
            }
            // Update sprite based on the elapsed time
            int newSpriteIndex = Mathf.Min(spriteList.Count - 1, (int)(spriteList.Count * lerpFactor));
            if (newSpriteIndex != spriteIndex)
            {
                spriteRenderer.sprite = spriteList[newSpriteIndex];
                spriteIndex = newSpriteIndex;
            }

            // Increment time
            timeElapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }
        timeElapsed = 0f;
        float totalAnimationTime2=1.5f;
        while(timeElapsed<totalAnimationTime2){
            float lerpFactor = EaseOut(timeElapsed / totalAnimationTime2);
            int newSpriteIndex = Mathf.Min(spriteList.Count - 1, (int)(spriteList.Count * lerpFactor));
            if (newSpriteIndex != spriteIndex)
            {
                spriteRenderer.sprite = spriteList[newSpriteIndex];
                spriteIndex = newSpriteIndex;
            }

            // Increment time
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        Destroy(animatedObject);
    }

    // Ease out function
    float EaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }



    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnGifSend-=showGif;
    }
}
