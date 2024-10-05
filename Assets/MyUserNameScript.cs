using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyUserNameScript : MonoBehaviour
{
   private TextMeshProUGUI myUserName;

   void Start()
   {
    myUserName = GetComponent<TextMeshProUGUI>();
    APIcalls.OnGetStatues+=updateName;
    updateName();
   }
   void OnDestroy()
   {
        APIcalls.OnGetStatues-=updateName;

   }
   void updateName()
   {
    myUserName.text = ProfileInfo.MyPlayer.user.user_name;
   }
}
