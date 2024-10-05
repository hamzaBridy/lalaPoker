using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendStatuesScript : MonoBehaviour
{
    public friendInfo parentScript;
    [SerializeField] private Sprite offlineSprite, onlineSprite;
    [SerializeField] private TextMeshProUGUI statuesText;

    public void SetStatues(bool isOnline)
    {
        if (isOnline)
        {
            this.GetComponent<Image>().sprite = onlineSprite;
            statuesText.text = "Online";
        }
        else
        {

            this.GetComponent<Image>().sprite = offlineSprite;
            statuesText.text = "Offline";
        }
    }
}
