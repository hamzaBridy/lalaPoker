using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class scrollDownText : MonoBehaviour
{
    public GameObject scroll;
    public ScrollRect scrollRect;
   string old;
   string newT;
   void Start()
   {
    newT="";
    old=GetComponent<TextMeshProUGUI>().text;
    // GetComponent<TextMeshProUGUI>().on.AddListener(OnTextValueChanged);
   }
   void Update()
   {
    newT=GetComponent<TextMeshProUGUI>().text;
    if(old!=newT){
        OnTextValueChanged();
    }
    old=newT;
    
   }
   void OnTextValueChanged(){
    // Debug.Log("samira");
    // Debug.Log(scroll.GetComponent<Scrollbar>().size);
    // scroll.GetComponent<Scrollbar>().value=-0.2f;
    StartCoroutine(AutoScroll());
   }

   IEnumerator AutoScroll()
    {
        yield return null; // Wait for one frame to ensure the text has been updated.

        float normalizedPosition = 0.0f; // Scroll to the bottom
        scrollRect.verticalNormalizedPosition = normalizedPosition;

        // Optionally, you can add additional delay for better visibility
        yield return new WaitForSeconds(0.1f);

        // Optionally, you can reset the scroll position to the top after a delay
        // yield return new WaitForSeconds(5.0f);
        // scrollRect.verticalNormalizedPosition = 1.0f; // Scroll to the top
    }
}
