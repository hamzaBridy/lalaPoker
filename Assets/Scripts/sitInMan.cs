using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sitInMan : MonoBehaviour
{
    public GameObject[] sitInContainer9, sitInContainer5;

    private void Start()
    {
        trueSitInImages();
        webSocetConnect.UIWebSocket.MainTable.OnMainPlayerJoind += SitInImagesfalse;
        webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft += trueSitInImages;

    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnMainPlayerJoind -= SitInImagesfalse;
        webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft -= trueSitInImages;
    }
    private void SitInImagesfalse()
    {
        if (GameVariables.gameSize == "9")
        {
            foreach (GameObject item in sitInContainer9)
            {
                item.SetActive(false);
            }
        }
        else if (GameVariables.gameSize == "5")
        {
            foreach (GameObject item in sitInContainer5)
            {
                item.SetActive(false);
            }
        }
    }
    private void trueSitInImages()
    {
        if(GameVariables.gameSize == "9")
        {
            foreach (GameObject item in sitInContainer9)
            {
                item.SetActive(true);
            }
        }
        else if (GameVariables.gameSize == "5")
        {
            foreach (GameObject item in sitInContainer5)
            {
                item.SetActive(true);
            }
        }

    }
}
