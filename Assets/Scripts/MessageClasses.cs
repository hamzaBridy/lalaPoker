using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;




[System.Serializable]
public class paymentItem
{
    public string id;
    public string chips_amount;
    public string price;
    public string stripe_product_id;
    public string google_product_id;
    public string apple_product_id;
}
[System.Serializable]
public class pymentItems{
    public List<paymentItem> items;
}


[System.Serializable]
public class FourOfAKina
{
    public string userId;
    public string chips;
   
}

[System.Serializable]
public class GameData
{
    public string gameSize;
    public string gameType;
    public string gameId;
    public List<JplayerData> players;
    public List<card> tableCards;
}

[System.Serializable]
public class NewPlayerJoind
{
    public string gameId;
    public List<JplayerData> playerData;

}
[System.Serializable]
public class ChipsUpdated
{
    public long change;
    public long chips;
    
    public string type;

}
[System.Serializable]
public class ChipsTransferred{
    public string notificationsCount;
}

[System.Serializable]
public class PlayerOutOfChips
{
    public string gameId;
    public string userId;

}
[System.Serializable]
public class JplayerData
{
    public Dictionary<int,bool> potPrio=new Dictionary<int, bool>();
    public bool HasWon;
    public string WiningCompo;
    public Dictionary<int,string> Wining =new Dictionary<int, string>();
    public string betAmount;
    public string chips;
    public bool IsMainPlayer = false;
    public List<card> playerCards;
    public string image;
    public string name;
    public string timerId;
    public string collectibleId;
    public string collectibleimage;
    public int position;
    public string stack;
    public string userId;
    public string betType;

}

[System.Serializable]
public class AutoBuyIn
{
    public string chips;
    public string gameId;
    public string userId;
}
[System.Serializable]
public class card
{
    public Dictionary<int,bool> IsWiningCard =new Dictionary<int, bool>();
    public string suit;
    public string value;
}
[System.Serializable]
public class privateCards
{
    public List<card> cards;
    public string gameId;
}
[System.Serializable]
public class newRoundStarted
{
    public string gameId;
}
[System.Serializable]
public class NewBetPlaced
{
    public string betType;
    public string betAmount;
    public string command;
    public string gameId;
    public string playerChips;
    public string totalChipsBet;
    public string userId;



}
[System.Serializable]
public class PrivateMessageSent
{
    public string message;
    public string notificationsCount;
    public string sender_id;
    public string sender_name;


}
[System.Serializable]
public class FriendRequestNoteInfo
{
    public string senderId;
    public string friendshipId;
    public string senderImage;
    public string notificationsCount;
    public string senderName;

}
[System.Serializable]
public class PlayerBetMoved
{
    public string maximumRaise;
    public string minimumRaise;
    public string gameId;
    public string userId;
    public long callAmount;
    public bool canRaise;
}
[System.Serializable]
public class GameCardsDealt
{
    public List<card> cards;
    public string gameId;

}
[System.Serializable]
public class HandStrength
{
    public string handStrength;
    public string userId;

}
[System.Serializable]
public class UserLeftGame
{
    public string gameId;
    public string userId;

}
[System.Serializable]
public class JplayerCardsList
{
    public string userId;
    public List<card> cards;


}
[System.Serializable]
public class GameMessageSent
{
    public string senderId;
    public string gameId;
    public string message;
    public string userName;
    

}
[System.Serializable]
public class GameGifSent
{
    public string gifId;
    public string gameId;
    public string receiverId;
    public string senderId;
    

}
[System.Serializable]
public class PlayerInvitedToLobby
{
    public string invitingUserId;
    public string notificationsCount;
    public string invitingUserName;
    public string gameId;


}
[System.Serializable]
public class JplayerChips
{
    public string chips;
    public string playerId;


}
[System.Serializable]
public class JpotsWithCombs
{
    public List<card> comboCards;
    public string comboType;
    public string userId;
    public string winnings;
}


[System.Serializable]
public class transes
{
    public List<trans> transfers;
}
[System.Serializable]
public class trans
{
    public string from;
    public string to;
    public string amount;
    public string created_at;
    public NUser sender;
    public NUser receiver;
}
[System.Serializable]
public class NUser
{
    public string user_name;
    public string profile_image_url;
}
[System.Serializable]
public class NotificationsGroup
{
    public List<OneNoti> NoGrop;
}
[System.Serializable]
public class OneNoti
{
    public string message;
    public string image;
    
}

[System.Serializable]
public class RoundWinner
{
    public string gameId;
    public List<JplayerCardsList> playerCardsList;
    public List<JplayerChips> playerChips;
    public List<List<JpotsWithCombs>> potsWithCombs;
    public void ParsePotsWithCombs(string json)
    {
        // Parse the JSON into a JObject
        JObject jsonObject = JObject.Parse(json);

        // Get the "potsWithCombs" property from the JSON
        JToken potsWithCombsToken = jsonObject["data"]["potsWithCombs"];

        // Initialize the potsWithCombs list
        potsWithCombs = new List<List<JpotsWithCombs>>();

        // Iterate through the potsWithCombsToken and parse each inner list
        foreach (JToken innerListToken in potsWithCombsToken)
        {
            List<JpotsWithCombs> innerList = new List<JpotsWithCombs>();

            // Parse each JpotsWithCombs object in the inner list
            foreach (JToken jpotsWithCombsToken in innerListToken)
            {
                JpotsWithCombs jpotsWithCombs = jpotsWithCombsToken.ToObject<JpotsWithCombs>();
                innerList.Add(jpotsWithCombs);
            }

            potsWithCombs.Add(innerList);
        }
    }

}

[System.Serializable]

public class GameMessage<T>
{
    public T data;
    public string type;
}
