using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class paymentButtonSpec : MonoBehaviour
{
    public GameObject buttonclick;
    public TextMeshProUGUI chips;
    public TextMeshProUGUI price;
    public Image cashImage;
    public string chipsAmount="test";
    public string priceAmount="test";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chips.text=chipsAmount;
        price.text=priceAmount;
    }
}
