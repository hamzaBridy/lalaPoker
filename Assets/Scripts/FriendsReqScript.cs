using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FriendsReqScript : MonoBehaviour
{
    public GameObject friendReqPref;
    public GameObject APIContainer;
    private List<GameObject> instantiatedFriendReqs = new List<GameObject>();

    void Start()
    {
        APIcalls.onFriendsReqReady += DoFriendsReq;
        APIcalls.OnFriendRequestProcessed += OnAcceptOrDeclineFriendRequest;

    }
    public void getFriends()
    {
        APIContainer.GetComponent<APIcalls>().GetFriendsWrapper();

    }
    public void DoFriendsReq()
    {
        removeAllFriendsReq();
        foreach (ProfileInfo.FriendReq item in ProfileInfo.MyFriendReq)
        {
            GameObject newFriend = Instantiate(friendReqPref);
            newFriend.transform.SetParent(transform, false);
            newFriend.GetComponent<FriendReqInfo>().aPIcalls = APIContainer.GetComponent<APIcalls>();
            newFriend.GetComponent<FriendReqInfo>().userName = item.user.user_name;
            newFriend.GetComponent<FriendReqInfo>().imageUrl = item.user.profile_image_url;
            newFriend.GetComponent<FriendReqInfo>().friendShipId = item.id.ToString();
            newFriend.GetComponent<FriendReqInfo>().setparam();

            instantiatedFriendReqs.Add(newFriend);

        }
    }
    void OnDestroy()
    {
        APIcalls.onFriendsReqReady -= DoFriendsReq;
        APIcalls.OnFriendRequestProcessed -= OnAcceptOrDeclineFriendRequest;

    }
    public void RemoveFriendRequestPrefab(string friendShipId)
    {
        GameObject prefabToRemove = instantiatedFriendReqs.FirstOrDefault(prefab => prefab.GetComponent<FriendReqInfo>().friendShipId == friendShipId);

        if (prefabToRemove != null)
        {
            instantiatedFriendReqs.Remove(prefabToRemove);
            Destroy(prefabToRemove);
        }
    }
    public void OnAcceptOrDeclineFriendRequest(int friendShipId)
    {
        
        RemoveFriendRequestPrefab(friendShipId.ToString());
    }
    void removeAllFriendsReq()
    {
        instantiatedFriendReqs.Clear();
        foreach (Transform child in this.transform)
        {
            FriendReqInfo friendReqInfo = child.GetComponent<FriendReqInfo>();
            if (friendReqInfo != null && !child.IsDestroyed())
            {
                Destroy(child.gameObject);
            }
        }
    }

}
