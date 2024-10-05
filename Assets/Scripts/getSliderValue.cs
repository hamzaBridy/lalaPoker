using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getSliderValue : MonoBehaviour
{
    public static float TableSliderValue = 0;
    public static float maxSliderValue = 10;
    private Slider raiseSlider;
    private float minValue;
    private float maxValue;

    void Start()
    {
        raiseSlider = GetComponent<Slider>();
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet += updateSilder;
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove += updateSilder;
        updateSilder();
    }
    void OnDestroy()
    {
        webSocetConnect.UIWebSocket.MainTable.OnPlaceBet -= updateSilder;
        webSocetConnect.UIWebSocket.MainTable.OnPlayerBetMove -= updateSilder;
    }
    private void OnEnable()
    {
        if (raiseSlider != null)
        {
            raiseSlider.value = 0;
            updateSilder();
        }
    }
    private void OnDisable()
    {
        if (raiseSlider != null)
        {
            raiseSlider.value = 0;
        }
    }
    void updateSilder()
    {

        minValue = long.Parse(GameVariables.bigBlindAmount);
        maxValue = long.Parse(GameVariables.maximumRaise) - long.Parse(GameVariables.minimumRaise);
        raiseSlider.minValue = 0;
        raiseSlider.maxValue = maxValue / minValue + 1;
        Debug.Log(minValue);

    }
    // Update is called once per frame
    void Update()
    {
        TableSliderValue = raiseSlider.value;
        maxSliderValue = raiseSlider.maxValue;
    }

    public void OnSliderValueChanged(float value)
    {
        float actualValue = Mathf.Round(value / long.Parse(GameVariables.bigBlindAmount)) * long.Parse(GameVariables.bigBlindAmount);
    }
}
