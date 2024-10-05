using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FriendsScripts : MonoBehaviour
{
    public GameObject friendPref;
    public GameObject APIContainer;
    private List<GameObject> instantiatedFriends = new List<GameObject>();
    public webSocetConnect webSocetConnectRef;
    [SerializeField] private TextMeshProUGUI friendsCount;

    void Start()
    {
        APIcalls.onFriendsReady += doFriends;
        APIcalls.OnFriendDelete += OnDeleteFriend;
    }

    public void getFriends()
    {
        APIContainer.GetComponent<APIcalls>().GetFriendsWrapper();
    }

    public void doFriends()
    {
        removeAllFriends();
        int FriendsNum = 0;

        // Separate online and offline friends
        List<ProfileInfo.Friend> onlineFriends = new List<ProfileInfo.Friend>();
        List<ProfileInfo.Friend> offlineFriends = new List<ProfileInfo.Friend>();

        foreach (ProfileInfo.Friend item in ProfileInfo.MyFriends)
        {
            if (item.status == "online")
            {
                onlineFriends.Add(item);
            }
            else
            {
                offlineFriends.Add(item);
            }
        }

        // Add online friends first
        FriendsNum += AddFriendsToList(onlineFriends);

        // Add offline friends
        FriendsNum += AddFriendsToList(offlineFriends);

        if (friendsCount)
            friendsCount.text = getTranslate.getWordByDict("Friends: ") + FriendsNum.ToString();
    }

    private int AddFriendsToList(List<ProfileInfo.Friend> friendsList)
    {
        int numAdded = 0;

        foreach (ProfileInfo.Friend item in friendsList)
        {
            GameObject newFriend = Instantiate(friendPref);
            newFriend.transform.SetParent(transform, false);
            newFriend.GetComponent<friendInfo>().webSocetConnectRef = webSocetConnectRef;
            newFriend.GetComponent<friendInfo>().aPIcalls = APIContainer.GetComponent<APIcalls>();
            newFriend.GetComponent<friendInfo>().userName = item.user_name;
            newFriend.GetComponent<friendInfo>().isOnline = item.status == "online";
            newFriend.GetComponent<friendInfo>().lastSeen = item.last_api_action_timestamp;
            newFriend.GetComponent<friendInfo>().friendGameId = item.current_game_id;
            newFriend.GetComponent<friendInfo>().imageUrl = item.profile_image_url;
            newFriend.GetComponent<friendInfo>().friendShipId = item.friendship.id.ToString();
            newFriend.GetComponent<friendInfo>().friendId = item.id.ToString();
            newFriend.GetComponent<friendInfo>().setparam();
            instantiatedFriends.Add(newFriend);
            numAdded++;
        }

        return numAdded;
    }

    public void RemoveFriendPrefab(string friendShipId)
    {
        GameObject prefabToRemove = instantiatedFriends.FirstOrDefault(prefab => prefab.GetComponent<friendInfo>().friendShipId == friendShipId);

        if (prefabToRemove != null)
        {
            instantiatedFriends.Remove(prefabToRemove);
            Destroy(prefabToRemove);
        }
    }

    public void OnDeleteFriend(int friendShipId)
    {
        RemoveFriendPrefab(friendShipId.ToString());
    }

    void OnDestroy()
    {
        APIcalls.onFriendsReady -= doFriends;
        APIcalls.OnFriendDelete -= OnDeleteFriend;
    }

    public void ShowOnline()
    {
        foreach (var friend in instantiatedFriends)
        {
            friend.gameObject.SetActive(friend.GetComponent<friendInfo>().isOnline);
        }
    }

    public void ShowOffline()
    {
        foreach (var friend in instantiatedFriends)
        {
            friend.gameObject.SetActive(!friend.GetComponent<friendInfo>().isOnline);
        }
    }

    public void ShowAll()
    {
        foreach (var friend in instantiatedFriends)
        {
            friend.gameObject.SetActive(true);
        }
    }

    public void removeAllFriends()
    {
        instantiatedFriends.Clear();
        foreach (Transform child in transform)
        {
            friendInfo friendReqInfo = child.GetComponent<friendInfo>();
            if (friendReqInfo != null && !child.IsDestroyed())
            {
                Destroy(child.gameObject);
            }
        }
    }
}
