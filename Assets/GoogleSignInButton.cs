#if UNITY_ANDROID

using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using System;
public class GoogleSignInButton : MonoBehaviour
{

    public APIcalls apiHolder;
    private void Start()
    {


        // Attach the button click event to the method that handles the Google Sign-In
        Button signInButton = GetComponent<Button>();
        signInButton.onClick.AddListener(OnSignInButtonClick);
        var config = new PlayGamesClientConfiguration.Builder()
            .AddOauthScope("profile")
            .AddOauthScope("email")
     // Requests an ID token be generated.  
     // This OAuth token can be used to
     // identify the player to other services such as Firebase.
     .RequestIdToken()
     .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void OnSignInButtonClick()
    {
        Social.localUser.Authenticate(OnGoogleLogin);

    }

    private void OnGoogleLogin(bool success)
    {

        if (success)
        {

            // If sign-in is successful, log the Google ID Token
            LogGoogleIdToken();
        }
        else
        {
            Debug.Log("Google Sign-In unsuccessful");

        }

    }

    private void LogGoogleIdToken()
    {
        // Debug.Log(Social.localUser.ToString());

        apiHolder.SendGoogleTokenWrap(((PlayGamesLocalUser)Social.localUser).GetIdToken());
        //try
        //{
        //     PlayGamesPlatform.Instance.RequestServerSideAccess(false, code =>
        //{

        //        // Now send this code to your server where you can exchange it for an access token.
        //        // SendAuthCodeToServer(code);
        //        apiHolder.SendGoogleTokenWrap(code);

        //});
        //}
        //catch (Exception ex)
        //{
        //}
        //    apiHolder.SendGoogleTokenWrap(PlayGamesPlatform.Instance.RequestServerSideAccess);


        // You can use the idToken for further processing, such as sending it to your server
    }
}
#endif

