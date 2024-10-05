using System.Collections;
using System.Collections.Generic;
//using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.SigI;
using UnityEngine;

public class loadingScript : MonoBehaviour
{
    public GameObject mainMenu;
    public APIcalls apiHolder;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.dataIsavilable && webSocetConnect.webSocetIsReady)
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
            apiHolder.SendMyLangWrapper();
            apiHolder.GET_PaymentWrapper();
        }
    }
}
