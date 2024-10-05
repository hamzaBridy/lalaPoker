using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetPromoText : MonoBehaviour
{
    [SerializeField] Button getPromoButton;
    [SerializeField] APIcalls aPIcallsRef;
    [SerializeField] TMP_InputField promoCode;
    [SerializeField] GameObject ressults;
    [SerializeField] TextMeshProUGUI textResults;

    private void Start()
    {
        getPromoButton.onClick.AddListener(() => { aPIcallsRef.PromoCodeWrap(promoCode.text); });
        aPIcallsRef.OnpromoCodeActivated += PromoResults;

    }

    private void PromoResults(string results)
    {
        textResults.text = results;
        ressults.SetActive(true);


    }
}
