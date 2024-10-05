using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageTableOptions : MonoBehaviour
{
    public GameObject standUp;
    void OnEnable()
    {
        if(ProfileInfo.Player.isSit){
            standUp.SetActive(true);
        }
        else{
            standUp.SetActive(false);
            
        }
    }
}
