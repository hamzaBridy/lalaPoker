using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class spincont : MonoBehaviour
{
    private TextMeshProUGUI spinCount;

    private void Start()
    {
        spinCount= GetComponent<TextMeshProUGUI>();
        LuckySpinSCript.updateSppinCount += updateCount;
        APIcalls.OnGet_SlotMachinePrizes += updatecount;
    }
    private void OnDestroy()
    {
        LuckySpinSCript.updateSppinCount -= updateCount;
        APIcalls.OnGet_SlotMachinePrizes -= updatecount;

    }
    void updatecount()
    {
        updateCount("Spins: " + LuckySpinSCript.spinsQueue.Count().ToString());

    }
    void updateCount(string  count)
    {
        spinCount.text = count;
    }
}
