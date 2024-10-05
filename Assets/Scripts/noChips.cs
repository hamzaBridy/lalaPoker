using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noChips : MonoBehaviour
{
    public GameObject buyInMenu;
    void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnNoChips+=openbuyIn;    
    }
    void openbuyIn(){
        buyInMenu.SetActive(true);
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnNoChips-=openbuyIn;  
    }
    // Update is called once per frame
    
}
