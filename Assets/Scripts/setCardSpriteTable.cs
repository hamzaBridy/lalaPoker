using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setCardSpriteTable : MonoBehaviour
{
    public int cardtableId;
    private Color originalColor;
    public Sprite backSp;
    public Sprite frontSp;
    private bool isFlipped = false;
    public GameObject animationPosition1,animationPosition2;
    public float speed = 0.5f;
    private float duration =0.3f;
    public GameObject border;
    public bool isPlayerCard = false;
     private AudioSource cardSound;
     private Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.rotation;
        originalColor = GetComponent<UnityEngine.UI.Image>().color;
        webSocetConnect.UIWebSocket.MainTable.OnWin+=winTableSp;
        StartCoroutine(FlipCardAnimation(frontSp));   
    }
    private void OnEnable()
    {
        if(!isPlayerCard && playerSettings.settings.sound)
        {
            cardSound = GetComponent<AudioSource>();
            cardSound.Play();
        }
        
    }
    void winTableSp(){
        List<int> winPrio=new List<int>();
        foreach (var item in webSocetConnect.UIWebSocket.MainTable.tableCards[cardtableId].IsWiningCard)
        {
            winPrio.Add(item.Key);
        }
        if(winPrio.Count>0){
            foreach (var item in winPrio)
            {
                StartCoroutine(potDelay(GameVariables.maxpotProi-item));
            }
            
        
        }
        else{
            border.GetComponent<UnityEngine.UI.Image>().enabled=false;
            // GetComponent<UnityEngine.UI.Image>().color=originalColor;
        }
    }
        IEnumerator potDelay(float delayTime)
{
   yield return new WaitForSeconds(2*delayTime);
//    GetComponent<UnityEngine.UI.Image>().color=new Color(1f, 0f, 0f, 0.5f);
    border.GetComponent<UnityEngine.UI.Image>().enabled=true;
    yield return new WaitForSeconds(1.5f);
    border.GetComponent<UnityEngine.UI.Image>().enabled=false;
}

IEnumerator FlipCardAnimation(Sprite newCardImage)
    {

        float startTime = Time.time;
        Vector3 startingPos = transform.position;
        Vector3 targetPos = animationPosition1.transform.position;
        float distance = Vector3.Distance(startingPos, targetPos);
        while (Time.time - startTime < duration) 
        {
            float t = (Time.time - startTime) / duration; // Normalized time between 0 and 1

            // Use lerp to interpolate between starting and target positions
            transform.position = Vector3.Lerp(startingPos, targetPos, t);

            yield return null;
        }
        transform.position = targetPos;



        startTime = Time.time;
        startingPos = transform.position;
        targetPos = animationPosition2.transform.position;
        distance = Vector3.Distance(startingPos, targetPos);
        while (Time.time - startTime < duration) 
        {
            float t = (Time.time - startTime) / duration; // Normalized time between 0 and 1

            // Use lerp to interpolate between starting and target positions
            transform.position = Vector3.Lerp(startingPos, targetPos, t);

            yield return null;
        }
        transform.position = targetPos;




        
        if (isFlipped)
        {
            GetComponent<Image>().sprite = newCardImage;
            yield break;
        }

        
        
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.Rotate(Vector3.up, 90f / duration * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isFlipped = !isFlipped;
        GetComponent<Image>().sprite = newCardImage;

        while (elapsed > 0f)
        {
            transform.Rotate(Vector3.up, -90f / duration * Time.deltaTime);
            elapsed -= Time.deltaTime;
            yield return null;
        }
        transform.rotation = originalRotation;
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnWin-=winTableSp;   
    }

    
}
