using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betTypePrefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        webSocetConnect.UIWebSocket.MainTable.OnWin += destroySelf;

    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnWin -= destroySelf;

    }
    private void destroySelf()
    {
        Destroy(gameObject);
    }
}
