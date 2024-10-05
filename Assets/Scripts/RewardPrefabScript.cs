using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ProfileInfo;

public class RewardPrefabScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rewardChipsText, rewardConditionText, rewardsizeText;
    [SerializeField] Button claimbutton;

    private string rewardId;
    public APIcalls aPIcallsRef;
    private void Start()
    {
        claimbutton.onClick.AddListener(OnClaimButtonClicked);

    }
    public void SetData(Reward reward)
    {
        rewardId = reward.id.ToString();
        Debug.Log(reward.chips);

        if (rewardChipsText != null)
            rewardChipsText.text ="$" +  reward.chips.ToString("N0");
        else
            Debug.LogError("rewardChipsText is not assigned in the inspector");

        switch (reward.reward.type)
        {
            case "level":
                if (rewardConditionText != null)
                {
                    rewardConditionText.text = "Level: " + reward.reward.level_condition.ToString();
                }
                else
                    Debug.LogError("rewardTypeText or rewardConditionText is not assigned in the inspector");

                break;

            case "bjp":
                if (rewardsizeText != null)
                {
                    rewardsizeText.text = reward.reward.table_size_condition.ToString();
                }
                else
                    Debug.LogError("rewardTypeText or rewardsizeText is not assigned in the inspector");

                break;

            case "hand_reward":
                if (rewardsizeText != null && rewardConditionText != null)
                {
                    rewardsizeText.text = reward.reward.table_size_condition.ToString();
                    rewardConditionText.text = reward.reward.hand_condition;
                }
                else
                    Debug.LogError("rewardTypeText or rewardsizeText or rewardConditionText is not assigned in the inspector");

                break;
        }
    }
    private void OnClaimButtonClicked()
    {
        Debug.Log("Claim button was clicked."); // Add this line
        Debug.Log(rewardId);
        


        aPIcallsRef.ClaimrewardsWraper(rewardId, this);
    }
}
