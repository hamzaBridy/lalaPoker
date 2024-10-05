using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alternateContent : MonoBehaviour
{
    public GameObject content1,content2,scrollView;
    public void alterCont1(){
        scrollView.GetComponent<ScrollRect>().content=content1.GetComponent<RectTransform>();
    }
    public void alterCont2(){
        scrollView.GetComponent<ScrollRect>().content=content2.GetComponent<RectTransform>();
    }
}
