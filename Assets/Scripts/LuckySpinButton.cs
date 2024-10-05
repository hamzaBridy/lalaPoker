using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpinButton : MonoBehaviour
{
    // Start is called before the first frame update
    public APIcalls aPIcallsRef;
    void Start()
    {
        
        aPIcallsRef.Get_SlotMachinePrizesWrapper();
        APIcalls.OnGet_SlotMachinePrizes += gotPrizes;
    }
    private void OnDestroy()
    {
        APIcalls.OnGet_SlotMachinePrizes -= gotPrizes;

    }
    private void gotPrizes()
    {
        if (LuckySpinSCript.spinsQueue.Count > 0)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }

    }   
}
