using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifPannelScript : MonoBehaviour
{
    [SerializeField] GameObject friendsMenu, chatMenu,profileMenu,content1,content2,scrollView;
    [SerializeField] Button sendToAll, sendToUser;
    private void OnEnable()
    {
         friendsMenu.SetActive(false);
    chatMenu.SetActive(false);
        profileMenu.SetActive(false);
        content1.SetActive(true);
        content2.SetActive(false);
                scrollView.GetComponent<ScrollRect>().content=content1.GetComponent<RectTransform>();

        if(GameVariables.isSpectating)
        {
            sendToAll.gameObject.SetActive(false);
            sendToUser.gameObject.SetActive(false);

        }
        else
        {
            sendToAll.gameObject.SetActive(true);
            sendToUser.gameObject.SetActive(true);

        }
    }
   
}
