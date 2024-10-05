using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raiseMeterButtons : MonoBehaviour
{
    [SerializeField] Slider raiseSlider;
    [SerializeField] Button allIn, fullPot, halfPot, plus, minus;

    public void Allin()
    {
        raiseSlider.value = Mathf.Min(raiseSlider.maxValue, raiseSlider.value);
    }

    public void FullPot()
    {
        long fullPotValue = long.Parse(GameTable.TotalChips);
        raiseSlider.value = Mathf.Clamp(fullPotValue, raiseSlider.minValue, raiseSlider.maxValue);
    }

    public void HalfPot()
    {
        long halfPotValue = long.Parse(GameTable.TotalChips) / 2;
        raiseSlider.value = Mathf.Clamp(halfPotValue, raiseSlider.minValue, raiseSlider.maxValue);
    }

    public void Minus()
    {
        long newAmount = (long)(raiseSlider.value - long.Parse(GameVariables.bigBlindAmount));
        raiseSlider.value = Mathf.Max(newAmount, raiseSlider.minValue);
    }

    public void Plus()
    {
        long newAmount = (long)(raiseSlider.value + long.Parse(GameVariables.bigBlindAmount));
        raiseSlider.value = Mathf.Min(newAmount, raiseSlider.maxValue);
    }

}
