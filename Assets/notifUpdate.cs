using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class notifUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI notText;
    // Start is called before the first frame update
    public void resetText(){
        ProfileInfo.MyPlayer.notifications_count="0";
    }
    // Update is called once per frame
    void Update()
    {
        notText.text=ProfileInfo.MyPlayer.notifications_count;
    }
}
