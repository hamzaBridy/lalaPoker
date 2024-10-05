using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
public class UpdateMoney : MonoBehaviour
{
    public static void updateText(TextMeshProUGUI text1,string text2){
        UpdateMoney instance = text1.gameObject.AddComponent(typeof(UpdateMoney)) as UpdateMoney;
        instance.StartCoroutine(instance.UpdateSimul(text1, text2,instance));
    }
    IEnumerator UpdateSimul(TextMeshProUGUI text1,string text2,UpdateMoney insta){
        float elapsedTime=0;
        string trueStringValue=text1.text.Replace(",", "");
         trueStringValue = trueStringValue.Replace("$", "");

        long t1 =long.Parse(trueStringValue);
        long t2=long.Parse(text2);
      //  Debug.Log(t1);
      //  Debug.Log(t2);
        while (elapsedTime < 1.5f)
    {
        // Interpolate between t1 and t2 based on the elapsed time
        long interpolatedValue = (long)Mathf.Round(Mathf.Lerp(t1, t2, elapsedTime / 1.5f));
        //Debug.Log(interpolatedValue);

        // Update t1 with the interpolated value
        text1.text="$"+interpolatedValue.ToString("N0");

        // Increment the elapsed time
        elapsedTime += Time.deltaTime;

        yield return null;
    }
    text1.text = "$" + long.Parse(text2).ToString("N0");
    getChips.isGrad=false;
    Destroy(insta);
    }
    
}
