using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closePrfileinLobby : MonoBehaviour
{
    [SerializeField] GameObject iamgesAndTimers, friendsMenu,mainMenu, nameTaken, profileMenu;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(closeMenu);
    }
    void closeMenu()
    {
        if(iamgesAndTimers.activeInHierarchy)
        {
            profileMenu.SetActive(false);
            iamgesAndTimers.SetActive(false);
            nameTaken.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            profileMenu.SetActive(false);
            nameTaken.SetActive(false);

        }
    }
}
