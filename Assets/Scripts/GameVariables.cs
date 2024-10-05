using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables :MonoBehaviour
{
    public static bool isLoggedIn=false;
    public static string gameType = "1", gameSize = "9";
    public static string isAutoRebuy = "0", isBuyInMax = "0";
    public static int seatOffset =0 , seatNumWatned;
    public static long callAmount, raiseAmount;
    public static string bigBlindAmount;
    public static string minimumRaise, maximumRaise;
    public static bool isMyTurn = false;
    public static int maxpotProi=0;
    public static bool firstIni = true;
    public static bool dataIsavilable=false, isOnTableScene = false,isSpectating = false, canRaise = false, isPlaying = false;
    
}
