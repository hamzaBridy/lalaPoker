using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class disablePot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI potText;
    [SerializeField] Image potImage;

    private void Update()
    {
        if (potText.text == "0"|| potText.text == "")
        {
            potImage.gameObject.SetActive(false);
        }
        else {
            potImage.gameObject.SetActive(true);

        }
    }
}
