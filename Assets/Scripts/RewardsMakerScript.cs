using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ProfileInfo;

public class RewardsMakerScript : MonoBehaviour
{
    [SerializeField] APIcalls aPIcallsRef;
    [SerializeField] Transform handRewardC, levelRewardC, bjpRewardC;
    [SerializeField] GameObject handRewardP, levelRewardP, bjpRewardP;
    private List<GameObject>  rewardsList = new List<GameObject>();

     void Start()
    {
        aPIcallsRef.OnGetRewards += CreatRewards;
    }
     void OnDestroy()
    {
        aPIcallsRef.OnGetRewards -= CreatRewards;

    }

    void CreatRewards(RewardList rewardList)
    {
        rewardsList.Clear();
        foreach (Reward reward in rewardList.rewards)
        {
            Debug.Log(rewardList.rewards.Count);

            GameObject newReward = null;

            if (reward.reward.type == "level")
            {
                newReward = Instantiate(levelRewardP, levelRewardC);
            }
            else if (reward.reward.type == "bjp")
            {
                newReward = Instantiate(bjpRewardP, bjpRewardC);
            }
            else if (reward.reward.type == "hand_reward")
            {
                newReward = Instantiate(handRewardP, handRewardC);
            }
            rewardsList.Add(newReward);
            newReward.GetComponent<RewardPrefabScript>().aPIcallsRef = aPIcallsRef;
            newReward.GetComponent<RewardPrefabScript>().SetData(reward);

        }
    }
}
