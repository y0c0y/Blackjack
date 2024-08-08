using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Enums.Suit suit;
    public Enums.Value value;

    public int GetValue()
    {
        return (int)value;
    }

    public string GetSuit()
    {
        return suit.ToString();
    }
}
