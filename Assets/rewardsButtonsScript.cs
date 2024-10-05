using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class rewardsButtonsScript : MonoBehaviour
{
    [SerializeField] private Button handReward, levelReward, Ljp;
    [SerializeField] private TextMeshProUGUI rewardsHeaders;
    private void OnEnable()
    {
        HandRewardButton();
    }
    private void OnDisable()
    {
        HandRewardButton();

    }
    private void Start()
    {
        handReward.onClick.AddListener(HandRewardButton);
        levelReward.onClick.AddListener(LevelRewardButton);
        Ljp.onClick.AddListener(LJP_Rewards);
    }
    void HandRewardButton()
    {
        Debug.Log("HandRewardButton");
        rewardsHeaders.text = getTranslate.getWordByDict("hrewards");
    }
    void LevelRewardButton()
    {
        Debug.Log("LevelRewardButton");

        rewardsHeaders.text = getTranslate.getWordByDict("lrewards");
    }
    void LJP_Rewards()
    {
        rewardsHeaders.text = getTranslate.getWordByDict("ljprewards");
    }
}
