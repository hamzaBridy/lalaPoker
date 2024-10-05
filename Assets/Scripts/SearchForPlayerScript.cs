using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SearchForPlayerScript : MonoBehaviour
{
    [SerializeField] APIcalls apicallsRef;
    [SerializeField] GameObject userPrefabe;
    private List<GameObject> users = new List<GameObject>();
    private void Start()
    {
        apicallsRef.OnusersFound += makeUsers;

    }
    private void OnDestroy()
    {
        apicallsRef.OnusersFound -= makeUsers;

    }
    private void OnEnable()
    {
        removeAllusers();
    }
    void makeUsers()
    {
        removeAllusers();
        foreach (ProfileInfo.FriendReq item in ProfileInfo.userSearched)
        {
            GameObject newuser = Instantiate(userPrefabe);
            newuser.transform.SetParent(transform, false);
            newuser.GetComponent<SearchForPlayerPrefab>().aPIcalls = apicallsRef;
            newuser.GetComponent<SearchForPlayerPrefab>().userName = item.user_name;
            newuser.GetComponent<SearchForPlayerPrefab>().imageUrl = item.profile_image_url;
            newuser.GetComponent<SearchForPlayerPrefab>().id = item.id.ToString();
            newuser.GetComponent<SearchForPlayerPrefab>().isFriendReqSent = item.friend_request_sent;
            newuser.GetComponent<SearchForPlayerPrefab>().setparam();

            users.Add(newuser);

        }

    }
    public void removeAllusers()
    {   
        
        users.Clear();
        foreach (Transform child in this.transform)
        {
            SearchForPlayerPrefab user = child.GetComponent<SearchForPlayerPrefab>();
            if (user != null && !child.IsDestroyed())
            {
                Destroy(child.gameObject);
            }
        }
    }

}

