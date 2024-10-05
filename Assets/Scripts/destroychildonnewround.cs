using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroychildonnewround : MonoBehaviour
{
    private void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart += DestroyAllChildren;
    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart -= DestroyAllChildren;

    }
    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
