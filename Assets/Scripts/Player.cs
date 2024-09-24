using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player 
{
    private List<Card> hand;
    private Transform handTransforms;

    public Player()
    {
        hand = new List<Card>();
    }
    public List<Card> GetHand() => hand;

    public void ResetHand()
    {
        hand.Clear();
    }

    public void ReceiveCard(Card card)
    {
        hand.Add(card);
    }

    public int CalculateScore()
    {
        int score = 0;
        int aceCount = 0;

        foreach (Card card in hand)
        {
            int cardValue = card.GetIntValue();
            score += cardValue;
            if (cardValue == 11)
            {
                aceCount++;
            }
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        return score;
    }

}   

