using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using TMPro;
using System;
public class FacebookManager : MonoBehaviour
{
   // public TextMeshProUGUI text;
    public APIcalls apiHolder;
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            try
            {
         //       text.text += "1";
                // Initialize the Facebook SDK
                FB.Init(InitCallback, OnHideUnity);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
        else
        {
      //      text.text += "-1";
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
    //    text.text+="in ini";
        if (FB.IsInitialized)
        {
      //      text.text+="2";
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
        //    text.text+="-2";
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
       //     text.text+="3";
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
       //     text.text+="-3";
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
   
    //login
    public void Facebook_LogIn()
    {
     //   text.text+="0";
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");
        //permissions.Add("user_friends");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);

    }
    void AuthCallBack(IResult result)
    {
        if (FB.IsLoggedIn)
        {
         //   text.text+="4";
          //  SetInit();
            //AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;

            // FB.API("/me?fields=email", HttpMethod.GET, GetEmailCallback);

            apiHolder.SendFBTokenWrap(aToken.ToJson());
            print(aToken.ToJson());

            foreach (string perm in aToken.Permissions)
            {
                print(perm);
            }
        }
        else
        {
          //  text.text+="-4";
            print("Failed to log in");
        }

    }



    void GetEmailCallback(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error) && result.ResultDictionary != null)
        {
            foreach(string perm in result.ResultDictionary.Keys) {
                Debug.Log(result.ResultDictionary[perm]);
                print(perm); }
                
            if (result.ResultDictionary.ContainsKey("email"))
            {
                string email = result.ResultDictionary["email"] as string;
                var aToken = AccessToken.CurrentAccessToken;
                Debug.Log("Access Token: " + aToken.TokenString);
                var combinedData = new Dictionary<string, string>
            {
                { "email", email },
                { "accessToken", aToken.TokenString },
                // Include other token fields if needed
            };
                Debug.Log(email);
                string combinedJson = JsonUtility.ToJson(combinedData);
                Debug.Log(combinedJson);
                apiHolder.SendFBTokenWrap(combinedJson); // Send combined data to your API holder
            }
            else
            {
                Debug.Log("Email permission was not granted or the user's email is not available.");
            }
        }
        else
        {
            Debug.Log("Error getting user email: " + result.ErrorDictionary);
            Debug.Log("Error getting user email: " + result.Error);
            Debug.Log("Result Dictionary: " + result.RawResult);

        }
    }


    //logout
    public void Facebook_LogOut()
    {
        StartCoroutine(LogOut());
    }
    IEnumerator LogOut()
    {
        FB.LogOut();
        while (FB.IsLoggedIn)
        {
            print("Logging Out");
            yield return null;
        }
        print("Logout Successful");
        // if (FB_profilePic != null) FB_profilePic.sprite = null;
        //if (FB_userName != null) FB_userName.text = "";
        //if (rawImg != null) rawImg.texture = null;
    }

}
