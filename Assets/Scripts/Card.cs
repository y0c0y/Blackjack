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

    public int GetIntValue()
    {
        if (value >= Enums.Value.Two && value <= Enums.Value.Ten)
        {
            return (int)value;
        }
        else if (value == Enums.Value.Ace)
        {
            return 11;
        }
        else
        {
            return 10;
        }
    }

    public Enums.Value GetValue() => value;
    public string GetSuit() => suit.ToString();
    public int GetSourceIdx() => sourceIdx;
}
