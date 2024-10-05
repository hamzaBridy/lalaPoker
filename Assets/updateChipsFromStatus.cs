using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateChipsFromStatus : MonoBehaviour
{
    public APIcalls apiHilder;
    public void updateOnPress(){
     //   Debug.Log("working");
    //    Debug.Log(getChips.isGrad);
        getChips.isGrad=true;
        apiHilder.getStatuesWrapper(ProfileInfo.MyPlayer.user_id,false);
    }
}
