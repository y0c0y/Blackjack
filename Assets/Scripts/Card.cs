using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card 
{
    private Enums.Suit suit;
    private Enums.Value value;
    private int sourceIdx;
    
    public Card(Enums.Suit suit, Enums.Value value, int idx)
    {
        this.suit = suit;
        this.value = value;
        //Assets / Sprites / Cards.png
        sourceIdx = idx;
    }

    public int GetIntValue()
    {
        switch(value)
        {
            case Enums.Value.Ace:
                return 11;
            case Enums.Value.Two:
                return 2;
            case Enums.Value.Three:
                return 3;
            case Enums.Value.Four:
                return 4;
            case Enums.Value.Five:
                return 5;
            case Enums.Value.Six:
                return 6;
            case Enums.Value.Seven:
                return 7;
            case Enums.Value.Eight:
                return 8;
            case Enums.Value.Nine:
                return 9;
            case Enums.Value.Ten:
                return 10;
            case Enums.Value.Jack:
                return 10;
            case Enums.Value.Queen:
                return 10;
            case Enums.Value.King:
                return 10;
            default:
                return 0;
        }
    }
    public Enums.Value GetValue() => value;
    public string GetSuit() =>suit.ToString();

    public int GetSourceIdx() => sourceIdx;
}
