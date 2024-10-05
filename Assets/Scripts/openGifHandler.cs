using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGifHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gifMenu;
    void Start()
    {
        openGif.OnOpenGifMenu+=openGifMenu;
    }
    public void openGifMenu(){
        gifMenu.SetActive(true);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        openGif.OnOpenGifMenu-=openGifMenu;
    }
}
