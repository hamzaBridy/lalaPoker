using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ProfileInfo : MonoBehaviour
{
    [System.Serializable]
    public class FriendReq
    {
        public int id;
        public int user_id;
        public int friend_id;
        public string status;
        public string created_at;
        public string updated_at;
        public string user_name;
        public string profile_image_url;
        public bool friend_request_sent = false;
        public User user;
    }
    [System.Serializable]
    public class UserLeveled
    { 
        public string level;
    }

    [System.Serializable]
    public class User
    {
        public string timer_id;
        public string vip_status;
        public int id;
        public string user_name;
        public string profile_image_url;
    }

    [System.Serializable]
    public class FriendReqList
    {
        public List<FriendReq> friendReqs;
    }

    public static List<FriendReq> MyFriendReq;
    public static List<FriendReq> userSearched;

    [System.Serializable]
    public class Friend
    {
        public string vip_status;
        public int id;
        public string user_name;
        public string profile_image_url;
        public string status;
        public string current_game_id;
        public Friendship friendship;
        public string last_api_action_timestamp;

    }
    [System.Serializable]
    public class messageList{
        public List<OneMessage> messages;
    }
    [System.Serializable]
    public class OneMessage{
        public string id;
        public string content;
        public string created_at;
        public string user_name;
        public string from_id;
    }

    [System.Serializable]
    public class Friendship
    {
        public int friend_id;
        public int user_id;
        public int id;
    }

    [System.Serializable]
    public class FriendList
    {
        public List<Friend> friends;
    }
    public static List<Friend> MyFriends;

    [System.Serializable]
    public class MyUser{
        public string notifications_count;
        public string token;
        public string profile_image_url;
        public string id;
        public string user_name;
        public bool isSit=false;
        public string timer_id;
        public string vip_status;

    }
    [System.Serializable]
    public class ImageUrl
    {
        public Dictionary<string, object> get ;
        public object set ;
        public bool withCaching ;
        public bool withObjectCaching ;
    }
    [System.Serializable]
    public class Item
    {
        public int id ;
        public string points_required ;
        public string expires_at ;
        public DateTime created_at ;
        public DateTime updated_at ;
        public string image ;
        public string start_date ;
        public ImageUrl Image_url ;
        public string status ;
        public int progress ;
    }
    [System.Serializable]
    public class SpiItemList{
        public List<Item> items;

    }
    public static SpiItemList SpItems;
    public static MyUser Player;
     [System.Serializable]
     public class  Statues
     {

        public long chips;
        public long biggest_win;
        public long experience_points;
        public long bjp_wins_amount;
        public string hands_played;
        public string level;
        public string vip_level;
        public string notifications_count;
        public string bjp_wins;
        public int win_percentage;
        public string user_id;
        public string profile_image_url;
        public string user_name;
        public string friendship_id;
        public bool is_blocked;
        public bool friend_request_sent;
        public User user; // Nested User object
        public string vip_status;
        public void ParseUser(string json)
        {
            // Parse the JSON into a JObject
            JObject jsonObject = JObject.Parse(json);

            // Get the "user" property from the JSON
            JToken userToken = jsonObject["user"];

            // If userToken is not null, parse the user properties
            if (userToken != null)
            {
                user_id = (string)userToken["id"];
                user_name = (string)userToken["user_name"];
                // Add other properties as needed
            }
            else
            {
                Debug.LogError("User object is null or missing in the JSON.");
            }
        }
    }

    public static Statues MyPlayer;
    [System.Serializable]
    public class SlotMachineWon
    {
        public long win;


    }
    public static Pots GamePots;
    [System.Serializable]
    public class Pots
    {
        public long big_pot_1;
        public long small_pot_1;
        public long big_pot_2;
        public long small_pot_2;
        public long big_pot_3;
        public long small_pot_3;
        public long big_pot_4;
        public long small_pot_4;
        public long big_pot_5;
        public long small_pot_5;
        public long big_pot_6;
        public long small_pot_6;
        public long big_pot_7;
        public long small_pot_7;
    }
    [System.Serializable]
    public class LuckyWheel
    {
        public long chips;
    }
    [System.Serializable]
    public class TimeLeft
    {
        public int y;
        public int m;
        public int d;
        public int h;
        public int i;
        public int s;
        public float f;
        public int invert;
        public int days;
        public bool from_string;
    }
    [System.Serializable]
    public class ServerResponse
    {
        public TimeLeft timeLeft;
    }
    public class RewardList
    {
        public List<Reward> rewards;
    }

    [System.Serializable]
    public class RewardData
    {
        public string type;
        public long chips;
        public int level_condition;
        public string hand_condition;
        public int table_size_condition;
        public int id;

    }

    [System.Serializable]
    public class Reward
    {
        public int id;
        public RewardData reward;
        public long chips;
        public string rewardId;

    }
}
