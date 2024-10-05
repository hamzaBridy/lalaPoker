using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class betMan : MonoBehaviour
{
    public GameObject funHolder;
    
    // Start is called before the first frame update
    [SerializeField] private Button foldBtn, checkBtn, callBtn, raiseBtn, raiseConfirmBtn;
    [SerializeField] private TextMeshProUGUI callFor;
    [SerializeField] private GameObject raiseSlider, waitText;
    public TMP_ColorGradient green, gold,red;
    private bool iFolded = false;
    [System.Serializable]
    public class ToggleButtonData
    {
        public Button button;
        public Sprite normalSprite;
        public Sprite pressedSprite;
        public bool isPressed = false;
        public TextMeshProUGUI buttenText;
        public TMP_ColorGradient pickedTextColr;
    }
    public List<ToggleButtonData> toggleButtons;

    void Start(){
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove += manageTurns;
        webSocetConnect.UIWebSocket.MainTable.OnWin += removeButtons;
        webSocetConnect.UIWebSocket.MainTable.OnWin +=  resetFold;
        webSocetConnect.UIWebSocket.MainTable.onMyTurn += OnPlayerTurn;
        webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft += removeButtons;
        webSocetConnect.UIWebSocket.MainTable.Ifolded += myPlayerFold;


        foreach (var toggleButton in toggleButtons)
        {
            toggleButton.button.onClick.AddListener(() => ToggleButton(toggleButton));
        }
        removeButtons();
    }
    private void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.Ifolded -= myPlayerFold;
        webSocetConnect.UIWebSocket.MainTable.OnWin -= resetFold;
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove -= manageTurns;
        webSocetConnect.UIWebSocket.MainTable.OnWin -= removeButtons;
        webSocetConnect.UIWebSocket.MainTable.onMyTurn -= OnPlayerTurn;
        webSocetConnect.UIWebSocket.MainTable.OnMainUserLeft -= removeButtons;

    }
    private void Update()
    {
    }
    void ToggleButton(ToggleButtonData clickedButtonData)
    {
        clickedButtonData.isPressed = !clickedButtonData.isPressed;
        UpdateButtonVisual(clickedButtonData);

        // Reset other buttons
        foreach (var tb in toggleButtons)
        {
            if (tb != clickedButtonData && tb.isPressed)
            {
                tb.isPressed = false;
                UpdateButtonVisual(tb);
            }
        }
    }
    void UpdateButtonVisual(ToggleButtonData buttonData)
    {
        buttonData.button.image.sprite = buttonData.isPressed ? buttonData.pressedSprite : buttonData.normalSprite;
        buttonData.buttenText.colorGradientPreset = buttonData.isPressed ? buttonData.pickedTextColr : gold;
             }
    public void OnPlayerTurn()
    {
#if UNITY_IOS || UNITY_ANDROID

        Handheld.Vibrate();
#endif
        foreach (var toggleButton in toggleButtons)
            {
                if (toggleButton.isPressed)
                {
                    if (toggleButton.button.name == "Check/Fold")
                    {
                        if (!(GameVariables.callAmount > 0))
                        {
                            Check();
                        }
                        else
                        {
                            Fold();
                        }
                    }
                    else if (toggleButton.button.name == "foldAny")
                    {
                        Fold();
                    }
                    else if (toggleButton.button.name == "callAny")
                    {
                        if (!(GameVariables.callAmount > 0))
                        {
                            Check();
                        }
                        else
                        {
                            Call();
                        }
                    }
                }
            }
        
        foreach (var tb in toggleButtons)
        {
            tb.isPressed = false;
            UpdateButtonVisual(tb);
        }
    }
    
    public void Fold(){
        Debug.Log("Fold");
        string gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        funHolder.GetComponent<webSocetConnect>().placeBet("FOLD","0",gameId);
    }
    public void Check(){
        Debug.Log("Check");
        string gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        funHolder.GetComponent<webSocetConnect>().placeBet("CHECK","0",gameId);
    }
    public void Call(){
        Debug.Log("Call");
        string gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        funHolder.GetComponent<webSocetConnect>().placeBet("CALL","0",gameId);
    }
    public void Raise(){
        Debug.Log("Raise");
        string gameId=webSocetConnect.UIWebSocket.MainTable.gameId;
        funHolder.GetComponent<webSocetConnect>().placeBet("RAISE", GameVariables.raiseAmount.ToString(), gameId);
    }
    void myPlayerFold()
    {iFolded = true;
    }
    void resetFold()
    {
        iFolded = false;
    }
    private void removeButtons()
    {
        raiseBtn.gameObject.SetActive(false);
        callBtn.gameObject.SetActive(false);
        checkBtn.gameObject.SetActive(false);
        foldBtn.gameObject.SetActive(false);
        raiseConfirmBtn.gameObject.SetActive(false);
        raiseSlider.SetActive(false);
        waitText.SetActive(true);
        foreach (var tb in toggleButtons)
        {
            
            tb.button.gameObject.SetActive(false);
        }
    }
    private void manageTurns()
    {
        if (GameTable.isOnTable && !iFolded)
        {
            if (callFor != null)
            {
                callFor.text = MoneyConverter.ConvertMoney(GameVariables.callAmount);
            }
            if (GameVariables.callAmount == 0 && GameVariables.isMyTurn)
            {
                if (GameVariables.canRaise)
                {
                    raiseBtn.gameObject.SetActive(true);
                    raiseConfirmBtn.gameObject.SetActive(false);
                    raiseSlider.SetActive(false);
                }
                if (!GameVariables.canRaise)
                {
                    raiseBtn.gameObject.SetActive(false);

                }
                callBtn.gameObject.SetActive(false);
                checkBtn.gameObject.SetActive(true);
                foldBtn.gameObject.SetActive(true);
                waitText.SetActive(false);
                foreach (var tb in toggleButtons)
                {

                    tb.button.gameObject.SetActive(false);
                }

            }
            else if (GameVariables.callAmount > 0 && GameVariables.isMyTurn)
            {
                if (GameVariables.canRaise)
                {
                    raiseBtn.gameObject.SetActive(true);
                    raiseConfirmBtn.gameObject.SetActive(false);
                    raiseSlider.SetActive(false);
                }
                if (!GameVariables.canRaise)
                {
                    raiseBtn.gameObject.SetActive(false);

                }
                callBtn.gameObject.SetActive(true);
                checkBtn.gameObject.SetActive(false);
                foldBtn.gameObject.SetActive(true);
                waitText.SetActive(false);
                foreach (var tb in toggleButtons)
                {

                    tb.button.gameObject.SetActive(false);
                }

            }
            else if (!GameVariables.isMyTurn)
            {
                
                raiseBtn.gameObject.SetActive(false);
                callBtn.gameObject.SetActive(false);
                checkBtn.gameObject.SetActive(false);
                foldBtn.gameObject.SetActive(false);
                raiseConfirmBtn.gameObject.SetActive(false);
                raiseSlider.SetActive(false);
                waitText.SetActive(false);
                foreach (var tb in toggleButtons)
                {

                    tb.button.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            removeButtons();
        }
    }
    private void CanRaise(bool canRaise)
    {
        if (GameVariables.canRaise)
        {
            raiseBtn.gameObject.SetActive(true);
            raiseConfirmBtn.gameObject.SetActive(false);
            raiseSlider.SetActive(false);
        }
        else
        {
            raiseBtn.gameObject.SetActive(false);
            raiseConfirmBtn.gameObject.SetActive(false);
            raiseSlider.SetActive(false);
        }
    }
}
