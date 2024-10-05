using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenFriends : MonoBehaviour
{
    public GameObject apiCalls;
    public GameObject friends;
    public GameObject friendsChat;
    [SerializeField] FriendsScripts friendsScripts;
    private void OnEnable()
    {
        apiCalls.GetComponent<APIcalls>().GetFriendsWrapper();
        friends.SetActive(true);
        friendsChat.SetActive(false);

    }
    private void OnDisable()
    {
        friendsScripts.removeAllFriends();
    }
}
