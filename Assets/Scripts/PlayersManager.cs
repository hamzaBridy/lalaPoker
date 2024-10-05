using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class PlayersManager : MonoBehaviour
{
  public static event Action OnSingleUser;
  public GameObject myPlayerPrefab, DefaultPlayerPrefab;
  public List<GameObject> playersList;
  public List<GameObject> allPlayers;
  public RectTransform[] ninePlayersPositions, fivePlayerPositions;
  public Transform playersParent;
  public GameObject StartDealingHandler, apiCallsContainer;
    private int test = 0;
   [SerializeField] private GameObject buyinPannel;


void OnEnable()
{
  loadExistingPlayers();
  webSocetConnect.UIWebSocket.MainTable.OnMainPlayerJoind+=MainPlayerJoind;
  webSocetConnect.UIWebSocket.MainTable.OnOtherPlayerJoind+=PlayerJoind;
  // webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft+=MainPlayerLeft;
  webSocetConnect.UIWebSocket.MainTable.OnUserLeft+=PlayerLeft;
  webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart+=newRoundStart;
  // webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove+=isPlaying;
}
void OnDestroy()
{
  webSocetConnect.UIWebSocket.MainTable.OnMainPlayerJoind-=MainPlayerJoind;
  webSocetConnect.UIWebSocket.MainTable.OnOtherPlayerJoind-=PlayerJoind;
  // webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft-=MainPlayerLeft;
  webSocetConnect.UIWebSocket.MainTable.OnUserLeft-=PlayerLeft;
  webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart-=newRoundStart;
  // webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove-=isPlaying;
}
void newRoundStart(){
  StartDealingHandler.GetComponent<dealingStart>().destinationLocations=new List<GameObject>();
  foreach(GameObject player in allPlayers)
  StartDealingHandler.GetComponent<dealingStart>().destinationLocations.Add(player);
  StartDealingHandler.GetComponent<dealingStart>().deal();
}


public void PlayerLeft(){
  List<GameObject> playersToRemove = new List<GameObject>();

    foreach (var player in allPlayers)
    {
        if (!webSocetConnect.UIWebSocket.MainTable.allPlayers.ContainsKey(player.GetComponent<PlayerGameId>().player.userId))
        {
            playersToRemove.Add(player);
        }
    }

    foreach (var playerToRemove in playersToRemove)
    {
        Destroy(playerToRemove);
        allPlayers.Remove(playerToRemove);
        playersList.Remove(playerToRemove);
    }
    StartCoroutine(singleUserRemoveAnimation());
}
IEnumerator singleUserRemoveAnimation(){
  yield return new WaitForSeconds(4);
  if(allPlayers.Count<=1){
      OnSingleUser?.Invoke();
    }
}

public void MainPlayerJoind(){
  newUserJoined(true);
}
public void PlayerJoind(){
  newUserJoined(false);
}
//public void newUserJoined(int myBackendSeat,int userBackendSeat, string userId, string imageUrl, string level,int timerId)

public void newUserJoined(bool isMainPlayer)
{
            string userId;
            int userBackendSeat;
            JplayerData LastPlayer;
            
  if(isMainPlayer){
    userId=webSocetConnect.UIWebSocket.MainTable.mainPlayer.userId;
    userBackendSeat=webSocetConnect.UIWebSocket.MainTable.mainPlayer.position;
    LastPlayer=webSocetConnect.UIWebSocket.MainTable.mainPlayer;
            buyinPannel.SetActive(false);
  }
  else{
    userId=webSocetConnect.UIWebSocket.MainTable.players.Last().userId;
    userBackendSeat=webSocetConnect.UIWebSocket.MainTable.players.Last().position;
    LastPlayer=webSocetConnect.UIWebSocket.MainTable.players.Last();
  }
  
            
              GameObject newPlayer = null;

         if(userId == ProfileInfo.Player.id)
         {
            if ((GameVariables.gameSize == "9"))
            {
                GameVariables.seatOffset = (5 - webSocetConnect.UIWebSocket.MainTable.mainPlayer.position);

            }
            else if (GameVariables.gameSize == "5")
            {
                GameVariables.seatOffset = (3 - webSocetConnect.UIWebSocket.MainTable.mainPlayer.position);
            }

                takeSeatInGame();
            newPlayer =  Instantiate(myPlayerPrefab,playersParent);
            newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();
            newPlayer.transform.position = ninePlayersPositions[4].position;
            newPlayer.GetComponent<PlayerGameId>().frontendSeat = 4;
            newPlayer.GetComponent<PlayerGameId>().PlayerId=userId;
            newPlayer.GetComponent<PlayerGameId>().player=LastPlayer;
            allPlayers.Add(newPlayer);
            
            return;
         }
            int frontendSeat = userBackendSeat;
        if (GameVariables.gameSize == "9")
        {


            frontendSeat = ((userBackendSeat + GameVariables.seatOffset) + 9) % 9;
                if (frontendSeat == 0)
                { frontendSeat = 9; }

    }
            else if(GameVariables.gameSize == "5")
            {
            
               
                frontendSeat = ((userBackendSeat + GameVariables.seatOffset) + 5) % 5;
                if (frontendSeat == 0)
                { frontendSeat = 5; }
            
            }
            if (GameVariables.gameSize == "9")
{
    newPlayer =  Instantiate(DefaultPlayerPrefab,playersParent);
            newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

            newPlayer.transform.position = ninePlayersPositions[frontendSeat-1].position;
            newPlayer.GetComponent<PlayerGameId>().frontendSeat = frontendSeat - 1;

    }
        else if(GameVariables.gameSize == "5")
            {
            newPlayer =  Instantiate(DefaultPlayerPrefab, playersParent);
    newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

            newPlayer.transform.position = fivePlayerPositions[frontendSeat - 1].position;
            newPlayer.GetComponent<PlayerGameId>().frontendSeat = frontendSeat - 1;

        }
newPlayer.GetComponent<PlayerGameId>().PlayerId=userId;
            newPlayer.GetComponent<PlayerGameId>().player=LastPlayer;
            allPlayers.Add(newPlayer);
            playersList.Add(newPlayer);
          
}
//public void loadExistingPlayers(int userBackendSeat, string userId, string imageUrl, string level,int timerId)
public void loadExistingPlayers(){


if(webSocetConnect.UIWebSocket.MainTable.players != null)
{
for(int i=0; i<webSocetConnect.UIWebSocket.MainTable.players.Count;i++)
{
            GameObject newPlayer = null;
                if (GameVariables.gameSize == "9")
                {
                    newPlayer =  Instantiate(DefaultPlayerPrefab, playersParent);
                    newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

                  
                    newPlayer.transform.position = ninePlayersPositions[webSocetConnect.UIWebSocket.MainTable.players[i].position-1].position;
                         newPlayer.GetComponent<PlayerGameId>().frontendSeat = webSocetConnect.UIWebSocket.MainTable.players[i].position - 1;

                         newPlayer.GetComponent<PlayerGameId>().PlayerId=webSocetConnect.UIWebSocket.MainTable.players[i].userId;
                         newPlayer.GetComponent<PlayerGameId>().player=webSocetConnect.UIWebSocket.MainTable.players[i];
                         allPlayers.Add(newPlayer);
                         playersList.Add(newPlayer);

            }
 else if (GameVariables.gameSize == "5")
            {
                newPlayer = Instantiate(DefaultPlayerPrefab, playersParent);
                newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

                newPlayer.transform.position = fivePlayerPositions[webSocetConnect.UIWebSocket.MainTable.players[i].position - 1].position;
                newPlayer.GetComponent<PlayerGameId>().frontendSeat = webSocetConnect.UIWebSocket.MainTable.players[i].position - 1;

                newPlayer.GetComponent<PlayerGameId>().PlayerId = webSocetConnect.UIWebSocket.MainTable.players[i].userId;
                newPlayer.GetComponent<PlayerGameId>().player = webSocetConnect.UIWebSocket.MainTable.players[i];
                allPlayers.Add(newPlayer);
                playersList.Add(newPlayer);

            }
        }
}
}
    //public void takeSeatInGame(int userBackendSeat, string userId, string imageUrl, string level,int timerId)

    public void takeSeatInGame()
    {
        DestroyListElements.DestroyElements(playersList);
        allPlayers = new List<GameObject>();
        playersList = new List<GameObject>();
        test++;
        if (webSocetConnect.UIWebSocket.MainTable.players != null)
        {
            for (int i = 0; i < webSocetConnect.UIWebSocket.MainTable.players.Count; i++)
            {
                if (webSocetConnect.UIWebSocket.MainTable.players[i].userId == ProfileInfo.Player.id)
                {

                    continue;
                }
                int backendseat = webSocetConnect.UIWebSocket.MainTable.players[i].position;
                int frontendSeat = backendseat;
                if (GameVariables.gameSize == "9")
                {

                    int tempFrontSeat = (webSocetConnect.UIWebSocket.MainTable.players[i].position + GameVariables.seatOffset);

                    //if (tempFrontSeat > 9)
                    //{
                    //    tempFrontSeat--;
                    //    Debug.LogError("-----");
                    //}

                    frontendSeat = (tempFrontSeat + 9) % 9;

                    if (frontendSeat == 0)
                    { frontendSeat = 9; }

                     }
                    else if (GameVariables.gameSize == "5")
                    {


                        frontendSeat = ((webSocetConnect.UIWebSocket.MainTable.players[i].position + GameVariables.seatOffset) + 5) % 5;
                        if (frontendSeat == 0)
                        { frontendSeat = 5; }

                    }
                    GameObject newPlayer = null;
                    if (GameVariables.gameSize == "9")
                    {
                        newPlayer = Instantiate(DefaultPlayerPrefab, playersParent);
                        newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

                        newPlayer.transform.position = ninePlayersPositions[frontendSeat - 1].position;
                        newPlayer.GetComponent<PlayerGameId>().frontendSeat = frontendSeat - 1;

                    }
                    else if (GameVariables.gameSize == "5")
                    {
                        newPlayer = Instantiate(DefaultPlayerPrefab, playersParent);
                        newPlayer.GetComponent<PlayerGameId>().aPIcalls = apiCallsContainer.GetComponent<APIcalls>();

                        newPlayer.transform.position = fivePlayerPositions[frontendSeat - 1].position;
                        newPlayer.GetComponent<PlayerGameId>().frontendSeat = frontendSeat - 1;

                    }
                    newPlayer.GetComponent<PlayerGameId>().PlayerId = webSocetConnect.UIWebSocket.MainTable.players[i].userId;
                    newPlayer.GetComponent<PlayerGameId>().player = webSocetConnect.UIWebSocket.MainTable.players[i];
                    allPlayers.Add(newPlayer);
                    playersList.Add(newPlayer);
                }
            }
        } 
    
// public void isPlaying(){
//   foreach (var player in allPlayers)
//   {
//     if(player.GetComponent<PlayerGameId>().PlayerId==webSocetConnect.UIWebSocket.MainTable.currentPlayerId)
//     player.transform.Find("timer").GetComponent<showTimer>().showTime();
//     else
//     player.transform.Find("timer").GetComponent<showTimer>().disaplTimer();
//   }
// }
}


