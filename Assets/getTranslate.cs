using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ArabicSupport;
using System;
using System.Linq;
public class getTranslate : MonoBehaviour
{
    
    public string key;
    private TMP_Text myText;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        myText.alignment = GetComponent<TextMeshProUGUI>().alignment;

        if (languageManager.myLangs!=null){
            if(languageManager.myLangs.mylang=="ar"){
                GetComponent<TextMeshProUGUI>().font=languageManager.arabicFont;
                GetComponent<TextMeshProUGUI>().text=ArabicFixer.Fix(languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,key));
                string revTemp=GetComponent<TextMeshProUGUI>().text;
                GetComponent<TextMeshProUGUI>().text = new string(revTemp.Reverse().ToArray());
                // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                GetComponent<TextMeshProUGUI>().isRightToLeftText=true;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().font=languageManager.defaultFont;
                GetComponent<TextMeshProUGUI>().text=languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,key);
                // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                GetComponent<TextMeshProUGUI>().isRightToLeftText=false;
            }
        }
        APIcalls.onLangReady+=setLang;
        langDrp.onLangSelCha+=setLang;
    }

    // Update is called once per frame
    void setLang()
    {
        
            if(languageManager.myLangs.mylang=="ar"){
                GetComponent<TextMeshProUGUI>().font=languageManager.arabicFont;
                GetComponent<TextMeshProUGUI>().text=ArabicFixer.Fix(languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,key));
                string revTemp=GetComponent<TextMeshProUGUI>().text;
                GetComponent<TextMeshProUGUI>().text = new string(revTemp.Reverse().ToArray());
            if(!(myText.verticalAlignment == VerticalAlignmentOptions.Middle))
            GetComponent<TextMeshProUGUI>().alignment  = TextAlignmentOptions.Bottom;

            // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
            GetComponent<TextMeshProUGUI>().isRightToLeftText=true;
            }
            else
            {
                GetComponent<TextMeshProUGUI>().font=languageManager.defaultFont;
                GetComponent<TextMeshProUGUI>().text=languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,key);
            GetComponent<TextMeshProUGUI>().verticalAlignment = myText.verticalAlignment;

            // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
            GetComponent<TextMeshProUGUI>().isRightToLeftText=false;
            }
    }
    
    public static string getWordByDict(string nkey){
        if(languageManager.myLangs.mylang=="ar"){
                
                string revTemp=ArabicFixer.Fix(languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,nkey));
                
                revTemp = new string(revTemp.Reverse().ToArray());
                // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                return revTemp;
            }
            else
            {
                // GetComponent<TextMeshProUGUI>().font=languageManager.defaultFont;
                string mtext=languageManager.myLangs.getWorkByKey(languageManager.myLangs.mylang,nkey);
                // GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                // GetComponent<TextMeshProUGUI>().isRightToLeftText=false;
                return mtext;
            }
        
    }
    void OnDestroy()
    {
        APIcalls.onLangReady-=setLang;
        langDrp.onLangSelCha-=setLang;
    }
}
