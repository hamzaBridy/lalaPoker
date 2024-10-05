using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class tableCards : MonoBehaviour
{
    public GameObject cardDic;
    public GameObject cardPrf;
    public List<GameObject> cardPos;
    public GameObject startDealingLocation;
    public GameObject dealer;
    List<GameObject> NowCards;
    void Start()
    {

        NowCards =new List<GameObject>();
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt+=getTableCards;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart+=resetTableCards;
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt+=deal;
        PlayersManager.OnSingleUser+=resetTableCards;
        getTableCards();
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt-=getTableCards;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart-=resetTableCards;
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt-=deal;
        PlayersManager.OnSingleUser-=resetTableCards;
    }
    void deal(){
        StartCoroutine(dealing());
    }
        IEnumerator dealing()
    {
        dealer.GetComponent<SkeletonAnimation>().AnimationName="Throwing";
        

        yield return new WaitForSeconds(1);
        dealer.GetComponent<SkeletonAnimation>().AnimationName="Idle";
    //    GetComponent<UnityEngine.UI.Image>().color=new Color(1f, 0f, 0f, 0.5f);
    }
    void getTableCards(){
        
        List<card> TCards=webSocetConnect.UIWebSocket.MainTable.tableCards;
        int firstLocationIndex=NowCards.Count;
        
        for(int i=NowCards.Count;i<TCards.Count;i++)
        {
            GameObject cPf=Instantiate(cardPrf);
            string suit=TCards[i].suit;
            string value=TCards[i].value;
            Sprite newCardImage=cardDic.GetComponent<cardsDict>().getCardSprite(suit,value);
            //cPf.GetComponent<UnityEngine.UI.Image>().sprite=newCardImage;
            cPf.GetComponent<setCardSpriteTable>().frontSp=newCardImage;
            cPf.GetComponent<setCardSpriteTable>().cardtableId=i;
            cPf.GetComponent<setCardSpriteTable>().animationPosition1=cardPos[firstLocationIndex];
            cPf.GetComponent<setCardSpriteTable>().animationPosition2=cardPos[i];
            cPf.transform.SetParent(startDealingLocation.transform, false);
            //cPf.transform.SetParent(cardPos[i].transform, false);
            NowCards.Add(cPf);
        }
    }
    void resetTableCards(){
        foreach (var obj in NowCards)
        {
            Destroy(obj);
        }
        NowCards=new List<GameObject>();
    }
}
