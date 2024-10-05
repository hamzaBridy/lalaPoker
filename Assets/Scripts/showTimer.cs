using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showTimer : MonoBehaviour
{
    public Sprite timer;
    JplayerData parentPlayer;
    public List<List<Sprite>> timerSps;
    public List<Sprite> timerImages;
    public List<Sprite> timerImages2;
    public List<Sprite> timerImages3;
    public List<Sprite> timerImages4;
    public List<Sprite> timerImages5;
    public List<Sprite> curentTimer;
    public float totalAnimationTime = 50;
    public bool isMyPlayer = false;
    private Coroutine timerCoroutine;
    void Start()
    {
        timerSps=new List<List<Sprite>>();
        parentPlayer = transform.parent.GetComponent<PlayerGameId>().player;
        curentTimer=timerImages;
        timerSps.Add(timerImages);
        timerSps.Add(timerImages2);
        timerSps.Add(timerImages3);
        timerSps.Add(timerImages4);
        timerSps.Add(timerImages5);
        curentTimer=timerSps[int.Parse(parentPlayer.timerId)-1];
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove+=timerChange;
        webSocetConnect.UIWebSocket.MainTable.OnWin+=disaplTimer;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart+=disaplTimer;
    }
    public void timerChange(){
        //Debug.Log(transform.parent.GetComponent<PlayerGameId>().player.userId);
        if(transform.parent.GetComponent<PlayerGameId>().player.userId==webSocetConnect.UIWebSocket.MainTable.currentPlayerId){
            disaplTimer();
            showTime();
        }
        else{
            disaplTimer();
        }
    }

    // Update is called once per frame
    public void showTime()
    {
        GetComponent<UnityEngine.UI.Image>().enabled=true;
        GetComponent<UnityEngine.UI.Image>().sprite=timer;
        timerCoroutine = StartCoroutine(PlayTimerAnimation());
    }
    public void disaplTimer(){
        GetComponent<UnityEngine.UI.Image>().enabled=false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        GetComponent<UnityEngine.UI.Image>().sprite=timer;
    }
    public void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove-=timerChange;
        webSocetConnect.UIWebSocket.MainTable.OnWin-=disaplTimer;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart-=disaplTimer;
    }
    IEnumerator PlayTimerAnimation()
    {
        bool oneVibrate = true;
        bool isAndro = false;
#if (UNITY_IOS || UNITY_ANDROID)
    isAndro = true;
#endif
        float frameDelay = totalAnimationTime / curentTimer.Count;
        int frameCounter = 0; // To track the progress
        float nowTime=Time.time;
        float accuringTime=Time.time;
        UnityEngine.UI.Image imageComponent = gameObject.GetComponent<UnityEngine.UI.Image>();
        imageComponent.color=new Color(imageComponent.color.r,imageComponent.color.g,imageComponent.color.b,1f);
        float flashDuration = 0.2f; // Duration for each flash cycle
        float flashTimer = 0;
        Color originalColor = imageComponent.color;
        while(accuringTime- nowTime< totalAnimationTime)
       // foreach (var im in curentTimer)
        {
            accuringTime = Time.time;
            float passedTime = accuringTime - nowTime;
            int currentFrame = (int)((passedTime * curentTimer.Count) / totalAnimationTime);
            if (currentFrame >= curentTimer.Count-1) 
            {
                currentFrame = curentTimer.Count-1;
            }
            //frameCounter++;
            frameCounter = currentFrame;

            if (imageComponent.enabled)
            {
                imageComponent.sprite = curentTimer[currentFrame];
            }

           // Check if 70% of the timer has elapsed
           if(playerSettings.settings.viber)
            if (isMyPlayer)
            {
                if (isAndro && oneVibrate && (frameCounter >= curentTimer.Count * 0.7))
                {
#if UNITY_IOS || UNITY_ANDROID

                     Handheld.Vibrate();
#endif

                        oneVibrate = false;
                }
            }

            if (isMyPlayer && frameCounter >= curentTimer.Count * 0.6)
        {
            // Flash the timer
            flashTimer += Time.deltaTime / flashDuration;
            float alpha = Mathf.Lerp(1, 0, Mathf.PingPong(flashTimer, 1));
            imageComponent.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
            yield return new WaitForSeconds(frameDelay);
            //yield return null;
        }

        // Ensure the last frame is displayed
        imageComponent.sprite = timer;
    }

}
