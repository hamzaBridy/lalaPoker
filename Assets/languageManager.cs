using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class languageManager : MonoBehaviour
{
    public GameObject apiRef;
    [System.Serializable]
    
public class LanguageData
{
    public int id;
    public string language_name;
    public string language_code;
    public string image;
    public List<ValueData> values;
    public string created_at;
    public string updated_at;
}

[System.Serializable]
public class ValueData
{
    public string name;
    public string value;
    public string font_size;
}

[System.Serializable]
public class LanguageContainer
{
    public List<LanguageData> languages;
    public List<string> langCodes;
    public string mylang;
    public int SetLangId;
    public string getWorkByKey(string code,string key){
        foreach (LanguageData languageData in languages)
            {
                if(code==languageData.language_code){
                    foreach (languageManager.ValueData valueData in languageData.values)
                        {
                            if(valueData.name==key){
                                return valueData.value;
                            }
                        }
                }
                
            }
        return "";
    }
    public void getCodes(){
        mylang=languages[0].language_code;
        SetLangId=languages[0].id;
        foreach (LanguageData languageData in languages)
            {
                langCodes.Add(languageData.language_code);
            }
    }
}
public static LanguageContainer myLangs=null;
public static TMP_FontAsset arabicFont;
public static TMP_FontAsset defaultFont;
public  TMP_FontAsset localarabicFont;
public  TMP_FontAsset localdefaultFont;
void Start()
{
    arabicFont=localarabicFont;
    defaultFont=localdefaultFont;
    apiRef.GetComponent<APIcalls>().GetLangWrapper();
}
}
