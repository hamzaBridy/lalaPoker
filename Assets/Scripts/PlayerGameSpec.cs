using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerGameId : MonoBehaviour
{
    public GameObject GifDestination;
    public GameObject GiftButton;
    public string PlayerId;
    public JplayerData player;
    [SerializeField] private RawImage profileImage;
    [SerializeField] private TextMeshProUGUI profileChips, handStrength;
    [SerializeField] private Sprite callR, callL, raiseR, raiseL, bigBlindR, bigBlindL, smallBlindR, smallBlindL, fold, check;
    [SerializeField] private Image foldedImage, handStrImage;
    public int frontendSeat;
    public Transform betContainer;
    public GameObject betPrefab, allinAnimation;
    private Dictionary<string, Func<int, Sprite>> betTypeToSpriteFunction;
    private bool betTypeBool = false;
    private GameObject currentBetIndicator;
    private GameObject dealerPosition;
    private float betSpeed = 1.3f;
    public APIcalls aPIcalls;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => aPIcalls.getStatuesWrapper(player.userId));
        InitializeBetTypeToSpriteMap();
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet += updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet += enAniamtion;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart += NewRoundStarted;
        webSocetConnect.UIWebSocket.MainTable.OnWin += updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnWin += disAnimation;
        webSocetConnect.UIWebSocket.MainTable.OnAutoBuyIn += updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet += updateBetType;
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt += MoveBetToDealerWrapper;
        webSocetConnect.UIWebSocket.MainTable.OnPlayerJoind += updateChips;
        updateChips();
        if (handStrength != null)
            webSocetConnect.UIWebSocket.MainTable.OnHandStrengthChange += updateHandStrength;

        if (player.image != null)
        {
            StartCoroutine(LoadPhoto(profileImage, player.image));
        }
        dealerPosition = GameObject.FindGameObjectWithTag("collectBets");
        AdjustBetContainerPosition();
        AdjustSendGifPosition();

    }
    void disAnimation()
    {
        allinAnimation.SetActive(false);

    }
    void enAniamtion()
    {
        if (player.betType == "ALL_IN")
        {
            allinAnimation.SetActive(true);
        }

    }
    private IEnumerator LoadPhoto(RawImage profileImage, string imageUrl)
    {


        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D photoTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite photoSprite = Sprite.Create(photoTexture, new Rect(0, 0, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f));

            profileImage.texture = photoSprite.texture;

        }

    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnAutoBuyIn -= updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnNewRoundStart -= NewRoundStarted;
        webSocetConnect.UIWebSocket.MainTable.OnWin -= disAnimation;
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet -= updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet -= updateBetType;
        webSocetConnect.UIWebSocket.MainTable.OnPlayerJoind -= updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnGameCardsDelt -= MoveBetToDealerWrapper;
        webSocetConnect.UIWebSocket.MainTable.OnWin -= updateChips;
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet -= enAniamtion;

        if (handStrength != null)
            webSocetConnect.UIWebSocket.MainTable.OnHandStrengthChange -= updateHandStrength;

    }
    public void MoveBetToDealerWrapper()
    {
        //Debug.LogError(currentBetIndicator.GetComponent<Image>().sprite.name);
        //if (currentBetIndicator.GetComponent<Image>().sprite == fold || currentBetIndicator.GetComponent<Image>().sprite == check)
        //{
        //    Destroy(currentBetIndicator);

        //}
        //else
        //{
            StartCoroutine(MoveBetToDealer(dealerPosition.transform.position, betSpeed, currentBetIndicator));

       // }
    }
    private void InitializeBetTypeToSpriteMap()
    {

        betTypeToSpriteFunction = new Dictionary<string, Func<int, Sprite>>()
    {
        { "RAISE", (position) => position >= 5 ? raiseL : raiseR },
        { "CALL", (position) => position >= 5 ? callL : callR },
        { "BIG_BLIND", (position) => position >= 5 ? bigBlindL : bigBlindR },
        { "SMALL_BLIND", (position) => position >= 5 ? smallBlindL : smallBlindR },
        { "CHECK", (position) => check },
        { "FOLD", (position) => fold },
        { "ALL_IN", (position) => position >= 5 ? raiseL : raiseR },
    };
    }
    private void updateBetType()
    {
        if (webSocetConnect.UIWebSocket.MainTable.lastBetId == player.userId)
        {
            CreateAndPositionBetType();
        }
    }
    private void CreateAndPositionBetType()
    {
        if (currentBetIndicator != null)
        {
            Destroy(currentBetIndicator);
        }

        currentBetIndicator = Instantiate(betPrefab, this.transform);
        currentBetIndicator.transform.position = betContainer.transform.position;
        UpdateBetTypeSprite(currentBetIndicator, player.betType, frontendSeat);
        UpdateBetAmountText(currentBetIndicator, long.Parse(player.betAmount), frontendSeat, player.betType);
    }
    private void UpdateBetTypeSprite(GameObject betTypeObject, string playerBetType, int positionIndex)
    {
        if (betTypeToSpriteFunction.TryGetValue(playerBetType, out Func<int, Sprite> getSpriteFunc))
        {

            betTypeObject.GetComponent<Image>().sprite = getSpriteFunc(positionIndex);
            betTypeBool = positionIndex >= 5;
        }
        else
        {
            Debug.LogWarning($"Bet type sprite function not found for bet type: {playerBetType}");
        }
    }
    private void UpdateBetAmountText(GameObject betTypeObject, long betAmount, int positionIndex, string playerBetType)
    {
        if (playerBetType == "CHECK")
            return;

        if (playerBetType == "FOLD")
        {
            PlayerFolded();
            return;
        }
        TextMeshProUGUI betAmountText = betTypeObject.transform.Find("BetAmountText").GetComponent<TextMeshProUGUI>();
        if (betAmountText != null)
        {

            betAmountText.text = MoneyConverter.ConvertMoney(betAmount);

            // Debug.Log(player.betAmount);

            float textPositionX = !betTypeBool ? -32 : 32;
            Vector3 textPosition = betAmountText.transform.localPosition;
            betAmountText.transform.localPosition = new Vector3(textPositionX, textPosition.y, textPosition.z);
        }

    }
    void PlayerFolded()
    {
        foldedImage.GetComponent<Image>().enabled = true;
    }
    void NewRoundStarted()
    {
        foldedImage.GetComponent<Image>().enabled = false;
    }
    private void updateChips()
    {

        profileChips.text = "$" + MoneyConverter.ConvertMoney(long.Parse(player.stack));

    }
    private void updateHandStrength()
    {

        if (handStrength != null)
        {
            if (playerSettings.settings.HandStr && GameVariables.isPlaying)
            {
                handStrImage.gameObject.SetActive(true);
                handStrength.text = webSocetConnect.UIWebSocket.MainTable.handStrength;

            }
            else
            {

                handStrImage.gameObject.SetActive(false);
            }
        }

    }
    private IEnumerator MoveBetToDealer(Vector3 dealerPosition, float duration, GameObject moveableObject)
    {
        if (moveableObject != null)
        {
            if (currentBetIndicator.GetComponent<Image>().sprite == fold || currentBetIndicator.GetComponent<Image>().sprite == check)
            {
                Destroy(currentBetIndicator);
                yield break;
            }
            else
            {
                float elapsedTime = 0;
                Vector3 startPosition = moveableObject.transform.position;

                while (elapsedTime < duration)
                {
                    if (moveableObject == null)
                    {
                        yield break;
                    }

                    moveableObject.transform.position = Vector3.Lerp(startPosition, dealerPosition, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                if (moveableObject != null)
                {
                    moveableObject.transform.position = dealerPosition;
                    Destroy(moveableObject);
                }
            }
           
        }
    }
    private void AdjustBetContainerPosition()
    {
        Vector3 newPosition = new Vector3();
        if(GameVariables.gameSize == "9")
        {
            newPosition = CalculatePositionBasedOnSeat9(frontendSeat);
        }
        else
        {
            if(PlayerId == ProfileInfo.Player.id)
            {
                newPosition = CalculatePositionBasedOnSeat5(2);

            }
            else
            {
                newPosition = CalculatePositionBasedOnSeat5(frontendSeat);

            }

        }
        betContainer.localPosition = newPosition;
    }
    private void AdjustSendGifPosition()
    {
       
        Vector3 newDestinationPosition = frontendSeat >= 5 ? newDestinationPosition = new(81f, 145f, 0f) : newDestinationPosition = new(-81f, 145f, 0f);
        Vector3 newButtonPosition = frontendSeat < 5 ? newButtonPosition = new(81f, 145f, 0f) : newButtonPosition = new(-81f, 145f, 0f);

        GifDestination.transform.localPosition = newDestinationPosition;
        GiftButton.transform.localPosition = newButtonPosition;

    }
    private Vector3 CalculatePositionBasedOnSeat9(int seatNumber)
    {
        Vector3[] seatPositions = new Vector3[] {
        new Vector3(6, -94, 0),
        new Vector3(-140, -50, 0),        
        new Vector3(-190, 126, 0),
        new Vector3(-40, 200, 0),
        new Vector3(0, 225, 0),
        new Vector3(35, 200, 0),
        new Vector3(170, 115, 0),
        new Vector3(150, -47, 0),
        new Vector3(33, -90, 0),


    };

        if (seatNumber >= 0 && seatNumber < seatPositions.Length)
        {

            return seatPositions[seatNumber];
        }
        else
        {
            Debug.LogWarning($"Invalid seat number: {seatNumber}. Defaulting to position 0.");
            return seatPositions[0];
        }

    }
    private Vector3 CalculatePositionBasedOnSeat5(int seatNumber)
    {
        Vector3[] seatPositions = new Vector3[] {
        new Vector3(6, -94, 0),
       // new Vector3(-140, -50, 0),
        new Vector3(-190, 126, 0),
        //new Vector3(-40, 200, 0),
        new Vector3(0, 225, 0),
        //new Vector3(35, 200, 0),
        new Vector3(170, 115, 0),
        //new Vector3(150, -47, 0),
        new Vector3(33, -90, 0),


    };

        if (seatNumber >= 0 && seatNumber < seatPositions.Length)
        {

            return seatPositions[seatNumber];
        }
        else
        {
            Debug.LogWarning($"Invalid seat number: {seatNumber}. Defaulting to position 0.");
            return seatPositions[0];
        }

    }
}
