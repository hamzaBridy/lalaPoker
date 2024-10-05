using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static languageManager;

public class langDrp : MonoBehaviour
{
   public TMP_Dropdown languageDropdown;
    public TextMeshProUGUI selectedLanguageText;
    public static event Action onLangSelCha;

    public static int DrIndex=0;



    private string selectedLanguage;
    public APIcalls apiHolder=null;
    void Start()
    {
        if(languageManager.myLangs!=null){
        InitializeDropdown();
        selectedLanguage = languageManager.myLangs.mylang;
        //selectedLanguageText.text = languageManager.myLangs.mylang;
        languageDropdown.value=DrIndex;
        }
        APIcalls.onLangReady+=InitializeDropdown;
    }

    void InitializeDropdown()
    {
        languageDropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        foreach (string lang in languageManager.myLangs.langCodes)
        {
            dropdownOptions.Add(new TMP_Dropdown.OptionData(lang));
        }

        languageDropdown.AddOptions(dropdownOptions);

        languageDropdown.onValueChanged.AddListener(OnLanguageDropdownValueChanged);

        selectedLanguage = languageManager.myLangs.langCodes[0];
        selectedLanguageText.text = languageManager.myLangs.langCodes[0];
    }



    void OnLanguageDropdownValueChanged(int index)
    {
        selectedLanguage = languageManager.myLangs.langCodes[index];
        selectedLanguageText.text = languageManager.myLangs.langCodes[index];
        languageManager.myLangs.mylang= languageManager.myLangs.langCodes[index];
        languageManager.myLangs.SetLangId=languageManager.myLangs.languages[index].id;
        DrIndex=index;
        if(apiHolder!=null)
        apiHolder.SendMyLangWrapper();
        onLangSelCha?.Invoke();

        // Additional actions based on the selected language can be performed here
    }
}
