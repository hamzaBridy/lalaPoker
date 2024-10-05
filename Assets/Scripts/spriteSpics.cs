using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class spriteSpics : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // Start is called before the first frame update
    [SerializeField] private Image overlay;
    public static string currentId="0";
    public int id;
    public void OnSelect(BaseEventData eventData)
    {
       // GetComponent<UnityEngine.UI.Image>().color=new Vector4(0.3f,0.3f,0.3f,1);
        currentId=id.ToString();
        overlay.gameObject.SetActive(true);
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
       // GetComponent<UnityEngine.UI.Image>().color=new Vector4(1,1,1,1);
        overlay.gameObject.SetActive(false);

    }
}
