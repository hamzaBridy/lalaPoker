    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    public class setCardSprite : MonoBehaviour
    {
        public GameObject cardDicGameObj;
        public Sprite defaultCardSp;
        public int cardId;
        public GameObject playerParentPref;
        private Color originalColor;
        private bool isFlipped = false;
        public GameObject border;
        private float duration = 0.4f; 
        private Quaternion originalRotation;
        void Start()
        {

            originalColor = GetComponent<UnityEngine.UI.Image>().color;    
            originalRotation = transform.rotation;
            
            webSocetConnect.UIWebSocket.MainTable.OnPrivateCardsDelt+=setCardSpriteFun;
            webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart+=setDefaultCardSprite;
            webSocetConnect.UIWebSocket.MainTable.OnWin+=setCardSpriteFun;
            PlayersManager.OnSingleUser+=setDefaultCardSprite;
            
        }

        // Update is called once per frame
        void setCardSpriteFun(){
            string suit=playerParentPref.GetComponent<PlayerGameId>().player.playerCards[cardId].suit;
            string value=playerParentPref.GetComponent<PlayerGameId>().player.playerCards[cardId].value;
            List<int> winPrio=new List<int>();
            foreach (var item in playerParentPref.GetComponent<PlayerGameId>().player.playerCards[cardId].IsWiningCard)
            {
                winPrio.Add(item.Key);
            }
            //int iswin=playerParentPref.GetComponent<PlayerGameId>().player.playerCards[cardId].IsWiningCard;
            GetComponent<UnityEngine.UI.Image>().enabled=true;
            if(winPrio.Count>0){
                foreach (var item in winPrio)
                {
                    StartCoroutine(potDelay(GameVariables.maxpotProi-item));    
                }
                
            }
            else{
                border.GetComponent<UnityEngine.UI.Image>().enabled=false;
                // GetComponent<UnityEngine.UI.Image>().color=originalColor;
            }
            if(suit=="-1"){
                GetComponent<UnityEngine.UI.Image>().enabled=false;
                GetComponent<UnityEngine.UI.Image>().sprite=defaultCardSp;
            }
            else{
            StartCoroutine(FlipCardAnimation(cardDicGameObj.GetComponent<cardsDict>().getCardSprite(suit, value)));
            }
        }
        IEnumerator potDelay(float delayTime)
    {
    yield return new WaitForSeconds(2*delayTime);
    border.GetComponent<UnityEngine.UI.Image>().enabled=true;
    yield return new WaitForSeconds(1.5f);
    border.GetComponent<UnityEngine.UI.Image>().enabled=false;
    //    GetComponent<UnityEngine.UI.Image>().color=new Color(1f, 0f, 0f, 0.5f);
    }
        void setDefaultCardSprite(){
            GetComponent<UnityEngine.UI.Image>().enabled=false;
            GetComponent<UnityEngine.UI.Image>().sprite=defaultCardSp;
            // GetComponent<UnityEngine.UI.Image>().color=originalColor;
            border.GetComponent<UnityEngine.UI.Image>().enabled=false;
            isFlipped = false;
        }
        void OnDestroy()
        {
            webSocetConnect.UIWebSocket.MainTable.OnPrivateCardsDelt-=setCardSpriteFun;
            webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart-=setDefaultCardSprite;
            webSocetConnect.UIWebSocket.MainTable.OnWin-=setCardSpriteFun;
            PlayersManager.OnSingleUser-=setDefaultCardSprite;
        }

        IEnumerator FlipCardAnimation(Sprite newCardImage)
        {
            
            if (isFlipped)
            {
                GetComponent<Image>().sprite = newCardImage;
                yield break;
            }

            
            
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.Rotate(Vector3.up, 90f / duration * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            isFlipped = !isFlipped;
            GetComponent<Image>().sprite = newCardImage;

            while (elapsed > 0f)
            {
                transform.Rotate(Vector3.up, -90f / duration * Time.deltaTime);
                elapsed -= Time.deltaTime;
                yield return null;
            }
            transform.rotation = originalRotation;
        }
    }
