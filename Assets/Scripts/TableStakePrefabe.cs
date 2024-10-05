using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class TableStakePrefabe : MonoBehaviour
{
  public string gameType;
  public long buyIn;
  private Button myButton;
  public webSocetConnect webSocetConnectRef;
  public GameObject openedStake,closedStake;
    [SerializeField] private TextMeshProUGUI myLjp, buyinTextO, buyinTextC;
   void Start()
    {
      // webSocetConnectRef= FindObjectOfType<webSocetConnect>();
        myButton = GetComponent<Button>();
        
        myButton.onClick.AddListener(OnButtonClick); 
        buyinTextO.text ="\n" +  MoneyConverter.ConvertMoney(buyIn * 1000000);
        buyinTextC.text = "\n" + MoneyConverter.ConvertMoney(buyIn * 1000000);
        getPots();

    }
    void getPots()
    {

        switch (gameType)
        {
            case "1":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_1);
                break;
            case "2":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_2);
                break;
            case "3":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_3);
                break;
            case "4":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_4);
                break;
            case "5":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_5);
                break;
            case "6":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_6);
                break;
            case "7":
                myLjp.text = MoneyConverter.ConvertMoney(ProfileInfo.GamePots.big_pot_7);
                break;
            default:
                Debug.Log("Invalid game type.");
                myLjp.text = "N/A"; // Default text for invalid cases
                break;
        }


    }
    void OnButtonClick()
    {
      if(openedStake.activeInHierarchy)
      {
        //Debug.Log(gameType);
       // Debug.Log(GameVariables.gameSize);
        
        webSocetConnectRef.findGame(gameType,GameVariables.gameSize);
      }
      else
      {
        Debug.Log("WAZUPPP");
        //open the addcash menu later
      }
    }
  void OnEnable()
  {
    if(ProfileInfo.MyPlayer.chips>(buyIn * 1000000))
    {
        openedStake.SetActive(true);
        closedStake.SetActive(false);
    }
    else
    {
         openedStake.SetActive(false);
         closedStake.SetActive(true);
    }
  }
    

    }
