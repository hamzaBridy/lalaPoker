using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sitinImageScript : MonoBehaviour
{
    public int seatNumber;
    public int seatNumber5;


    public void Changebuyin_Seat()
    {
        if (GameVariables.gameSize == "9")
        {
            GameVariables.seatNumWatned = seatNumber - GameVariables.seatOffset;
        }
        if (GameVariables.gameSize == "5")
        {
            GameVariables.seatNumWatned = seatNumber5 - GameVariables.seatOffset;
        }

    }
}
