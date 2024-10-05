using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpinSCript : MonoBehaviour
{
    public GameObject row; 
    public GameObject luckySpin;
    public TextMeshProUGUI[] prizeTexts; 
    public float spinSpeed = 100f; 
    public float spinTime = 4f; 
    public float resetPosition = 272f; 
    public Animator Handle;
    public GameObject Winanimation, BlackOverlay;
    private int id; 
    private long[] values; 
    public TextMeshProUGUI amountWon;
    public AudioSource audiosource;
    public GameObject mainmenu, Slotmenu;
    public static Queue<Spin> spinsQueue = new Queue<Spin>();
    public APIcalls aPIcallsRef;
    public TextMeshProUGUI spinsCount;
    public static event Action<string> updateSppinCount;
    public class Spin
    {
        public int Id { get; set; }
        public long[] Values { get; set; }
    }

    private void Start()
    {

        PrepareNextSpin(); // Prepare the first spin without actually spinning
        aPIcallsRef.onLuckySpinWon += luckySpinWon;
        spinsCount.text ="Spins: "+ spinsQueue.Count().ToString();
        updateSppinCount?.Invoke("Spins: " + spinsQueue.Count().ToString());


    }
    private void OnDestroy()
    {
        aPIcallsRef.onLuckySpinWon -= luckySpinWon;
    }
    private void luckySpinWon(long Winvalue)
    {
        StartSpin(Winvalue);
        if (spinsQueue.Count > 0)
        {
            spinsQueue.Dequeue();
        }
        if (spinsQueue.Count > 0)
        {
            luckySpin.GetComponent<Button>().interactable = true;
        }
        else
        {
            luckySpin.GetComponent<Button>().interactable = false;
        }
        spinsCount.text = "Spins: " + spinsQueue.Count().ToString();
        updateSppinCount?.Invoke("Spins: " + spinsQueue.Count().ToString());
    }
    public void PrepareNextSpin()
    {
        if (spinsQueue.Count > 0)
        {
            Spin nextSpin = spinsQueue.Peek(); // Peek instead of Dequeue
            SetValues(nextSpin.Id, nextSpin.Values); // Set the values for this spin
        }
        else
        {
            spinsCount.text = "Spins: 0";
            updateSppinCount?.Invoke("Spins: 0");

        }
    }
    public void SetValues(int receivedId, long[] receivedValues)
    {
        id = receivedId;
        values = receivedValues;

        // Update the prize texts
        for (int i = 0; i < prizeTexts.Length && i < values.Length; i++)
        {
            prizeTexts[i].text = "$" + MoneyConverter.ConvertMoney(values[i]);
        }
    }
    public void StartSpin(long targetValueIndex) // Call this method to start a spin
    {
       // Debug.Log(targetValueIndex);
        prizeTexts[5].text = "$" + MoneyConverter.ConvertMoney(targetValueIndex);

        // if (!isSpinning && betManager.spinsQueue.Count > 0) 
        StartCoroutine(Spinning());
    }
    public IEnumerator Spinning()
    {
        if(playerSettings.settings.sound)
        audiosource.Play();
        float time = 0f; // We'll increment time as we spin, rather than decrementing it

        float targetPosition = 260f; // Your hardcoded position

        while (time < spinTime) // Continue spinning for the full spinTime duration
        {
            // Calculate how far to move during this frame
            float movement = spinSpeed * Time.deltaTime;

            // Translate the row downward
            row.transform.Translate(0, -movement, 0);

            // If the row's position is below the reset position, move it back to the top
            if (row.transform.localPosition.y <= -resetPosition)
            {
                row.transform.localPosition += new Vector3(0, resetPosition, 0);
            }

            // Increment the time spent spinning
            time += Time.deltaTime;

            yield return null;
        }

        // Snap to the target position after spinning is complete
        if (playerSettings.settings.sound)
        audiosource.Stop();
        row.transform.localPosition = new Vector3(row.transform.localPosition.x, targetPosition, row.transform.localPosition.z); // Snap to target position
        StartCoroutine(Preaperandshowanim());
        if (spinsQueue.Count > 0)
        {
            luckySpin.GetComponent<Button>().enabled = true;
        }
        else
        {
            luckySpin.GetComponent<Button>().enabled = false;
        }
        yield return new WaitForSeconds(2);
        if (spinsQueue.Count > 0)
        {
            luckySpin.GetComponent<Button>().enabled = true;
        }
        else
        {
            Slotmenu.SetActive(true);
            luckySpin.GetComponent<Button>().enabled = false;
        }
    }
    public void SlotTrigger()
    {
        if (spinsQueue.Count > 0)
        {
            Handle.SetTrigger("Handle");
        }
        SendIdToSecondAPI();
    }

    // Method to send the ID to the second API
    public void SendIdToSecondAPI()
    {
        Spin nextSpin = spinsQueue.Peek(); // Get the first spin from the queue without removing it
        int spinId = nextSpin.Id;
        aPIcallsRef.Spin_SLotMachineWrapper(spinId.ToString());
    }
    
    public IEnumerator Preaperandshowanim()
    {
        yield return new WaitForSeconds(0.5f);
        BlackOverlay.SetActive(true);
        Winanimation.SetActive(true);
        amountWon.text = "COLLECT   " + prizeTexts[5].text;
        yield return new WaitForSeconds(0.5f);

        PrepareNextSpin();
    }
}
