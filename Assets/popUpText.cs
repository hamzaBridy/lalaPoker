using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class popUpText : MonoBehaviour
{
    public GameObject holder;
    public GameObject player;
    public GameObject text;
    private float time=0;
    private bool isCoroutineRunning = false;
    [SerializeField] private int letterNumbers = 20;
    // Start is called before the first frame update
    void Start()
    {
        if (playerSettings.settings.chatBubbles)
        {
            webSocetConnect.UIWebSocket.MainTable.OnMessageReceived += popMessage;
        }
    }
    public void popMessage(){
        if (playerSettings.settings.chatBubbles)
        {
            if (player.GetComponent<PlayerGameId>().player.userId == webSocetConnect.UIWebSocket.MainTable.lastMessageSenderId)
            {
                holder.SetActive(true);
                TextMeshProUGUI textMeshPro = text.GetComponent<TextMeshProUGUI>();

                string originalMessage = webSocetConnect.UIWebSocket.MainTable.lastPlainMessage;

                // Check if the message is longer than 20 characters
                if (originalMessage.Length > 20)
                {
                    // If it is, display the first 20 characters and add three dots
                    textMeshPro.text = originalMessage.Substring(0, letterNumbers) + "...";
                }
                else
                {
                    // If not, display the original message
                    textMeshPro.text = originalMessage;
                }
                time = 0;
                if (!isCoroutineRunning)
                {
                    StartCoroutine(DisText());
                }
            }
        }
       
        
    }
    private IEnumerator DisText(){
        if (playerSettings.settings.chatBubbles)
        {
            isCoroutineRunning = true;
            while (time < 2f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            holder.SetActive(false);
            isCoroutineRunning = false;
        }
    }

    // Update is called once per frame
    
    void OnDestroy()
    {
        if (playerSettings.settings.chatBubbles)
        {
            webSocetConnect.UIWebSocket.MainTable.OnMessageReceived -= popMessage;
        }
    }
}
