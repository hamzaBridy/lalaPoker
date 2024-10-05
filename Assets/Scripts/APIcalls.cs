using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using static ProfileInfo;
using System.Linq;
using Newtonsoft.Json.Linq;
using static LuckySpinSCript;
using System.Security;
using System.IO;
public class APIcalls : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }
  
    private void Start()
    {
        GameVariables.firstIni = true;
    }
    public TMP_InputField Email;
    public TMP_InputField Password;
    public TMP_InputField conPassword;
    public TMP_InputField User_nameR;
    public TMP_InputField EmailLogIn;
    public TMP_InputField PasswordLogIn;
    [SerializeField] private GameObject profilePannel;
    [SerializeField] private TMP_InputField emailVerCode, resetPasswordEmail,newPassword,resetPassCode, newPasswordConfirm;
    [SerializeField] private GameObject verCodePannel, problemWithRegisterPannel,newPasswordPannel,loadingScreen;
    public static event  Action OnGetStatues;
    public event Action OnGetMessages;
    public LuckySpinSCript luckySpinSCript;
    public static string myEmail;
    public static string Host = "https://api.lalapoker.com";
    // public static string Host = "http://localhost";
    [System.Serializable]
    public class userInfo{
        public string Token;
    }
    public void LogoutUser()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "myInfo.json");

        // Check if the file exists before trying to delete
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            // Optionally, log this action if needed for debugging
            // Debug.Log("User logged out and token removed");
        }
        else
        {
            // If the file doesn't exist, optionally handle this case, e.g., log a message
            // Debug.Log("No user token found to delete");
        }
    }

    public void saveUser(string token){
        string filePath = Path.Combine(Application.persistentDataPath, "myInfo.json");
      //  Debug.Log(filePath);
        userInfo myInfo= new userInfo{
            Token=token,
        };
        string json = JsonUtility.ToJson(myInfo);
       // Debug.Log("start saving");
        File.WriteAllText(filePath, json);
        //Debug.Log("done saving");

    }
    public void AutoLogin(string token){
        StartCoroutine(Tokenize(token));
    }

    public IEnumerator Tokenize(string token)
    {

        UnityWebRequest www = UnityWebRequest.Get(Host + "/api/user");

        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + token);


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
            LogoutUser();
            loadingScreen.SetActive(false);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(www.downloadHandler.text);
            ProfileInfo.Player.token=token;

            // saveUser(ProfileInfo.Player.token);
            GameVariables.isLoggedIn=true;
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }

    IEnumerator Playasguest()
    {
        WWWForm form = new WWWForm();
        int DevID = UnityEngine.Random.Range(1000, 111220);
        form.AddField("email", "");
        form.AddField("password", "");
        form.AddField("password_confirmation", "");
        form.AddField("device_id", DevID);

        UnityWebRequest GuestPostRequest = UnityWebRequest.Post(Host + "/api/auth/register", form);
        GuestPostRequest.SetRequestHeader("Accept", "application/json");
        yield return GuestPostRequest.SendWebRequest();
        
        
        if (GuestPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GuestPostRequest.error);
            Debug.Log(GuestPostRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log(GuestPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(GuestPostRequest.downloadHandler.text);
            saveUser(ProfileInfo.Player.token);
            GameVariables.isLoggedIn=true;
            if(webSocetConnect.UIWebSocket!=null)
            webSocetConnect.UIWebSocket.ConnectToWebSocket();
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
            


        }

    }
    public void PlayasguestWraper()
    {
        StartCoroutine(Playasguest());
    }
    public static event Action<string> onRegister;
    public static event Action<string> onLoginResults;

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        if(Email.text != null)
        myEmail = Email.text;
        form.AddField("email",Email.text);
        form.AddField("password", Password.text);
        form.AddField("password_confirmation", conPassword.text);
        form.AddField("device_id", "ASDASasda123");
        form.AddField("user_name", User_nameR.text);
        UnityWebRequest RegesterPostRequest = UnityWebRequest.Post(Host + "/api/auth/register", form);
        RegesterPostRequest.SetRequestHeader("Accept", "application/json");
        yield return RegesterPostRequest.SendWebRequest();
        if (RegesterPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(RegesterPostRequest.error);
            Debug.Log(RegesterPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((RegesterPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);

            onRegister?.Invoke(combinedError);
           // problemWithRegisterPannel.SetActive(true);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(RegesterPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(RegesterPostRequest.downloadHandler.text);
            saveUser(ProfileInfo.Player.token);
            if (webSocetConnect.UIWebSocket != null)
                webSocetConnect.UIWebSocket.ConnectToWebSocket();
            Debug.Log(ProfileInfo.Player.token);

            verCodePannel.SetActive(true);
        }
    }
    public void Registerwraper()
    {
        StartCoroutine(Register());
    }
    public void LoginWraper()
    {
        StartCoroutine(LogIn());
    }
    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", EmailLogIn.text);
        form.AddField("password", PasswordLogIn.text);
        //form.AddField("device_id", SystemInfo.deviceUniqueIdentifier);
        form.AddField("device_id", "ASDAS1234");


        UnityWebRequest loginPostRequest = UnityWebRequest.Post(Host + "/api/auth/login", form);
        loginPostRequest.SetRequestHeader("Accept", "application/json");
        yield return loginPostRequest.SendWebRequest();

        //error handling sth sth
        if (loginPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(loginPostRequest.error);
            //Debug.Log(www.downloadHandler.text);

            Debug.Log(loginPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((loginPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);
            onLoginResults?.Invoke(combinedError);
        }
        else
        {
            Debug.Log(loginPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(loginPostRequest.downloadHandler.text);
            saveUser(ProfileInfo.Player.token);
            if (webSocetConnect.UIWebSocket != null)
                webSocetConnect.UIWebSocket.ConnectToWebSocket();
            GameVariables.isLoggedIn=true;
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
    public void EmailConfirmationwraper()
    {
        StartCoroutine(EmailConfirmation());
    }
    IEnumerator EmailConfirmation()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", myEmail);
        form.AddField("code", emailVerCode.text);


        UnityWebRequest loginPostRequest = UnityWebRequest.Post(Host + "/api/auth/verify_email", form);
        loginPostRequest.SetRequestHeader("Accept", "application/json");
        yield return loginPostRequest.SendWebRequest();

        //error handling sth sth
        if (loginPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(loginPostRequest.error);
            Debug.Log(loginPostRequest.downloadHandler.text);
            if (loginPostRequest.error == "HTTP/1.1 400 Bad Request")
            {
                JObject stuff1 = JObject.Parse((loginPostRequest.downloadHandler.text));

                string errormessage = stuff1["message"].ToObject<string>();
                onRegister?.Invoke(errormessage);
                yield break;

            }
            Debug.Log(loginPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((loginPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);

            onRegister?.Invoke(combinedError);
        }
        else
        {
            Debug.Log(loginPostRequest.downloadHandler.text);
            Debug.Log(ProfileInfo.Player.token);
            GameVariables.isLoggedIn = true;
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
    public void ReqResetPasswraper()
    {
        StartCoroutine(ReqResetPass());
    }
    IEnumerator ReqResetPass()
    {
        WWWForm form = new WWWForm();
        myEmail = resetPasswordEmail.text;
        form.AddField("email", myEmail);
        UnityWebRequest loginPostRequest = UnityWebRequest.Post(Host + "/api/auth/request_token", form);
        loginPostRequest.SetRequestHeader("Accept", "application/json");
        yield return loginPostRequest.SendWebRequest();

        //error handling sth sth
        if (loginPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(loginPostRequest.error);
            //Debug.Log(www.downloadHandler.text);

            Debug.Log(loginPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((loginPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);

            OnNewPassError?.Invoke(combinedError);

        }
        else
        {
            Debug.Log(loginPostRequest.downloadHandler.text);

            newPasswordPannel.SetActive(true);
        }
    }
    public static event Action<string> OnNewPassError;

    public void ResetPasswraper()
    {
        StartCoroutine(ResetPass());
    }
    IEnumerator ResetPass()
    {
        WWWForm form = new WWWForm();
        myEmail = resetPasswordEmail.text;
        form.AddField("email", myEmail);
        form.AddField("code", resetPassCode.text);
        form.AddField("password", newPassword.text);
        form.AddField("password_confirmation", newPasswordConfirm.text);


        UnityWebRequest loginPostRequest = UnityWebRequest.Post(Host + "/api/auth/reset_password", form);
        loginPostRequest.SetRequestHeader("Accept", "application/json");
        yield return loginPostRequest.SendWebRequest();

        //error handling sth sth
        if (loginPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(loginPostRequest.error);
            //Debug.Log(www.downloadHandler.text);
            if(loginPostRequest.error == "HTTP/1.1 400 Bad Request")
            {
                JObject stuff1 = JObject.Parse((loginPostRequest.downloadHandler.text));

                string errormessage = stuff1["message"].ToObject<string>();
                OnNewPassError?.Invoke(errormessage);
                yield break;

            }
            Debug.Log(loginPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((loginPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);

            OnNewPassError?.Invoke(combinedError);
        }
        else
        {
            Debug.Log(loginPostRequest.downloadHandler.text);
            OnNewPassError?.Invoke("Your password has been changed");
            newPasswordPannel.SetActive(false);

        }
    }
    public static event Action onMyStatus;
    public IEnumerator GetStatues(string id,bool isl3b6a)
    {
        WWWForm form = new WWWForm();


        UnityWebRequest statuesGetRequest = UnityWebRequest.Get(Host + "/api/user/game-profile/" + id);


        statuesGetRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        statuesGetRequest.SetRequestHeader("Accept", "application/json");


        yield return statuesGetRequest.SendWebRequest();

        //error handling sth sth
        if (statuesGetRequest.result != UnityWebRequest.Result.Success)
        {
             Debug.Log("Form asdasdasd complete!");

            Debug.Log(statuesGetRequest.error);
            Debug.Log(statuesGetRequest.downloadHandler.text);
            Debug.Log(id);
        }
        else
        {
           

            Debug.Log("Form upload complete!");
            Debug.Log(statuesGetRequest.downloadHandler.text);
            if (id == ProfileInfo.Player.id)
            {
                ProfileInfo.MyPlayer = JsonUtility.FromJson<ProfileInfo.Statues>(statuesGetRequest.downloadHandler.text);
                onMyStatus?.Invoke();
            }
            ProfileInfo.Statues infos = JsonUtility.FromJson<ProfileInfo.Statues>(statuesGetRequest.downloadHandler.text);
            infos.ParseUser(statuesGetRequest.downloadHandler.text);
            GameVariables.dataIsavilable=true;
            

                ProfilePannel profile = profilePannel.GetComponent<ProfilePannel>();
                profile.balance =  infos.chips.ToString("N0");
                profile.handsPlayed = infos.hands_played;
                profile.biggestWin = MoneyConverter.ConvertMoney(infos.biggest_win);
                profile.level = infos.level;
                profile.xpPoints = infos.experience_points.ToString("N0");
                profile.vipLevel = infos.vip_level;
                profile.ljpChipsWon = MoneyConverter.ConvertMoney(infos.bjp_wins_amount);
                profile.ljpWins = infos.bjp_wins;
                profile.imageUrl = infos.user.profile_image_url;
                profile.userName = infos.user.user_name;
                profile.isBlocked = infos.is_blocked;
                profile.FriendRequestSent = infos.friend_request_sent;
                profile.id = infos.user_id;           
                profile.friendShipId = infos.friendship_id;
                profile.winPerentage = infos.win_percentage.ToString();
             if (!GameVariables.firstIni && isl3b6a)
            {
                profilePannel.gameObject.SetActive(true);
                OnGetStatues?.Invoke();
            }
            GameVariables.firstIni = false;



        }
    }
public void getStatuesWrapper(string id,bool isl3b6a=true){
        StartCoroutine(GetStatues(id,isl3b6a));
    }
public void SendFriendRequestWrapper(string id){
        StartCoroutine(SendFriendRequest(id));
    }
    IEnumerator SendFriendRequest(string friendId)
    {
        WWWForm form = new WWWForm();
        form.AddField("friend_id",friendId);
        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/friendship/send", form);
        SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
    }

    public void acceptFriendRequestWrapper(string id){
        StartCoroutine(acceptFriendRequest(id));
    }
    public static event Action<int> OnFriendRequestProcessed;

    IEnumerator acceptFriendRequest(string friendShipId)
    {
        WWWForm form = new WWWForm();
        form.AddField("friendship_id",friendShipId);
        UnityWebRequest acceptPostRequest = UnityWebRequest.Post(Host + "/api/friendship/accept", form);
        acceptPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        acceptPostRequest.SetRequestHeader("Accept", "application/json");
        yield return acceptPostRequest.SendWebRequest();
        if (acceptPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(acceptPostRequest.error);
            Debug.Log(acceptPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(acceptPostRequest.downloadHandler.text);
            OnFriendRequestProcessed?.Invoke(int.Parse(friendShipId));

            var requestToRemove = ProfileInfo.MyFriendReq.FirstOrDefault(req => req.id.ToString() == friendShipId);

            if (requestToRemove != null)
            {
                ProfileInfo.MyFriendReq.Remove(requestToRemove);
            }
            
        }
    }

    public void declineFriendRequestWrapper(string id){
        StartCoroutine(declineFriendRequest(id));
    }
    IEnumerator declineFriendRequest(string friendShipId)
    {
        WWWForm form = new WWWForm();
        form.AddField("friendship_id",friendShipId);
        UnityWebRequest declinePostRequest = UnityWebRequest.Post(Host + "/api/friendship/decline", form);
        declinePostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        declinePostRequest.SetRequestHeader("Accept", "application/json");
        yield return declinePostRequest.SendWebRequest();
        if (declinePostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(declinePostRequest.error);
            Debug.Log(declinePostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(declinePostRequest.downloadHandler.text);
            OnFriendRequestProcessed?.Invoke(int.Parse(friendShipId));

        }
    }

    public void deleteFriendRequestWrapper(string id){
        StartCoroutine(deleteFriendRequest(id));
    }
    public static event Action<int> OnFriendDelete;

    IEnumerator deleteFriendRequest(string friendShipId)
    {
        
        UnityWebRequest deletesGetRequest = UnityWebRequest.Delete(Host + "/api/friendship/delete?friendship_id="+friendShipId);
        deletesGetRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        deletesGetRequest.SetRequestHeader("Accept", "application/json");
        yield return deletesGetRequest.SendWebRequest();
        if (deletesGetRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(deletesGetRequest.error);
            Debug.Log(deletesGetRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            //Debug.Log(deletesGetRequest.downloadHandler.text);
            OnFriendDelete?.Invoke(int.Parse(friendShipId));

        }
    }
    public static event Action onFriendsReady;

    public IEnumerator GetFriends()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GetFriendsGetRequest = UnityWebRequest.Get(Host + "/api/users/friends");


        GetFriendsGetRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GetFriendsGetRequest.SetRequestHeader("Accept", "application/json");


        yield return GetFriendsGetRequest.SendWebRequest();

        //error handling sth sth
        if (GetFriendsGetRequest.result != UnityWebRequest.Result.Success)
        {
             Debug.Log("Form asdasdasd complete!");

            Debug.Log(GetFriendsGetRequest.error);
            Debug.Log(GetFriendsGetRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetFriendsGetRequest.downloadHandler.text);
            var friendsList = JsonUtility.FromJson<FriendList>("{\"friends\":" + GetFriendsGetRequest.downloadHandler.text + "}");
            ProfileInfo.MyFriends = friendsList.friends;
            onFriendsReady?.Invoke();
        }
    }
public void GetFriendsWrapper(){
        StartCoroutine(GetFriends());
    }
    public static event Action onFriendsReqReady;

    public IEnumerator GetFriendsReq()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GetFriendsGetRequest = UnityWebRequest.Get(Host + "/api/friendship/requests");


        GetFriendsGetRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GetFriendsGetRequest.SetRequestHeader("Accept", "application/json");


        yield return GetFriendsGetRequest.SendWebRequest();

        //error handling sth sth
        if (GetFriendsGetRequest.result != UnityWebRequest.Result.Success)
        {
             Debug.Log("Form asdasdasd complete!");

            Debug.Log(GetFriendsGetRequest.error);
            Debug.Log(GetFriendsGetRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetFriendsGetRequest.downloadHandler.text);
            var friendReqList = JsonUtility.FromJson<FriendReqList>("{\"friendReqs\":" + GetFriendsGetRequest.downloadHandler.text + "}");
            ProfileInfo.MyFriendReq = friendReqList.friendReqs;

            onFriendsReqReady?.Invoke();

        }
    }
    public void getprivatechatWrapper(string userId)
    {
        StartCoroutine(getprivatechat(userId));
    }
    IEnumerator getprivatechat(string id)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest getprivatechatRequest = UnityWebRequest.Get(Host + "/api/message/" + id);


        getprivatechatRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        getprivatechatRequest.SetRequestHeader("Accept", "application/json");


        yield return getprivatechatRequest.SendWebRequest();

        //error handling sth sth
        if (getprivatechatRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(getprivatechatRequest.error);
            Debug.Log(getprivatechatRequest.downloadHandler.text);
        }
        else
        {
          
            Debug.Log("Form upload complete!");
            Debug.Log(getprivatechatRequest.downloadHandler.text);
            messageList meeages = JsonUtility.FromJson<messageList>("{\"messages\":" + getprivatechatRequest.downloadHandler.text + "}");
            Debug.Log("{\"messages\":" + getprivatechatRequest.downloadHandler.text + "}");
            Dictionary<string,string> DateForamtedMessage=new Dictionary<string, string>();
            
            foreach (OneMessage item in meeages.messages)
            {
                string color="blue";
                if(item.from_id==ProfileInfo.MyPlayer.user_id){
                    color= "#FF0000";
                }
                else{
                    color= "#FF5733";
                }
                string dateConfig="error";
                if (DateTime.TryParse(item.created_at, out DateTime parsedDate))
        {
            // Format the parsed date as "yyyy-MM-dd HH:mm"
            string formattedDate = parsedDate.ToString("yyyy-MM-dd HH:mm");
             dateConfig=$"<size={20}>{formattedDate}</size>\n";

            // Display the formatted date
            // Console.WriteLine("Formatted Date: " + formattedDate);
        }
                
                string TempMessage=dateConfig+"<color="+color+">"+item.user_name+"</color>"+" : "+item.content+"\n";
                DateForamtedMessage.Add(item.id,TempMessage);
                
            }
           DateForamtedMessage = DateForamtedMessage.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
           PrivateChateManager.PrivateMessages="";
           PrivateTableChat.PrivateMessages="";
           foreach(var s in DateForamtedMessage){
                PrivateChateManager.PrivateMessages+=s.Value;
                PrivateTableChat.PrivateMessages+=s.Value;
           }
           OnGetMessages?.Invoke();
        }
    }
    public void GetFriendsReqWrapper(){
        StartCoroutine(GetFriendsReq());
    }
    public void Spin_SLotMachineWrapper(string SlotID)
    {
        StartCoroutine(Spin_SLotMachine(SlotID));
    }
    public  event Action<long > onLuckySpinWon;

    IEnumerator Spin_SLotMachine(string SlotID)
    {
        WWWForm form = new WWWForm();


        UnityWebRequest Spin_SLotMachineRequest = UnityWebRequest.Post(Host + "/api/user/" + SlotID + "/spin", form);


        Spin_SLotMachineRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        Spin_SLotMachineRequest.SetRequestHeader("Accept", "application/json");


        yield return Spin_SLotMachineRequest.SendWebRequest();

        //error handling sth sth
        if (Spin_SLotMachineRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Spin_SLotMachineRequest.error);
            Debug.Log(Spin_SLotMachineRequest.downloadHandler.text);
        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log(Spin_SLotMachineRequest.downloadHandler.text);
            long winvalue = JsonUtility.FromJson<SlotMachineWon>(Spin_SLotMachineRequest.downloadHandler.text).win;
         //   Debug.Log(winvalue);           
            onLuckySpinWon?.Invoke(winvalue);      
        }
    
    }
    public void Get_SlotMachinePrizesWrapper()
    {
        StartCoroutine(Get_SlotMachinePrizes());
    }
    public static event Action OnGet_SlotMachinePrizes;

    public IEnumerator Get_SlotMachinePrizes()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest Get_SlotMachinePrizesRequest = UnityWebRequest.Get(Host + "/api/user/spins");

        Get_SlotMachinePrizesRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        Get_SlotMachinePrizesRequest.SetRequestHeader("Accept", "application/json");

        yield return Get_SlotMachinePrizesRequest.SendWebRequest();

        if (Get_SlotMachinePrizesRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Get_SlotMachinePrizesRequest.error);
            Debug.Log(Get_SlotMachinePrizesRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(Get_SlotMachinePrizesRequest.downloadHandler.text);

            // Parse the JSON response
            JArray jsonArray = JArray.Parse(Get_SlotMachinePrizesRequest.downloadHandler.text);
            //Main_Menu.SetActive(false);
            //SlotMachine_Panel.SetActive(true);
            spinsQueue.Clear();
            foreach (JObject json in jsonArray)
            {
                int receivedId = json["id"].Value<int>();
                JArray valuesArray = json["level_spin"]["values"].Value<JArray>();
                long[] receivedValues = valuesArray.ToObject<long[]>();

                // Create a new Spin object and enqueue it
                Spin spin = new Spin { Id = receivedId, Values = receivedValues };
                spinsQueue.Enqueue(spin);
            }
            OnGet_SlotMachinePrizes?.Invoke();
        }
        
    }
    public void LuckyWheelSpinWrapper()
    {
        StartCoroutine(LuckyWheelSpin());
    }
    public event Action<long> OnLuckyWheelSpin;
    IEnumerator LuckyWheelSpin()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest LuckyWheelSpinRequest = UnityWebRequest.Post(Host + "/api/spin", form);


        LuckyWheelSpinRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        LuckyWheelSpinRequest.SetRequestHeader("Accept", "application/json");


        yield return LuckyWheelSpinRequest.SendWebRequest();

        if (LuckyWheelSpinRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(LuckyWheelSpinRequest.error);
            Debug.Log(LuckyWheelSpinRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(LuckyWheelSpinRequest.downloadHandler.text);

            long chipsWon = (JsonUtility.FromJson<LuckyWheel>(LuckyWheelSpinRequest.downloadHandler.text).chips);
            OnLuckyWheelSpin?.Invoke(chipsWon);
                

        }
    }
    public void GetLuckyWheelTimeWrapper()
    {
        StartCoroutine(GetLuckyWheelTime());
    }
    public event Action<ServerResponse> OnGetLuckyWheelTime;
    public IEnumerator GetLuckyWheelTime()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest GetLuckyWheelTimeRequest = UnityWebRequest.Get(Host + "/api/spin");

        GetLuckyWheelTimeRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        GetLuckyWheelTimeRequest.SetRequestHeader("Accept", "application/json");

        yield return GetLuckyWheelTimeRequest.SendWebRequest();

        if (GetLuckyWheelTimeRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GetLuckyWheelTimeRequest.error);
            Debug.Log(GetLuckyWheelTimeRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetLuckyWheelTimeRequest.downloadHandler.text);



            ServerResponse response = null;
            response = JsonUtility.FromJson<ServerResponse>(GetLuckyWheelTimeRequest.downloadHandler.text);
            OnGetLuckyWheelTime?.Invoke(response);
            
            }
        }
    public void SendPrivateMessageWrap(string id,string message){
        StartCoroutine(SendPrivateMessage(id,message));
    }
    IEnumerator SendPrivateMessage(string friendShipId,string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("content",message);
        UnityWebRequest messagesPostRequest = UnityWebRequest.Post(Host + "/api/message/"+ friendShipId, form);
        messagesPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        messagesPostRequest.SetRequestHeader("Accept", "application/json");
        yield return messagesPostRequest.SendWebRequest();
        if (messagesPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(messagesPostRequest.error);
            Debug.Log(messagesPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(messagesPostRequest.downloadHandler.text);
            

        }
    }

    public static event Action OnTrans;
    public static event Action<string> OnTransDone;

    public void TransferMoneyWrap(string id,string message){
        StartCoroutine(TransferMoney(id,message));
    }
    IEnumerator TransferMoney(string friendShipId,string amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("amount",amount);
        UnityWebRequest transPostRequest = UnityWebRequest.Post(Host + "/api/user/"+ friendShipId+"/transfer-chips", form);
        transPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        transPostRequest.SetRequestHeader("Accept", "application/json");
        yield return transPostRequest.SendWebRequest();
        if (transPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(transPostRequest.error);
            Debug.Log(transPostRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((transPostRequest.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);
            OnTransDone?.Invoke(combinedError);

        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(transPostRequest.downloadHandler.text);
            OnTransDone?.Invoke("Transer Completed!");
            getStatuesWrapper(ProfileInfo.Player.id, false);

        }
    }
    public void GetRewardsWraper()
    {
        StartCoroutine(GetRewards());
    }
    public event Action<RewardList> OnGetRewards;
 public void GetTransWrapper()
    {
        StartCoroutine(GetTrans());
    }
    

    public IEnumerator GetTrans()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest GetTransRequest = UnityWebRequest.Get(Host + "/api/transfer-logs");

        GetTransRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        GetTransRequest.SetRequestHeader("Accept", "application/json");

        yield return GetTransRequest.SendWebRequest();

        if (GetTransRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GetTransRequest.error);
            Debug.Log(GetTransRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetTransRequest.downloadHandler.text);
            transes transfers = JsonUtility.FromJson<transes>("{\"transfers\":" + GetTransRequest.downloadHandler.text + "}");
            transferInfo.MyTrans=transfers;
            OnTrans?.Invoke();
            
        }
        
    }
    public IEnumerator GetRewards()
    {
        UnityWebRequest www = UnityWebRequest.Get(Host + "/api/rewards");
        www.SetRequestHeader("Accept", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            string jsonResponse = www.downloadHandler.text;

            RewardList rewardList = JsonUtility.FromJson<RewardList>("{\"rewards\":" + jsonResponse + "}");            
            OnGetRewards?.Invoke(rewardList);
        }
    }
    public void ClaimrewardsWraper(string rewardid, RewardPrefabScript rewardDisplay)
    {
        StartCoroutine(Claimrewards(rewardid, rewardDisplay));
    }
    public IEnumerator Claimrewards(string rewardid, RewardPrefabScript rewardDisplay)
    {

        WWWForm form = new WWWForm();


        UnityWebRequest www = UnityWebRequest.Post(Host + "/api/rewards/" + rewardid + "/claim", form);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            Destroy(rewardDisplay.gameObject);
        }
    }
    public void searchForFriendWraper(string friendName)
    {
        StartCoroutine(searchForFriend(friendName));
    }
    public event Action OnusersFound;

    public IEnumerator searchForFriend(string friendName)
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Get(Host + "/api/users/search?search=" + friendName);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            var friendReqList = JsonUtility.FromJson<FriendReqList>("{\"friendReqs\":" + www.downloadHandler.text + "}");
            ProfileInfo.userSearched = friendReqList.friendReqs;
            OnusersFound?.Invoke();          
        }
    }
    public void PromoCodeWrap( string code)
    {
        StartCoroutine(PromoCode(code));
    }
    public event Action<string> OnpromoCodeActivated;

    IEnumerator PromoCode( string code)
    {
        WWWForm form = new WWWForm();
        form.AddField("code", code);
        UnityWebRequest transPostRequest = UnityWebRequest.Post(Host + "/api/promo_code", form);
        transPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        transPostRequest.SetRequestHeader("Accept", "application/json");
        yield return transPostRequest.SendWebRequest();
        if (transPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(transPostRequest.error);
            Debug.Log(transPostRequest.downloadHandler.text);
            OnpromoCodeActivated?.Invoke("Invalid Promo Code");

        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log(transPostRequest.downloadHandler.text);

            OnpromoCodeActivated?.Invoke("Promo Code redeemed sucessfully");
        }
    }
     public void GetNotifiWrapper()
    {
        StartCoroutine(GetNotifi());
    }
    
    public static event Action OnNotifi;
    public IEnumerator GetNotifi()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest GetNotifiRequest = UnityWebRequest.Get(Host + "/api/notifications");

        GetNotifiRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        GetNotifiRequest.SetRequestHeader("Accept", "application/json");

        yield return GetNotifiRequest.SendWebRequest();

        if (GetNotifiRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GetNotifiRequest.error);
            Debug.Log(GetNotifiRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetNotifiRequest.downloadHandler.text);
            NotificationsGroup notif = JsonUtility.FromJson<NotificationsGroup>("{\"NoGrop\":" + GetNotifiRequest.downloadHandler.text + "}");
            notificationsHandel.NGrop= notif;
            OnNotifi?.Invoke();
            
        }
        
    }
    public void EditMyProfileWrapper(string userName)
    {
        StartCoroutine(EditMyProfile(userName));
    }

    public static event Action OnEditMyProfile;
    public static event Action OnEditMyProfileError;

    IEnumerator EditMyProfile(string userName)
    {
        

        WWWForm form = new WWWForm();
        form.AddField("user_name", userName);
        UnityWebRequest www = UnityWebRequest.Post(Host + "/api/user/", form);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
            OnEditMyProfileError?.Invoke();

        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            OnEditMyProfile?.Invoke();
            getStatuesWrapper(ProfileInfo.Player.id);

        }
    }
    public  event Action<float> OnGETCollectiblesProgress;

    public void GETCollectiblesProgressWrapper()
    {
        StartCoroutine(GETCollectiblesProgress());
    }
    IEnumerator GETCollectiblesProgress()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GETCollectiblesProgressRequest = UnityWebRequest.Get(Host + "/api/get-collectibles");


        GETCollectiblesProgressRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GETCollectiblesProgressRequest.SetRequestHeader("Accept", "application/json");


        yield return GETCollectiblesProgressRequest.SendWebRequest();

        //error handling sth sth
        if (GETCollectiblesProgressRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GETCollectiblesProgressRequest.error);
            Debug.Log(GETCollectiblesProgressRequest.downloadHandler.text);


        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GETCollectiblesProgressRequest.downloadHandler.text);
            JObject stuff = JObject.Parse((GETCollectiblesProgressRequest.downloadHandler.text));
            float Progress = stuff["progress"].ToObject<int>();
           // string Max_Progress = stuff["progress"].ToObject<string>();
            JToken user = stuff["collectible"];
            float Max_Progress = (int)user["points_required"];

           float fillAmount = (Progress / Max_Progress);
            OnGETCollectiblesProgress?.Invoke(fillAmount);
        }
    }
    public void GET_CollectiblesWrapper()
    {
        StartCoroutine(GET_Collectibles());
    }
    IEnumerator GET_Collectibles()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GET_CollectiblesRequest = UnityWebRequest.Get(Host + "/api/get-my-collectibles");


        GET_CollectiblesRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GET_CollectiblesRequest.SetRequestHeader("Accept", "application/json");


        yield return GET_CollectiblesRequest.SendWebRequest();

        //error handling sth sth
        if (GET_CollectiblesRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(GET_CollectiblesRequest.error);
            Debug.Log(GET_CollectiblesRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GET_CollectiblesRequest.downloadHandler.text);



        }
    }
    public void LogoutWrapper()
    {
        StartCoroutine(Logout());
    }
    IEnumerator Logout()
    {
        PlayerPrefs.DeleteKey("Token");
        WWWForm form = new WWWForm();


        UnityWebRequest LogoutRequest = UnityWebRequest.Post(Host + "/api/logout/", form);


        LogoutRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        LogoutRequest.SetRequestHeader("Accept", "application/json");


        yield return LogoutRequest.SendWebRequest();

        //error handling sth sth
        if (LogoutRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(LogoutRequest.error);
            Debug.Log(LogoutRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(LogoutRequest.downloadHandler.text);
            LogoutUser();
            ProfileInfo.Player = null;
            SceneManager.LoadSceneAsync("Login Scene", LoadSceneMode.Single);

        }

    }
    public void DeleteUserWrapper()
    {
        StartCoroutine(DeleteUser());
    }
    IEnumerator DeleteUser()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest DeleteUserRequest = UnityWebRequest.Post(Host + "/api/user/delete", form);


        DeleteUserRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        DeleteUserRequest.SetRequestHeader("Accept", "application/json");


        yield return DeleteUserRequest.SendWebRequest();

        //error handling sth sth
        if (DeleteUserRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(DeleteUserRequest.error);
            Debug.Log(DeleteUserRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(DeleteUserRequest.downloadHandler.text);
            SceneManager.LoadSceneAsync("Login Scene", LoadSceneMode.Single);

        }

    }
    public void BlockUserWrapper(string id)
    {
        StartCoroutine(BlockUser(id));
    }
    IEnumerator BlockUser(string id)
    {
        WWWForm form = new WWWForm();


        UnityWebRequest BlockUserRequest = UnityWebRequest.Post(Host + "/api/user/block/" + id, form);


        BlockUserRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        BlockUserRequest.SetRequestHeader("Accept", "application/json");


        yield return BlockUserRequest.SendWebRequest();

        //error handling sth sth
        if (BlockUserRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(BlockUserRequest.error);
            Debug.Log(BlockUserRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(BlockUserRequest.downloadHandler.text);
            OnUserBlocked?.Invoke();

        }
    }
    public static event Action OnUserBlocked;
    public static event Action OnUserUnBlocked;
    public void UnBlockUserWrapper(string id)
    {
        StartCoroutine(UnBlockUser(id));
    }
    IEnumerator UnBlockUser(string id)
    {
        WWWForm form = new WWWForm();


        UnityWebRequest UnBlockUserRequest = UnityWebRequest.Post(Host + "/api/user/unblock/" + id, form);


        UnBlockUserRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        UnBlockUserRequest.SetRequestHeader("Accept", "application/json");


        yield return UnBlockUserRequest.SendWebRequest();

        //error handling sth sth
        if (UnBlockUserRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(UnBlockUserRequest.error);
            Debug.Log(UnBlockUserRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(UnBlockUserRequest.downloadHandler.text);
            OnUserUnBlocked?.Invoke();
        }
    }
    public static event Action OnReportDone;

    public void ReportWrapper(string gameId, string userId, string category, string report)
    {
        StartCoroutine(Report( gameId,userId,category,report));
    }
    IEnumerator Report(string gameId, string userId, string category, string report)
    {
        WWWForm form = new WWWForm();
        form.AddField("game_id", gameId);
        form.AddField("user_id", userId);
        form.AddField("category", category);
        form.AddField("report", report);


        UnityWebRequest UnBlockUserRequest = UnityWebRequest.Post(Host + "/api/users/report", form);


        UnBlockUserRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        UnBlockUserRequest.SetRequestHeader("Accept", "application/json");


        yield return UnBlockUserRequest.SendWebRequest();

        //error handling sth sth
        if (UnBlockUserRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(UnBlockUserRequest.error);
            Debug.Log(UnBlockUserRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(UnBlockUserRequest.downloadHandler.text);
            OnReportDone?.Invoke();
        }
    }
    public void SendGoogleTokenWrap(string token){
        StartCoroutine(SendGoogleToken(token));
    }
    IEnumerator SendGoogleToken(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token",token);
        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/auth/google-login", form);
        // SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(SendPostRequest.downloadHandler.text);
            saveUser(ProfileInfo.Player.token);
            if (webSocetConnect.UIWebSocket != null)
                webSocetConnect.UIWebSocket.ConnectToWebSocket();
            GameVariables.isLoggedIn = true;
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
    public void SendFBTokenWrap(string token){
        StartCoroutine(SendFBToken(token));
    }
    IEnumerator SendFBToken(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token",token);
        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/auth/facebook-login", form);
        // SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(SendPostRequest.downloadHandler.text);
            GameVariables.isLoggedIn = true;
            saveUser(ProfileInfo.Player.token);

            if (webSocetConnect.UIWebSocket != null)
                webSocetConnect.UIWebSocket.ConnectToWebSocket();
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
    public void SendAppleTokenWrap(string token){
        StartCoroutine(SendAppleToken(token));
    }
    IEnumerator SendAppleToken(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token",token);
        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/auth/apple-login", form);
        // SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            ProfileInfo.Player = JsonUtility.FromJson<ProfileInfo.MyUser>(SendPostRequest.downloadHandler.text);
            saveUser(ProfileInfo.Player.token);
            if (webSocetConnect.UIWebSocket != null)
                webSocetConnect.UIWebSocket.ConnectToWebSocket();
            GameVariables.isLoggedIn = true;
            SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
        }
    }
    public void EditMyPasswordWrapper(string Password, string PasswordConfirm)
    {
        StartCoroutine(EditMyPassword(Password, PasswordConfirm));
    }

    public static event Action<string> OnEditPassword;


    IEnumerator EditMyPassword(string Password, string PasswordConfirm)
    {


        WWWForm form = new WWWForm();
        form.AddField("password", Password);
        form.AddField("password_confirmation", PasswordConfirm);

        UnityWebRequest www = UnityWebRequest.Post(Host + "/api/user/", form);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
            JObject stuff = JObject.Parse((www.downloadHandler.text));
            string message = stuff["message"].ToObject<string>();
            JObject errors = stuff["errors"].ToObject<JObject>();

            List<string> allErrors = new List<string>();
            foreach (var error in errors)
            {
                foreach (string errMsg in error.Value)
                {
                    allErrors.Add(errMsg);
                }
            }

            string combinedError = string.Join("\n", allErrors);

            OnEditPassword?.Invoke(combinedError);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            OnEditPassword?.Invoke("Your Password have been changed.");

        }
    }
    public void GET_BJPPOTWrapper(string id)
    {
        StartCoroutine(GET_BJPPOT(id));
    }
    public IEnumerator GET_BJPPOT(string tableid)
    {
        WWWForm form = new WWWForm();


        UnityWebRequest www = UnityWebRequest.Get(Host + "/api/get-jackpots?table_id=" + tableid);


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            //JObject stuff = JObject.Parse((www.downloadHandler.text).ToString());
            ProfileInfo.GamePots = JsonUtility.FromJson<ProfileInfo.Pots>( www.downloadHandler.text);

            //ProfileInfo.GamePots.bp1 = stuff["big_pot_1"].ToObject<long>();
            //ProfileInfo.GamePots.sp1 = stuff["small_pot_1"].ToObject<long>();
            //ProfileInfo.GamePots.bp2 = stuff["big_pot_2"].ToObject<long>();
            //ProfileInfo.GamePots.sp2 = stuff["small_pot_2"].ToObject<long>();
            //ProfileInfo.GamePots.bp3 = stuff["big_pot_3"].ToObject<long>();
            //ProfileInfo.GamePots.sp3 = stuff["small_pot_3"].ToObject<long>();
            //ProfileInfo.GamePots.bp4 = stuff["big_pot_4"].ToObject<long>();
            //ProfileInfo.GamePots.sp4 = stuff["small_pot_4"].ToObject<long>();
            //ProfileInfo.GamePots.bp5 = stuff["big_pot_5"].ToObject<long>();
            //ProfileInfo.GamePots.sp5 = stuff["small_pot_5"].ToObject<long>();
            //ProfileInfo.GamePots.bp6 = stuff["big_pot_6"].ToObject<long>();
            //ProfileInfo.GamePots.sp6 = stuff["small_pot_6"].ToObject<long>();
            //ProfileInfo.GamePots.bp7 = stuff["big_pot_7"].ToObject<long>();
            //ProfileInfo.GamePots.sp7 = stuff["small_pot_7"].ToObject<long>();
           


        }
    }











    public static event Action onSpItReady;

    public IEnumerator GetSpIt()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GetSpItRequest = UnityWebRequest.Get(Host + "/api/get-all-collectibles");


        GetSpItRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GetSpItRequest.SetRequestHeader("Accept", "application/json");


        yield return GetSpItRequest.SendWebRequest();

        //error handling sth sth
        if (GetSpItRequest.result != UnityWebRequest.Result.Success)
        {
             Debug.Log("Form asdasdasd complete!");

            Debug.Log(GetSpItRequest.error);
            Debug.Log(GetSpItRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(GetSpItRequest.downloadHandler.text);
            SpiItemList GetSpItems = JsonUtility.FromJson<SpiItemList>("{\"items\":" + GetSpItRequest.downloadHandler.text + "}");
            ProfileInfo.SpItems = GetSpItems;
            onSpItReady?.Invoke();
        }
    }
public void GetSpItWrapper(){
        StartCoroutine(GetSpIt());
    }


    public static event Action onSpItSet;
    public void SendSpItWrapper(string id)
    {
        StartCoroutine(SendSpIt(id));
    }
    IEnumerator SendSpIt(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("collectible_id", id);

        UnityWebRequest SpItRequest = UnityWebRequest.Post(Host + "/api/set-collectible", form);


        SpItRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        SpItRequest.SetRequestHeader("Accept", "application/json");


        yield return SpItRequest.SendWebRequest();

        //error handling sth sth
        if (SpItRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SpItRequest.error);
            Debug.Log(SpItRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(SpItRequest.downloadHandler.text);
            onSpItSet?.Invoke();
        }
    }



    public static event Action onLangReady;
    public IEnumerator GetLang()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest GetSpItRequest = UnityWebRequest.Get(Host + "/api/translations");


        // GetSpItRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        GetSpItRequest.SetRequestHeader("Accept", "application/json");

        // Debug.Log("alslam 3lekm");
        yield return GetSpItRequest.SendWebRequest();

        //error handling sth sth
        if (GetSpItRequest.result != UnityWebRequest.Result.Success)
        {

            Debug.Log(GetSpItRequest.error);
            Debug.Log(GetSpItRequest.downloadHandler.text);
        }
        else
        {
            languageManager.myLangs = JsonUtility.FromJson<languageManager.LanguageContainer>("{\"languages\":" + GetSpItRequest.downloadHandler.text + "}");
            languageManager.myLangs.getCodes();            
            onLangReady?.Invoke();
        }
    }
public void GetLangWrapper(){
        StartCoroutine(GetLang());
    }






    public void SendMyLangWrapper()
    {
        StartCoroutine(SendMyLang());
    }
    IEnumerator SendMyLang()
    {
        WWWForm form = new WWWForm();
        form.AddField("language_id", languageManager.myLangs.SetLangId);

        UnityWebRequest SpItRequest = UnityWebRequest.Post(Host + "/api/user/set-language", form);


        SpItRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        SpItRequest.SetRequestHeader("Accept", "application/json");


        yield return SpItRequest.SendWebRequest();

        //error handling sth sth
        if (SpItRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SpItRequest.error);
            Debug.Log(SpItRequest.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(SpItRequest.downloadHandler.text);
        }
    }





public static event Action onPaymentReady;
public void GET_PaymentWrapper()
    {
        StartCoroutine(GET_Payment());
    }
    public IEnumerator GET_Payment()
    {
        WWWForm form = new WWWForm();


        UnityWebRequest www = UnityWebRequest.Get(Host + "/api/payments");


        www.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        www.SetRequestHeader("Accept", "application/json");


        yield return www.SendWebRequest();

        //error handling sth sth
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            pymentItems mypaymentitem = JsonUtility.FromJson<pymentItems>("{\"items\":" + www.downloadHandler.text + "}");
           paymentVars.payments=mypaymentitem;
            onPaymentReady?.Invoke();

        }
    }


public openLink openHolder;

public void SendCheckOutWrap(string pid){
        StartCoroutine(SendCheckOut(pid));
    }
    IEnumerator SendCheckOut(string pid)
    {
        WWWForm form = new WWWForm();
        form.AddField("product_id",pid);
        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/create-checkout-session", form);
        SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {
        
            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            JObject jsonResponse = JObject.Parse(SendPostRequest.downloadHandler.text);
            string url = (string)jsonResponse["url"];
            Debug.Log("Parsed URL: " + url);
            openHolder.OpenURL(url);
        }
    }
    public void SubscribeWrap(string tiere)
    {
        StartCoroutine(Subscribe(tiere));
    }
    IEnumerator Subscribe(string tiere)
    {
        WWWForm form = new WWWForm();
        form.AddField("product_id", tiere);

        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/create-subscription", form);
        SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            JObject jsonResponse = JObject.Parse(SendPostRequest.downloadHandler.text);
            string url = (string)jsonResponse["url"];
            Debug.Log("Parsed URL: " + url);
            openHolder.OpenURL(url);
        }
    }

    public void ManagePaymentsWraper()
    {
        StartCoroutine(ManagePayments());
    }
    IEnumerator ManagePayments()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest SendPostRequest = UnityWebRequest.Post(Host + "/api/manage-plans", form);
        SendPostRequest.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);
        SendPostRequest.SetRequestHeader("Accept", "application/json");
        yield return SendPostRequest.SendWebRequest();
        if (SendPostRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(SendPostRequest.error);
            Debug.Log(SendPostRequest.downloadHandler.text);
        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log(SendPostRequest.downloadHandler.text);
            JObject jsonResponse = JObject.Parse(SendPostRequest.downloadHandler.text);
            string url = (string)jsonResponse["url"];
            Debug.Log("Parsed URL: " + url);
            openHolder.OpenURL(url);
        }
    }







    public void SendApplePrWrapper(string id,string rec)
    {
        StartCoroutine(SendApplePr(id,rec));
    }
    IEnumerator SendApplePr(string productId,string receit)
    {
        WWWForm form = new WWWForm();
        form.AddField("ProductId", productId);
        form.AddField("transactionId", receit);
        

        UnityWebRequest PurchesDone = UnityWebRequest.Post(Host + "/api/end-apple-session", form);


        PurchesDone.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        PurchesDone.SetRequestHeader("Accept", "application/json");


        yield return PurchesDone.SendWebRequest();

        //error handling sth sth
        if (PurchesDone.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(PurchesDone.error);
            Debug.Log(PurchesDone.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(PurchesDone.downloadHandler.text);
        }
    }




    public void SendGooglePrWrapper(string id,string rec)
    {
        StartCoroutine(SendGooglePr(id,rec));
    }
    IEnumerator SendGooglePr(string productId,string receit)
    {
        Debug.Log(productId);
        Debug.Log(receit);
        WWWForm form = new WWWForm();
        form.AddField("ProductId", productId);
        form.AddField("transactionId", receit);
        

        UnityWebRequest PurchesDone = UnityWebRequest.Post(Host + "/api/end-google-session", form);


        PurchesDone.SetRequestHeader("Authorization", "Bearer " + ProfileInfo.Player.token);

        PurchesDone.SetRequestHeader("Accept", "application/json");


        yield return PurchesDone.SendWebRequest();

        //error handling sth sth
        if (PurchesDone.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(PurchesDone.error);
            Debug.Log(PurchesDone.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(PurchesDone.downloadHandler.text);
        }
    }
}
