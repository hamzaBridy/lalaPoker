using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class didnotSearchYet : MonoBehaviour
{
    public static bool searched=false;
    public GameObject friendText;
    public NoContentChecker seachCont;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(searched);
        if(searched){
        friendText.SetActive(true);
        seachCont.notSeach=true;
        }
        else{
            seachCont.notSeach=false;
            friendText.SetActive(false);
        }
    }
}
