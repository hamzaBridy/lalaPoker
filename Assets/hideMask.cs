using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideMask : MonoBehaviour
{
    public Image circularMask;
    public float percentage = 0f; // 0 to 100

    void Update()
    {
        // Ensure the percentage is clamped between 0 and 100
        percentage = Mathf.Clamp(percentage, 0f, 100f);

        // Calculate the fill amount based on the percentage
        float fillAmount = percentage / 100f;

        // Set the fill amount of the circular mask
        circularMask.fillAmount = fillAmount;
    }
}
