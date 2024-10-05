
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardsManger : MonoBehaviour
{
    public Transform[] childernContainer;
    public GameObject hand, level, ljp, howToButton;
    private void OnEnable()
    {
        level.SetActive(false);
        ljp.SetActive(false);
        hand.SetActive(true);
        howToButton.SetActive(true);
    }

    private void OnDisable()
    {
       
        for(int i =0; i< childernContainer.Length; i++)
        {
         foreach (Transform child in childernContainer[i])
        {
                RewardPrefabScript friendReqInfo = child.GetComponent<RewardPrefabScript>();
            if (friendReqInfo != null && !child.IsDestroyed())
            {
                Destroy(child.gameObject);
            }
        }
        }
        level.SetActive(false);
        ljp.SetActive(false);
        hand.SetActive(true);
    }
}
