using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
[System.Serializable]
public class GameTable
{
    public event Action OnHandStrengthChange;
    public event Action OnMainPlayerJoind;
    public event Action onMyTurn;
    public event Action OnOtherPlayerJoind;
    public event Action OnPlayerJoind;
    public event Action OnGifSend;
    public event Action OnPrivateCardsDelt;
    public event Action Ifolded;

    public event Action OnNewRoundStart;
    public event Action OnPlaceBet;
    public event Action OnPlayerBetMove;
    public event Action OnGameCardsDelt;

    public event Action OnUserLeft;
    public event Action OnMainUserLeft;
    public event Action OnOtherUserLeft;
    public event Action OnNoChips;
    public event Action OnAutoBuyIn;
    public event Action OnWin;
    public event Action OnGameMessageSent;
    public event Action OnMessageReceived;
    public event Action ChangeDone;
    public GameGifSent gifInfo;
    public string LastMessage;
    public string AllTableMessages="";
    public List<JplayerData> players;
    public Dictionary<string, JplayerData> allPlayers;
    public JplayerData mainPlayer;
    public static string TotalChips = "0";
    public static bool isOnTable = false;
    public string gameId;
    public int currentPlayersCount;
    public string handStrength;
    public string lastMessageSenderId;
    public string lastBetId;
    public string gameSize;
    public string currentPlayerId;
    public string gameType;
    public List<card> tableCards;
    public string minimumRaise;
    public string maximumRaise;
    public string lastPlainMessage;
    List<string> messageColors = new List<string> { "#FF5733", "#FF0000", "#900C3F", "#9200BD", "#2E32FF", "#18CB62", "#FFEC00", "#FF00B9", "#808080" };

    Dictionary<string,string> playerToColor;

    private string GetUniqueColor()
{
    foreach (string color in messageColors)
    {
        if (!playerToColor.ContainsValue(color))
        {
            return color;
        }
    }
    // Handle the case where all colors are already assigned
    throw new InvalidOperationException("All colors are already assigned to players.");
}
    public GameTable(GameData data)
    {
        playerToColor=new Dictionary<string, string>();
        gameId = data.gameId;
        //players=data.data.players;
        tableCards = data.tableCards;
        players = data.players;
        allPlayers = new Dictionary<string, JplayerData>();
        gameSize = data.gameSize;
        gameType = data.gameType;
        GameVariables.gameSize = data.gameSize;
        GameVariables.gameType = data.gameType;
        foreach (JplayerData pD in players)
        {
            pD.playerCards = new List<card>();
            card c1 = new card();
            c1.suit = "-1";
            c1.value = "-1";
            c1.IsWiningCard = new Dictionary<int, bool>();
            card c2 = new card();
            c2.suit = "-1";
            c2.value = "-1";
            c2.IsWiningCard = new Dictionary<int, bool>();
            pD.playerCards.Add(c1);
            pD.playerCards.Add(c2);
            string color = GetUniqueColor();
            playerToColor.Add(pD.userId, color);
            allPlayers.Add(pD.userId, pD);
        }

    }
    public void handStrengthChange(HandStrength newHand)
    {
        handStrength = newHand.handStrength;
        OnHandStrengthChange?.Invoke();
        ChangeDone?.Invoke();
    }
    public void GifSend(GameGifSent data){
        gifInfo=data;
        OnGifSend?.Invoke();
    }
    
    public void newPlayerJoind(JplayerData newPlayer)
    {
        if (newPlayer.userId == ProfileInfo.Player.id)
        {
            newPlayer.IsMainPlayer = true;
            mainPlayer = newPlayer;
            mainPlayer.stack = newPlayer.stack;
            mainPlayer.image = newPlayer.image;
            GameVariables.isSpectating = false;
            OnMainPlayerJoind?.Invoke();
            ProfileInfo.Player.isSit=true;
        }
        else
        {
            players.Add(newPlayer);
            JplayerData playerToUpdate = players.FirstOrDefault(p => p.userId == newPlayer.userId);
            playerToUpdate.stack = newPlayer.stack;
            playerToUpdate.image = newPlayer.image;
            OnOtherPlayerJoind?.Invoke();
        }
        string color = GetUniqueColor();
        playerToColor.Add(newPlayer.userId, color);
        newPlayer.playerCards = new List<card>();
        card c1 = new card();
        c1.suit = "-1";
        c1.value = "-1";
        c1.IsWiningCard = new Dictionary<int, bool>();
        card c2 = new card();
        c2.suit = "-1";
        c2.value = "-1";
        c2.IsWiningCard = new Dictionary<int, bool>();
        newPlayer.playerCards.Add(c1);
        newPlayer.playerCards.Add(c2);
        allPlayers.Add(newPlayer.userId, newPlayer);
        currentPlayersCount = players.Count + 1;
        OnPlayerJoind?.Invoke();
        ChangeDone?.Invoke();
    }
    public void privateCardsDelt(privateCards newCards)
    {
        mainPlayer.playerCards = new List<card>();
        newCards.cards[0].IsWiningCard = new Dictionary<int, bool>();
        newCards.cards[1].IsWiningCard = new Dictionary<int, bool>();
        isOnTable = true;
        GameVariables.isPlaying = true;

        mainPlayer.playerCards.Add(newCards.cards[0]);
        mainPlayer.playerCards.Add(newCards.cards[1]);
        OnPrivateCardsDelt?.Invoke();
        ChangeDone?.Invoke();
    }
    public void TableMessage(GameMessageSent data){
        if(data.message != "")
        {
            LastMessage = "<color=" + playerToColor[data.senderId] + ">" + data.userName + "</color>" + " : " + data.message + "\n";
            lastPlainMessage = data.message;
            AllTableMessages += LastMessage;
            lastMessageSenderId = data.senderId;
            OnMessageReceived?.Invoke();
        }
       
    }
    public void newRoundStart()
    {
        GameVariables.maxpotProi=0;
        foreach (var item in allPlayers)
        {
            item.Value.WiningCompo = null;
            item.Value.HasWon = false;
            item.Value.Wining = new Dictionary<int, string>();
            item.Value.potPrio = new Dictionary<int, bool>();
            item.Value.Wining = new Dictionary<int, string>();
            item.Value.betType = "2m3li";
            foreach (card Citem in item.Value.playerCards)
            {
                Citem.value = "-1";
                Citem.suit = "-1";
                Citem.IsWiningCard = new Dictionary<int, bool>();
            }
        }
        TotalChips = "0";
        OnNewRoundStart?.Invoke();
        ChangeDone?.Invoke();
    }
    public void PlayerOutOfChip(PlayerOutOfChips data)
    {

        OnNoChips?.Invoke();
        ChangeDone?.Invoke();

    }
    public void GameMessageSent(PlayerOutOfChips data)
    {

        OnGameMessageSent?.Invoke();
        ChangeDone?.Invoke();

    }
    public void autoBuyIn(AutoBuyIn data)
    {
        allPlayers[data.userId].stack = data.chips;
        allPlayers[data.userId].chips = data.chips;
        OnAutoBuyIn?.Invoke();

    }
    public void UserLeft(UserLeftGame data)
    {
        bool inVal = true;
        if (mainPlayer != null)
            if (data.userId == mainPlayer.userId)
            {
                mainPlayer = null;
                isOnTable = false;
                ProfileInfo.Player.isSit=false;
                GameVariables.isPlaying = false;

                OnMainUserLeft?.Invoke();
                inVal = false;
            }
        if (inVal)
        {
            players.RemoveAll(player => player.userId == data.userId);
            currentPlayersCount = players.Count + 1;
            OnOtherUserLeft?.Invoke();
        }
        playerToColor.Remove(data.userId);
        allPlayers.Remove(data.userId);
        OnUserLeft?.Invoke();
        ChangeDone?.Invoke();

    }
    public void placeBet(NewBetPlaced bet)
    {
        lastBetId = bet.userId;
        if (bet.userId == ProfileInfo.Player.id)
        {
            mainPlayer.betAmount = bet.betAmount;
            mainPlayer.stack = bet.playerChips;
            mainPlayer.betType = bet.command;
            if (bet.command == "FOLD")
            {
                Ifolded?.Invoke();
            }
        }
        else
        {
            JplayerData playerToUpdate = players.FirstOrDefault(p => p.userId == bet.userId);
            playerToUpdate.betAmount = bet.betAmount;
            playerToUpdate.stack = bet.playerChips;
            playerToUpdate.betType = bet.command;
        }
        if (bet.command == "BIG_BLIND")
        {
            GameVariables.bigBlindAmount = bet.betAmount;
        }

        TotalChips = bet.totalChipsBet;
        TotalChips = bet.totalChipsBet;
        OnPlaceBet?.Invoke();
        ChangeDone?.Invoke();
    }
    public void PlayerBetMove(PlayerBetMoved data)
    {
        currentPlayerId = data.userId;
        GameVariables.minimumRaise = data.minimumRaise;
        GameVariables.maximumRaise = data.maximumRaise;
        GameVariables.callAmount = data.callAmount;
        maximumRaise = data.maximumRaise;
        minimumRaise = data.minimumRaise;
        if (data.userId == ProfileInfo.Player.id)
        {
            onMyTurn?.Invoke();
            GameVariables.isMyTurn = true;
            GameVariables.canRaise = data.canRaise;
        }
        else
        {
            GameVariables.isMyTurn = false;
        }
        OnPlayerBetMove?.Invoke();
        ChangeDone?.Invoke();
    }
    public void manageWining(RoundWinner data)
    {
        foreach (JplayerCardsList playrCL in data.playerCardsList)
        {
            if (allPlayers.ContainsKey(playrCL.userId))
            {
                JplayerData matchingPlayer = allPlayers[playrCL.userId];



                if (matchingPlayer != null)
                {
                    if (matchingPlayer.playerCards == null)
                    {
                        matchingPlayer.playerCards = new List<card>();
                        matchingPlayer.playerCards.Add(new card());
                        matchingPlayer.playerCards.Add(new card());
                    }
                    while (matchingPlayer.playerCards.Count < 2)
                    {
                        matchingPlayer.playerCards.Add(new card());
                    }
                    // reset Data when round start 
                    matchingPlayer.playerCards[0].suit = playrCL.cards[0].suit;
                    matchingPlayer.playerCards[1].suit = playrCL.cards[1].suit;
                    matchingPlayer.playerCards[0].value = playrCL.cards[0].value;
                    matchingPlayer.playerCards[1].value = playrCL.cards[1].value;

                }
            }
        }
     //   Debug.Log(data.potsWithCombs.Count);
        int potPrio = 0;
        foreach (List<JpotsWithCombs> pcl in data.potsWithCombs)
        {
            potPrio += 1;
            foreach (JpotsWithCombs playrCL in pcl)
            {
                
                if (allPlayers.ContainsKey(playrCL.userId))
                {
                    JplayerData matchingPlayer = allPlayers[playrCL.userId];
                    if (playrCL.comboCards != null)
                        CheckCardEq(tableCards, playrCL.comboCards, potPrio);

                    if (matchingPlayer != null)
                    {
                        if (matchingPlayer.playerCards != null)
                            if (playrCL.comboCards != null)
                                CheckCardEq(matchingPlayer.playerCards, playrCL.comboCards, potPrio);
                        // reset Data when round start 
                        matchingPlayer.HasWon = true;
                        matchingPlayer.Wining.Add(potPrio,playrCL.winnings);
                        matchingPlayer.potPrio.Add(potPrio,true);
                        matchingPlayer.WiningCompo = playrCL.comboType;
                    }
                }
            }
        }
        GameVariables.maxpotProi=potPrio;
        foreach (JplayerChips playrCL in data.playerChips)
        {
            if (allPlayers.ContainsKey(playrCL.playerId))
            {
                JplayerData matchingPlayer = allPlayers[playrCL.playerId];

                if (matchingPlayer != null)
                {


                    // reset Data when round start 
                    matchingPlayer.stack = playrCL.chips;
                    matchingPlayer.chips = playrCL.chips;

                }
            }

        }
        ChangeDone?.Invoke();
        OnWin?.Invoke();
    }
    public void CheckCardEq(List<card> l1, List<card> l2, int prio)
    {
        foreach (card item1 in l1)
        {
            foreach (card item2 in l2)
            {
                if (item1.suit == item2.suit && item1.value == item2.value)
                {
                    if (!item1.IsWiningCard.ContainsKey(prio))
                    item1.IsWiningCard.Add(prio,true);
                    if (!item2.IsWiningCard.ContainsKey(prio))
                    item2.IsWiningCard.Add(prio,true);
                }
            }
        }
    }
    public void GameCardsDelt(GameCardsDealt data)
    {
        tableCards = data.cards;
        OnGameCardsDelt?.Invoke();
        ChangeDone?.Invoke();
    }

}
