using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum Suit
    {
        Spades,
        Hearts,
        Diamonds,
        Clubs
    }

    public enum Value
    {
        Ace = 11, // 1 or 11
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10
    }

    public Suit suit;
    public Value value;

    public int GetValue()
    {
        return (int)value;
    }
}
