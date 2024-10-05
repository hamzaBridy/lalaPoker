using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerSettings : MonoBehaviour
{
    [System.Serializable]
    public class PlayerSettings
{
    public bool chat=true;
    public bool chatBubbles=true;
    public bool sound=true;
    public bool video=true;
    public bool HandStr=true;
    public bool viber =true;
    public bool getValue(string key){
        switch (key)
        {
            case "chat":
                return chat;
            case "chatBubbles":
                return chatBubbles;
            case "sound":
                return sound;
            case "video":
                return video;
            case "viber":
                return viber;
            case "HandStr":
                return HandStr;
            default:
                // Handle the case where the provided key is not recognized
                Debug.LogWarning("Unrecognized key: " + key);
                return true; // or throw an exception, depending on your preference
        }
    }
}
    public static PlayerSettings settings;




    public static event Action<bool> OnSettingsChanged;
    public static void SaveSettings(bool chat, bool chatBubbles,bool sound,bool video,bool HandStr,bool viber)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerSettings.json");
        settings = new PlayerSettings
        {
            chat=chat,
            chatBubbles=chatBubbles,
            sound=sound,
            video=video,
            HandStr=HandStr,
            viber=viber,
        };
        OnSettingsChanged?.Invoke(sound);
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(filePath, json);
    }
    

    public static void LoadSettings()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerSettings.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            settings = JsonUtility.FromJson<PlayerSettings>(json);
        }
        else{
            settings=new PlayerSettings();
        }
    }
    
}
