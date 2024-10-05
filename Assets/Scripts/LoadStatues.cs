using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStatues : MonoBehaviour
{
    public APIcalls aPIcallsRef;
   public void Start()
    {
        aPIcallsRef.getStatuesWrapper(ProfileInfo.Player.id);
        aPIcallsRef.GET_BJPPOTWrapper(ProfileInfo.Player.id);
    }
}
