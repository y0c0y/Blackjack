using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private Enums.Suit suit;
    private Enums.Value value;
    private int sourceIdx;

    public Card(Enums.Suit suit, Enums.Value value, int idx)
    {
        this.suit = suit;
        this.value = value;
        this.sourceIdx = idx;
    }

    // Get the numerical value of the card (e.g., Ace = 11, Face cards = 10)
    public int GetIntValue()
    {
        if (value >= Enums.Value.Two && value <= Enums.Value.Ten)
        {
            return (int)value; // Values Two to Ten map directly to their integer values
        }
        else if (value == Enums.Value.Ace)
        {
            return 11; // Ace is worth 11 points
        }
        else
        {
            return 10; // Face cards (Jack, Queen, King) are worth 10 points
        }
    }

    public Enums.Value GetValue() => value;
    public string GetSuit() => suit.ToString();
    public int GetSourceIdx() => sourceIdx;
}
