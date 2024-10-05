using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CangeImageDueToAspect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite bigImage;
    public Sprite smallImage;
    public int left,top,right,bot;
    Vector4 orginalPadding;
    void Start()
    {
        orginalPadding=GetComponent<UnityEngine.UI.Image>().raycastPadding;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.aspect>1.6f)
        {
            GetComponent<UnityEngine.UI.Image>().sprite = bigImage;
            GetComponent<UnityEngine.UI.Image>().raycastPadding = orginalPadding;
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().sprite = smallImage;
            GetComponent<UnityEngine.UI.Image>().raycastPadding = new Vector4(left,top,bot,right);
        }
    }
}
