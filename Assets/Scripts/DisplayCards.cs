using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCards : MonoBehaviour
{
    public Image cardImage; 
    public List<Sprite> cardSprites;

   
    private string[] suits = { "Heart", "Spade", "Club", "Diamond" };
    private string[] ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    public void SetCard(string suit, string rank)
    {
        int suitIndex = System.Array.IndexOf(suits, suit);
        int rankIndex = System.Array.IndexOf(ranks, rank);

        

        int spriteIndex = suitIndex * ranks.Length + rankIndex;
       
            cardImage.sprite = cardSprites[spriteIndex];
        
        
    }
}