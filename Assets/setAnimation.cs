using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class setAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        
        animator = GetComponent<Animator>();
        if(playerSettings.settings.video){
        webSocetConnect.UIWebSocket.MainTable.OnWin+=winAnime;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart+=winAnimeRest;
        winAnimeRest();
        }
        else{
            animator.enabled=false;
        }
    }
    public void winAnime(){
        if(webSocetConnect.UIWebSocket.MainTable.mainPlayer!= null)
        if(webSocetConnect.UIWebSocket.MainTable.mainPlayer.HasWon){
        animator.SetBool("winBar", true);
        animator.SetBool("idle", false);
        }
        
    }
    public void winAnimeRest(){
       

        animator.SetBool("idle", true);
         animator.SetBool("winBar", false);
        
    }
    void OnDestroy()
    {
        if(playerSettings.settings.video){
        webSocetConnect.UIWebSocket.MainTable.OnWin-=winAnime;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart-=winAnimeRest;
        }
    }
}
