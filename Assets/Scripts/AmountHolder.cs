using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmountHolder : MonoBehaviour
{
    private TextMeshProUGUI raiseAmount;
    [SerializeField] private Slider raiseSlider;
    // Start is called before the first frame update
    void Start()
    {
        raiseAmount = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raiseSlider.value >= raiseSlider.maxValue - 1)
        {
            GameVariables.raiseAmount = long.Parse(GameVariables.maximumRaise);
        }
        else
        {
            Debug.Log(GameVariables.maximumRaise);
            Debug.Log(GameVariables.bigBlindAmount);

            GameVariables.raiseAmount = (long)raiseSlider.value * long.Parse(GameVariables.bigBlindAmount) + long.Parse(GameVariables.minimumRaise);


        }
        raiseAmount.text = MoneyConverter.ConvertMoney((long)GameVariables.raiseAmount);
    }
}
