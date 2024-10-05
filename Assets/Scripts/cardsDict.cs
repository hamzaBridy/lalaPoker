using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class cardsDict : MonoBehaviour
{
    private string[] suits = { "Hearts", "Spades", "Clubs", "Diamonds" };
    private string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13","14" };
    public List<Sprite> images=new List<Sprite>();
    public Sprite getCardSprite(string suit, string rank)
    {
        int suitIndex = System.Array.IndexOf(suits, suit);
        int rankIndex = System.Array.IndexOf(ranks, rank);

        

        int spriteIndex = suitIndex * ranks.Length + rankIndex;
            return images[spriteIndex];
        
        
    }
}
