using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class raisController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider mySlider;
    public void allIn(){
        mySlider.value = mySlider.maxValue;
        Debug.Log(mySlider.maxValue);
    }
    public void fullPot(){
        mySlider.value=(long.Parse(GameTable.TotalChips)-long.Parse(GameVariables.minimumRaise))/long.Parse(GameVariables.bigBlindAmount);
        
    }
    public void halfPot(){
        long Tval=(long.Parse(GameTable.TotalChips)/2-long.Parse(GameVariables.minimumRaise))/long.Parse(GameVariables.bigBlindAmount);
        Debug.Log(Tval);
        mySlider.value=Tval;
        
    }
    public void Plus(){
        if(mySlider.value+1 <= mySlider.maxValue){
            mySlider.value+=1;
        }
        else{
            mySlider.value=mySlider.maxValue;
        }
    }
    public void Minus(){
        if(mySlider.value-1 >= mySlider.minValue){
            mySlider.value-=1;
        }
        else{
            mySlider.value=mySlider.minValue;
        }
    }
}
