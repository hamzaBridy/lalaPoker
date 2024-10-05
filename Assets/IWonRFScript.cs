using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWonRFScript : MonoBehaviour
{
    [SerializeField] GameObject blackOverlay, royalFlushAnimation;

    private void Start()
    {
        webSocetConnect.UIWebSocket.OnWinRoyalFush += IWonRF;
    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.OnWinRoyalFush -= IWonRF;

    }
    void IWonRF(int userId, long chipsWon)
    {
        if(userId.ToString() == ProfileInfo.Player.id)
        { blackOverlay.SetActive(true);
            royalFlushAnimation.SetActive(true);
            StartCoroutine(Delay(3f));
        }
    }
    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        blackOverlay.SetActive(false);
        royalFlushAnimation.SetActive(false);
    }
}
