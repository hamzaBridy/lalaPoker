
//using System.Threading.Tasks;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.SocialPlatforms;
//using Unity.Services.Authentication;
//using Unity.Services.Core;
//using UnityEngine;
//using TMPro;

//public class GoogleLogin : MonoBehaviour
//{
//    public APIcalls apiHolder;
//    public TextMeshProUGUI ttt;
//    private void Start()
//    {
//        InitializePlayGamesLogin();
//    }
//    void InitializePlayGamesLogin()
//    {
//        ttt.text += "1";
//        var config = new GooglePlayGames.BasicApi.PlayGamesClientConfiguration.Builder()
//            // Requests an ID token be generated.  
//            // This OAuth token can be used to
//            // identify the player to other services such as Firebase.

//            .RequestIdToken()
//            .Build();

//        PlayGamesPlatform.InitializeInstance(config);
//        PlayGamesPlatform.DebugLogEnabled = true;
//        PlayGamesPlatform.Activate();
//    }

//    public void LoginGoogle()
//    {
//        ttt.text += "2";
//        Social.localUser.Authenticate(OnGoogleLogin);
//    }

//    void OnGoogleLogin(bool success)
//    {
//        ttt.text += "3";
//        if (success)
//        {
//            ttt.text += "31";
//            string MyToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
//            //apiHolder.SendGoogleTokenWrap(MyToken);
//            // Call Unity Authentication SDK to sign in or link with Google.
//            Debug.Log("Login with Google done. IdToken: " + ((PlayGamesLocalUser)Social.localUser).GetIdToken());

//        }
//        else
//        {
//            ttt.text += "32";
//            Debug.Log("Unsuccessful login");
//        }
//    }
//    async Task SignInWithGoogleAsync(string idToken)
//    {
//        try
//        {
//            await AuthenticationService.Instance.SignInWithGoogleAsync(idToken);
//            Debug.Log("SignIn is successful.");
//        }
//        catch (AuthenticationException ex)
//        {
//            // Compare error code to AuthenticationErrorCodes
//            // Notify the player with the proper error message
//            Debug.LogException(ex);
//        }
//        catch (RequestFailedException ex)
//        {
//            // Compare error code to CommonErrorCodes
//            // Notify the player with the proper error message
//            Debug.LogException(ex);
//        }
//    }

//}
