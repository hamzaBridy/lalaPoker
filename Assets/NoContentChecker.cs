using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoContentChecker : MonoBehaviour
{
    [SerializeField] public bool notSeach=true;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] string massageDisplied = "No Content Available\n\nThere are currently no items to display. Please check back later or add new content to view here.";
    private void Update()
    {
        if (transform.childCount == 1)
        {
            if(messageText != null && notSeach)
            {
                messageText.text = massageDisplied;
                messageText.gameObject.SetActive(true);
            }
            
        }
        else
        {
            if (messageText != null)
            messageText.gameObject.SetActive(false);
        }
    }
}
