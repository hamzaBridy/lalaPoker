using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestHTTP.WebSocket;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class webSocetConnect : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }
    private void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //   // UIWebSocket.test();

        //}
    }
    public static bool webSocetIsReady =false;
    public class CustomWebSocet{
        
        string link="wss://websockets.lalapoker.com/game?token=";
        // string link="ws://localhost:81/game?token=";
        public GameTable MainTable;
        public LobbyEvents lobbyevents;
        public event Action OnGetMessages;
        public event Action OnFriendRequestSent;
        public event Action OnChipsUpdated;
        public event Action connectionError;
        public event Action OnJackpotsUpdated;
        public  event Action<string> OnUserLeveledUp;
        public event Action<int,long> OnWinFOAK;
        public event Action<int, long> OnWinRoyalFush;
        public event Action<string, string, string> OnWinRoyalFlushLobby;
        private bool attemptingReconnect = false;
        public PrivateMessageSent MessageData;
        public FriendRequestNoteInfo Requestinfo;
        public bool isreconnecting=false;
        WebSocket webSocket;
        bool isOpen=false;
        public bool waitMessage=false;
        string lastMessage="";
        List<string> messageBuffer=new List<string>();

        public bool connectionEstablished=true;
    //     IEnumerator ReconnectAfterDelay(int delaySeconds)
    // {
    //     Debug.Log($"Attempting to reconnect in {delaySeconds} seconds...");
    //     yield return new WaitForSeconds(delaySeconds);
    //     // Reset the reconnection flag
    //     attemptingReconnect = false;
    //     // UIWebSocket.connectionError-=reconnect;
    //     // Attempt to reconnect
    //     UIWebSocket.ConnectToWebSocket();
    //     // UIWebSocket.connectionError+=reconnect;

    //     // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    // }

        private async Task PingLoop()
    {
       // Debug.Log("dingadinga");
        await Task.Delay(TimeSpan.FromSeconds(10));
        
        while (isOpen)
        {
                //Debug.Log("dingadinga");
            await Task.Delay(TimeSpan.FromSeconds(5)); // Adjust the interval as needed

            if (webSocket != null && webSocket.IsOpen && !isreconnecting)
            {
                if(!connectionEstablished){
                  //  Debug.Log("notified");
                    // connectionError?.Invoke();
                    ConnectToWebSocket();
                }
                try
                {
                 //   Debug.Log("try");
                    webSocket.Send("ping");
                    connectionEstablished=false;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to send ping: " + ex.Message);
                }
            }
        }
    }
        public CustomWebSocet(){
            
            string connectLink=link+ProfileInfo.Player.token;
            //Debug.Log(connectLink);
            
            webSocket = new WebSocket(new Uri(connectLink));
            webSocket.OnOpen += OnWebSocketOpen;
            webSocket.OnError += OnError;
            webSocket.OnClosed += OnWebSocketClosed;
            webSocket.OnMessage += OnMessageReceived;
            lobbyevents = new LobbyEvents();
            webSocetIsReady =true;
            webSocket.Open();
            Task.Run(PingLoop);
        }
        public string createMessage(string command,IDictionary<string, string>  parms){
            string Imessage="";
            Imessage+="{\"command\":\""+command+"\",\"params\":{";
            int i=0;
            foreach (var par in parms)
            {
                i+=1;
                Imessage+="\""+par.Key+"\":\""+par.Value+"\"";
                if(i<parms.Count){
                    Imessage+=",";
                }
            }
            Imessage+="}}";
            return Imessage;
        }
        public void closeConnection(){
            webSocket.Close();
        }
        public void SendMessageOptions(string massage){
            Debug.Log(massage);
            webSocket.Send(massage);
        }

        private void OnWebSocketOpen(WebSocket webSocket)
        {
            //Debug.Log("connectio is open");
            connectionEstablished=true;
            isOpen=true;
            isreconnecting=false;
            if(MainTable!=null){
                LeaveToLobby(MainTable.gameId);
            }
        }
        void OnError(WebSocket ws, string error)
        {
            if(error.Contains("closed unexpectedly by the remote server")||error.Contains("Unable to read data from the transport connection")||error.Contains(" No close_notify alert received")){
            isOpen=false;
            Debug.Log("from the error");
            UIWebSocket.ConnectToWebSocket();
    }
            if (error.Contains("disconnected")) // Replace "disconnected" with the actual keyword or condition.
    {
        isOpen = false;
        // connectionError?.Invoke();
        webSocket.Close();
        // UIWebSocket.ConnectToWebSocket();
    }
    if (error.Contains("Could not resolve host") || error.Contains("did not properly respond after a period of time")||error.Contains("Timed Out!")){
        isOpen = false;
        
        webSocket.Close();
        
    }
    
            // connectionError?.Invoke();
            Debug.LogError("TError: " + error);
        }
        private void OnWebSocketClosed(WebSocket webSocket, UInt16 code, string message)
        {
            // connectionError?.Invoke();
            // UIWebSocket.ConnectToWebSocket();
            isOpen=false;
            Debug.Log("from close");
            UIWebSocket.ConnectToWebSocket();
            
            
            
        }
    
    public  void ConnectToWebSocket(){
      //  Debug.Log("qweewq");
        // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        isreconnecting=true;
        int attempts=0;
        
            attempts+=1;
            try{
      //  Debug.Log("qweewq");
        string connectLink = link + ProfileInfo.Player.token;
        // webSocket.Close();
        webSocket = new WebSocket(new Uri(connectLink));
        webSocket.OnOpen += OnWebSocketOpen;
        webSocket.OnError += OnError;
        webSocket.OnClosed += OnWebSocketClosed;
        webSocket.OnMessage += OnMessageReceived;
        lobbyevents = new LobbyEvents();
        webSocetIsReady = true;
        webSocket.Open();
        isOpen=true;
        Debug.Log("donene");
        
        // webSocket.Send("ping");
        // Task.Run(PingLoop);
      
            }
            catch{
                Debug.Log("cAtch");
                ConnectToWebSocket();
            }
        }
        // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    
        private void OnMessageReceived(WebSocket webSocket, string message)
        {
           Debug.Log("Text Message received from server: " + message);
            if(message=="pong"){
                connectionEstablished=true;
                isreconnecting=false;
                return;
            }
            lastMessage=message;
            GameMessage<object> gameMessage = JsonUtility.FromJson<GameMessage<object>>(UIWebSocket.getMessage());
           // Debug.Log(gameMessage.type);
            if(gameMessage.type=="NewUserJoinedGame"){
                GameMessage<NewPlayerJoind> newGameMessage = JsonUtility.FromJson<GameMessage<NewPlayerJoind>>(UIWebSocket.getMessage());
                NewPlayerJoind gameData = newGameMessage.data;
                MainTable.newPlayerJoind(gameData.playerData[0]);
               
            }
            if(gameMessage.type=="GameFound"){
                GameMessage<GameData> newGameMessage = JsonUtility.FromJson<GameMessage<GameData>>(UIWebSocket.getMessage());
                GameData gameData = newGameMessage.data;
                MainTable=new GameTable(gameData);
                SceneManager.LoadScene("Table Scene", LoadSceneMode.Single);
           
            }
            if(gameMessage.type=="PrivateCardsDealt"){
                GameMessage<privateCards> newGameMessage = JsonUtility.FromJson<GameMessage<privateCards>>(UIWebSocket.getMessage());
                privateCards gameData = newGameMessage.data;
                MainTable.privateCardsDelt(gameData);
            }
            if(gameMessage.type=="NewRoundStarted"){
                GameMessage<newRoundStarted> newGameMessage = JsonUtility.FromJson<GameMessage<newRoundStarted>>(UIWebSocket.getMessage());
                newRoundStarted gameData = newGameMessage.data;
                MainTable.newRoundStart();
            }
            if(gameMessage.type=="NewBetPlaced"){
                GameMessage<NewBetPlaced> newGameMessage = JsonUtility.FromJson<GameMessage<NewBetPlaced>>(UIWebSocket.getMessage());
                NewBetPlaced gameData = newGameMessage.data;
                MainTable.placeBet(gameData);
                
            }
            if(gameMessage.type=="PlayerBetMoved"){
                GameMessage<PlayerBetMoved> newGameMessage = JsonUtility.FromJson<GameMessage<PlayerBetMoved>>(UIWebSocket.getMessage());
                PlayerBetMoved gameData = newGameMessage.data;
                MainTable.PlayerBetMove(gameData);
            }
            if(gameMessage.type=="GameCardsDealt"){
                GameMessage<GameCardsDealt> newGameMessage = JsonUtility.FromJson<GameMessage<GameCardsDealt>>(UIWebSocket.getMessage());
                GameCardsDealt gameData = newGameMessage.data;
                MainTable.GameCardsDelt(gameData);
            }
            if(gameMessage.type=="UserLeftGame"){
                GameMessage<UserLeftGame> newGameMessage = JsonUtility.FromJson<GameMessage<UserLeftGame>>(UIWebSocket.getMessage());
                UserLeftGame gameData = newGameMessage.data;
                MainTable.UserLeft(gameData);
            }
            if(gameMessage.type=="RoundWinner"){
                GameMessage<RoundWinner> newGameMessage = JsonUtility.FromJson<GameMessage<RoundWinner>>(UIWebSocket.getMessage());
                RoundWinner gameData = newGameMessage.data;
                gameData.ParsePotsWithCombs(UIWebSocket.getMessage());
                MainTable.manageWining(gameData);
            }
            if (gameMessage.type == "HandStrengthChanged")
            {
                GameMessage<HandStrength> newGameMessage = JsonUtility.FromJson<GameMessage<HandStrength>>(UIWebSocket.getMessage());
                HandStrength gameData = newGameMessage.data;
               
                MainTable.handStrengthChange(gameData);
            }
            if (gameMessage.type == "PlayerOutOfChips")
            {
                GameMessage<PlayerOutOfChips> newGameMessage = JsonUtility.FromJson<GameMessage<PlayerOutOfChips>>(UIWebSocket.getMessage());
                PlayerOutOfChips gameData = newGameMessage.data;
               
                MainTable.PlayerOutOfChip(gameData);
            }
            if (gameMessage.type == "AutoBuyIn")
            {
                GameMessage<AutoBuyIn> newGameMessage = JsonUtility.FromJson<GameMessage<AutoBuyIn>>(UIWebSocket.getMessage());
                AutoBuyIn gameData = newGameMessage.data;
               
                MainTable.autoBuyIn(gameData);
            }
            if (gameMessage.type == "GameMessageSent")
            {
                GameMessage<GameMessageSent> newGameMessage = JsonUtility.FromJson<GameMessage<GameMessageSent>>(UIWebSocket.getMessage());
                GameMessageSent gameData = newGameMessage.data;

                MainTable.TableMessage(gameData);
            }
            if (gameMessage.type == "GameGifSent")
            {
                GameMessage<GameGifSent> newGameMessage = JsonUtility.FromJson<GameMessage<GameGifSent>>(UIWebSocket.getMessage());
                GameGifSent gameData = newGameMessage.data;

                MainTable.GifSend(gameData);
            }
            if (gameMessage.type == "PlayerInvitedToLobby")
            {
                GameMessage<PlayerInvitedToLobby> newGameMessage = JsonUtility.FromJson<GameMessage<PlayerInvitedToLobby>>(UIWebSocket.getMessage());
                PlayerInvitedToLobby gameData = newGameMessage.data;
                // ProfileInfo.Player.notifications_count=gameData.notificationsCount;
                lobbyevents.PlayerInvited(gameData);
            }
            if (gameMessage.type == "PrivateMessageSent")
            {
                GameMessage<PrivateMessageSent> newGameMessage = JsonUtility.FromJson<GameMessage<PrivateMessageSent>>(UIWebSocket.getMessage());
                PrivateMessageSent gameData = newGameMessage.data;
                ProfileInfo.MyPlayer.notifications_count=gameData.notificationsCount;
                MessageData=gameData;
                
                OnGetMessages?.Invoke();
            }
            if (gameMessage.type == "FriendRequestSent")
            {
                GameMessage<FriendRequestNoteInfo> newGameMessage = JsonUtility.FromJson<GameMessage<FriendRequestNoteInfo>>(UIWebSocket.getMessage());
                FriendRequestNoteInfo gameData = newGameMessage.data;
                ProfileInfo.MyPlayer.notifications_count=gameData.notificationsCount;
                Requestinfo = gameData;
                OnFriendRequestSent?.Invoke();
            }
            if (gameMessage.type == "JackpotsUpdated")
            {
                GameMessage<ProfileInfo.Pots> newGameMessage = JsonUtility.FromJson<GameMessage<ProfileInfo.Pots>>(UIWebSocket.getMessage());

                ProfileInfo.GamePots = newGameMessage.data;
               
                OnJackpotsUpdated?.Invoke();
            }
            if (gameMessage.type == "UserLeveledUp")
            {
                GameMessage<ProfileInfo.UserLeveled> newGameMessage = JsonUtility.FromJson<GameMessage<ProfileInfo.UserLeveled>>(UIWebSocket.getMessage());
                ProfileInfo.UserLeveled newLevel = newGameMessage.data;

                OnUserLeveledUp?.Invoke(newLevel.level);
            }
            if (gameMessage.type == "ChipsUpdated")
            {
                
                GameMessage<ChipsUpdated> newGameMessage = JsonUtility.FromJson<GameMessage<ChipsUpdated>>(UIWebSocket.getMessage());
                ChipsUpdated gameData = newGameMessage.data;
                ProfileInfo.MyPlayer.chips=gameData.chips;
                getChips.isGrad=true;
                if(gameData.type!="spin")
                {
                    OnChipsUpdated?.Invoke();

                }
            }
            if(gameMessage.type=="ChipsTransferred"){
                GameMessage<ChipsTransferred> newGameMessage = JsonUtility.FromJson<GameMessage<ChipsTransferred>>(UIWebSocket.getMessage());
                ChipsTransferred gameData = newGameMessage.data;
                ProfileInfo.MyPlayer.notifications_count=gameData.notificationsCount;
                
            }
            if (gameMessage.type == "FourOfAKind")
            {
                
                JObject jsonObject = JObject.Parse(UIWebSocket.getMessage());

                long chipsWon = jsonObject["data"]["chips"].ToObject<long>();
                int userWon = jsonObject["data"]["userId"].ToObject<int>();

                Debug.Log(chipsWon);
                 OnWinFOAK?.Invoke(userWon,chipsWon);

            }
            if (gameMessage.type == "StraightFlush")
            {
                
                JObject jsonObject = JObject.Parse(UIWebSocket.getMessage());

                long chipsWon = jsonObject["data"]["chips"].ToObject<long>();
                int userWon = jsonObject["data"]["userId"].ToObject<int>();

                Debug.Log(chipsWon);
                OnWinFOAK?.Invoke(userWon, chipsWon);

            }
            if (gameMessage.type == "RoyalFlush")
            {

                JObject jsonObject = JObject.Parse(UIWebSocket.getMessage());

                long chipsWon = jsonObject["data"]["chips"].ToObject<long>();
                int userWon = jsonObject["data"]["userId"].ToObject<int>();
                string userName = jsonObject["data"]["userName"].ToObject<string>();
                string imageUrl = jsonObject["data"]["userImage"].ToObject<string>();
                string gameId = jsonObject["data"]["gameId"].ToObject<string>(); 
                Debug.Log(chipsWon);
                OnWinRoyalFush?.Invoke(userWon, chipsWon);
                OnWinRoyalFlushLobby?.Invoke(imageUrl, userName, gameId);


            }
            waitMessage =false;
            messageBuffer.Add(message);
            lastMessage=message;

            if(messageBuffer.Count>20){
                messageBuffer.RemoveAt(0);
            }
        }
        public void test()
        {
            Debug.Log("space key was pressed");
            OnWinFOAK?.Invoke(int.Parse(ProfileInfo.Player.id), 50000000000);
        }
        public string getMessage(){
            return lastMessage;
        }
        public int UISendMessage(string message,string GameId){
            string command="send-message";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("message",message);
            options.Add("gameId",GameId);
            string NewMessage=createMessage(command,options);
            if(isOpen && !waitMessage){
                waitMessage=true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }

        public int UISendGif(string GameId,string gifId,string receiverId){
            string command="send-gif";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("gifId",gifId);
            options.Add("gameId",GameId);
            options.Add("receiverId",receiverId);
            string NewMessage=createMessage(command,options);
            if(isOpen && !waitMessage){
                waitMessage=true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }

        public int UIFindGame(string gameTypeId,string gameSize){
            string command="find-game";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("gameTypeId",gameTypeId);
            options.Add("gameSize",gameSize);
            string NewMessage=createMessage(command,options);
            //Debug.Log(NewMessage);
            Debug.Log(waitMessage);
            if(isOpen && !waitMessage){
                //Debug.Log(NewMessage);
                waitMessage=true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        public int joinGame(string gameId)
        {
            if(webSocetConnect.UIWebSocket.MainTable != null)
                if(webSocetConnect.UIWebSocket.MainTable.gameId !=null)
                {
                    LeaveToLobby(webSocetConnect.UIWebSocket.MainTable.gameId);

                }
            string command = "join-game";
            IDictionary<string, string> options = new Dictionary<string, string>();
            options.Add("gameId", gameId);
            string NewMessage = createMessage(command, options);
            if (isOpen && !waitMessage)
            {
                waitMessage = true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        public int LeaveToLobby(string gameId)
        {
            string command = "go-to-lobby";
            IDictionary<string, string> options = new Dictionary<string, string>();
            options.Add("gameId", gameId);
            string NewMessage = createMessage(command, options);
            GameVariables.isPlaying = false;
            if (isOpen && !waitMessage)
            {
                //waitMessage = true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        public int InviteToLobby(string gameId, string userId)
        {
            string command = "invite-to-lobby";
            IDictionary<string, string> options = new Dictionary<string, string>();
            options.Add("gameId", gameId);
            options.Add("userId", userId);
            string NewMessage = createMessage(command, options);
            if (isOpen && !waitMessage)
            {
                waitMessage = true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        public int UIBuyIn(string seatNumber,string autoBuyIn,string buyInAmount,string gameId){
            string command="buy-in";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("seatNumber",seatNumber);
            options.Add("autoBuyIn",autoBuyIn);
            options.Add("buyInAmount",buyInAmount);
            options.Add("gameId",gameId);
            string NewMessage=createMessage(command,options);
            //Debug.Log(NewMessage);
            if(isOpen && !waitMessage){
                waitMessage=true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        

        public int UIPlaceBet(string betType,string amount,string gameId){
            string command="bet";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("betType",betType);
            options.Add("amount",amount);
            options.Add("gameId",gameId);
            string NewMessage=createMessage(command,options);
            //Debug.Log(NewMessage);
            if(isOpen && !waitMessage){
                waitMessage=true;
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }

        public int UILeave(string gameId){
            string command="leave";
            IDictionary<string,string> options=new Dictionary<string,string>();
            options.Add("gameId",gameId);
            string NewMessage=createMessage(command,options);
            GameVariables.isPlaying = false;

            //Debug.Log(NewMessage);
            if (isOpen && !waitMessage){
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }
        public int UINewTable(string gameId)
        {
            string command = "new-table";
            IDictionary<string, string> options = new Dictionary<string, string>();
            options.Add("gameId", gameId);
            string NewMessage = createMessage(command, options);
            GameVariables.isPlaying = false;

            //Debug.Log(NewMessage);
            if (isOpen && !waitMessage)
            {
                SendMessageOptions(NewMessage);
                return 1;
            }
            return -1;
        }

    }
    public static CustomWebSocet UIWebSocket =null;
    private static bool attemptingReconnect = false;
    void Start(){
        if (UIWebSocket == null)
        {
        UIWebSocket=new CustomWebSocet();
        UIWebSocket.connectionError+=reconnect;
        }
         
    }
    public static void reconnect(){
            
            attemptingReconnect = true;
            Debug.Log("reconnecting");
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
            attemptingReconnect = false;
        // UIWebSocket.connectionError-=reconnect;
        // Attempt to reconnect
            UIWebSocket.ConnectToWebSocket();
            
            // UIWebSocket.connectionError+=reconnect;
            // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
    IEnumerator ReconnectAfterDelay(int delaySeconds)
    {
        Debug.Log($"Attempting to reconnect in {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        // Reset the reconnection flag
        attemptingReconnect = false;
        // UIWebSocket.connectionError-=reconnect;
        // Attempt to reconnect
        UIWebSocket.ConnectToWebSocket();
        // UIWebSocket.connectionError+=reconnect;

        // SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
    //public void placeBet(string betType,string amount,string gameId){
void OnDestroy()
{
    UIWebSocket.connectionError-=reconnect;
}
    public void placeBet(string betType,string amount,string gameId){  
        gameId=UIWebSocket.MainTable.gameId;
        StartCoroutine(placeBetNum(betType,amount,gameId));
        
    }
    IEnumerator placeBetNum(string betType,string amount,string gameId)
{   
    UIWebSocket.UIPlaceBet(betType,amount,gameId);
    Debug.Log("placeBet");
    while (UIWebSocket.waitMessage)
    {
        yield return null;
        
    }  
}
    public void findGame(string gameType, string gameSize){
        Debug.Log(gameType);
        Debug.Log(gameSize);
        
        StartCoroutine(findGameNum(gameType,gameSize));        
    
    }

    IEnumerator findGameNum(string gameType, string gameSize)
{   
    UIWebSocket.UIFindGame(gameType,gameSize);
    Debug.Log("findinggame");
    // UIWebSocket.waitMessage=true;
    while (UIWebSocket.waitMessage)
    {
       // Debug.Log("waiting ");
        yield return null;       
    }
    
    UIWebSocket.MainTable.gameSize=gameSize;
    UIWebSocket.MainTable.gameType=gameType;
   // Debug.Log("2222 ");
   // SceneManager.LoadScene("Table Scene", LoadSceneMode.Single);
  //  GameMessage<GameData> gameMessage = JsonUtility.FromJson<GameMessage<GameData>>(UIWebSocket.getMessage());
  //  GameData GmaeData = gameMessage.data;
    GameVariables.isOnTableScene = true;
        GameVariables.isSpectating = true;


    }
    public void buyIn(string seatNumber, string autoBuyIn,string buyInAmount,string gameId){
        
        StartCoroutine(buyInNum(seatNumber,autoBuyIn,buyInAmount,gameId));
        Debug.Log("wtf ");
    }
        IEnumerator buyInNum(string seatNumber, string autoBuyIn,string buyInAmount,string gameId)
    {   
        UIWebSocket.UIBuyIn(seatNumber,autoBuyIn,buyInAmount,gameId);
        Debug.Log("buyinGame");
        while (UIWebSocket.waitMessage)
        {
            yield return null;
            
        }
        
    SceneManager.LoadScene("Table Scene", LoadSceneMode.Single);
        Debug.Log("wtf ");

        PlayersManager playersManager = FindObjectOfType<PlayersManager>();
    playersManager.takeSeatInGame();


    }
    public void LeaveToLobbyWrapper(string gameId)
    {
        StartCoroutine(LeaveToLobby(gameId));
        GameVariables.seatOffset=0;
    }
    IEnumerator LeaveToLobby(string gameId)
    {
        UIWebSocket.LeaveToLobby(gameId);
        Debug.Log("LeaveToLobby");
        while (UIWebSocket.waitMessage)
        {
            yield return null;
        }
        webSocetConnect.UIWebSocket.MainTable.gameId = null;
        GameVariables.isOnTableScene = false;
        GameVariables.firstIni = true;
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        GameVariables.isSpectating = false;

    }
    public void inviteToGameWrapper(string gameId, string userId)
    {
        StartCoroutine(inviteToGame(gameId,userId));
    }
    IEnumerator inviteToGame(string gameId, string userId)
    {
        UIWebSocket.InviteToLobby(gameId, userId);
        Debug.Log("inviteToGame");
        while (UIWebSocket.waitMessage)
        {
            yield return null;
        }
    }
    public void joinGameWrapper(string gameId)
    {
        StartCoroutine(joinGameNum(gameId));
    }
    IEnumerator joinGameNum(string gameId)
    {
        UIWebSocket.joinGame(gameId);
        Debug.Log("JoinGame");
        while (UIWebSocket.waitMessage)
        {
            yield return null;
        }
     //   SceneManager.LoadScene("Table Scene", LoadSceneMode.Single);
        GameVariables.isSpectating = true;

    }
    public void leaveTable(string gameId){
        gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        StartCoroutine(leaveNum(gameId));
    }
    IEnumerator leaveNum(string gameId){
        UIWebSocket.UILeave(gameId);
        Debug.Log("leaveGame");
        while (UIWebSocket.waitMessage)
        {
            yield return null;           
        }
        GameVariables.isSpectating = true;
    }
    public void WrapperNewTable(string gameId)
    {
        gameId = webSocetConnect.UIWebSocket.MainTable.gameId;
        StartCoroutine(NewTable(gameId));
    }
    IEnumerator NewTable(string gameId)
    {
        UIWebSocket.UINewTable(gameId);
        Debug.Log("newTableFound");
        while (UIWebSocket.waitMessage)
        {
            yield return null;
        }
        //   SceneManager.LoadScene("Table Scene", LoadSceneMode.Single);
        GameVariables.seatOffset = 0;

        GameVariables.isSpectating = true;
    }
    public void sendMessage(string gameId,string message){
        gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        StartCoroutine(sendMessageNum(gameId,message));
    }
    IEnumerator sendMessageNum(string gameId,string message){
        UIWebSocket.UISendMessage(message,gameId);
        Debug.Log("leaveGame");
        while (UIWebSocket.waitMessage)
        {
            yield return null;           
        }
    }
    void OnApplicationQuit()
    {
        UIWebSocket.closeConnection();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
