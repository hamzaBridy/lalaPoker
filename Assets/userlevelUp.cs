using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userlevelUp : MonoBehaviour
{
    [SerializeField] private GameObject userLeveledUpPrefabe;
    private void Start()
    {
        webSocetConnect.UIWebSocket.OnUserLeveledUp += iLeveledUp;

    }

    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.OnUserLeveledUp -= iLeveledUp;

    }
    private void iLeveledUp(string newLevel)
    {
        GameObject levelUpPrefab = Instantiate(userLeveledUpPrefabe,transform);
        levelUpPrefab.GetComponent<userlevelUpPrefabScript>().setParam(newLevel);

    }
}
