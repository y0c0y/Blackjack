using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card 
{
    private Enums.Suit suit;
    private Enums.Value value;
    
    public Card(Enums.Suit suit, Enums.Value value)
    {
        this.suit = suit;
        this.value = value;
    }

    public int GetValue() => (int)value;
    public string GetSuit() =>suit.ToString();
}
