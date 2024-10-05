using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyConverter : MonoBehaviour
{
    private const long Thousand = 1000;
    private const long Million = Thousand * Thousand; // 1,000,000
    private const long Billion = Million * Thousand; // 1,000,000,000
    private const long Trillion = Billion * Thousand; // 1,000,000,000,000
    private const long Quadrillion = Trillion * Thousand; // 1,000,000,000,000,000

    public static string ConvertMoney(long number)
    {
        if (number < 0) return "Invalid"; // Error handling for negative numbers

        if (number < Thousand) return number.ToString(); // Early return for small numbers
        if (number < Million) return FormatWithPrecision(number, Thousand, "K");
        if (number < Billion) return FormatWithPrecision(number, Million, "M");
        if (number < Trillion) return FormatWithPrecision(number, Billion, "B");
        if (number < Quadrillion) return FormatWithPrecision(number, Trillion, "T");

        return FormatWithPrecision(number, Quadrillion, "Q");
    }

    private static string FormatWithPrecision(long number, long divisor, string suffix)
    {
        double result = (double)number / divisor;
        return result.ToString("F1") + suffix;
    }
}
