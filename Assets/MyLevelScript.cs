using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyLevelScript : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        levelText.text = ProfileInfo.MyPlayer.level;
        APIcalls.OnGetStatues += UpdateMyLevel;
    }
    private void OnDestroy()
    {
        APIcalls.OnGetStatues -= UpdateMyLevel;

    }
    void UpdateMyLevel()
    {
        levelText.text = ProfileInfo.MyPlayer.level;

    }
    void Update(){
        if(ProfileInfo.MyPlayer.level!=null){
            UpdateMyLevel();
        }
    }
}
