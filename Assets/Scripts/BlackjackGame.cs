using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    public Deck deck;
    public List<Card> player;
    public List<Card> dealer;    

    void InitializeGame()
    {
        deck = new Deck();
        
        deck.DeckReset();

        player = new List<Card>();
        dealer = new List<Card>();
        StartGame();
    }

    void StartGame()
    {
        player.Add(deck.DealCard());
        player.Add(deck.DealCard());

        dealer.Add(deck.DealCard());
        dealer.Add(deck.DealCard());
    }

    public int GetScore(List<Card> cards)
    {
        int score = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            score += card.GetValue();
            if (card.value == Card.Value.Ace)
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

    public List<Card> GetPlayer() => player; 
    public List<Card> GetDealer() => dealer;
    public int GetPlayerScore() => GetScore(player);
    public int GetDealerScore() => GetScore(dealer);
}
