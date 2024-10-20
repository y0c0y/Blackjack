using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums 
{
    public enum Suit
    {
        Spades = 0,
        Hearts = 45,
        Diamonds = 15,
        Clubs = 30
    }

    public enum Value
    {
        Ace,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack,
        Queen,
        King
    }

    public enum PlayerAction
    {
        Hit,
        Stay,
        Split,
        DoubleDown,
        Surrender,
        Insurance
    }

    public enum GameResult
    {
        PlayerBlackjack,
        PlayerWin,
        DealerBlackjack,
        DealerWin,
        PlayerBust,
        DealerBust,
        Push,
        None
    }

    

    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }
}
