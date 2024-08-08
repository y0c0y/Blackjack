using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    private Enums.GameState _state;

    private Deck deck;
    private List<Card> player;
    private List<Card> dealer;    

    void Blackjack()
    {
        deck = new Deck();
        player = new List<Card>();
        dealer = new List<Card>();
        StartGame();
    }

    void StartGame() // StartGame() is called in InitializeGame()
    {
        deck.DeckReset();
        player.Clear();
        dealer.Clear();

        Hit(player);
        Hit(player);

        Hit(dealer);
        Hit(dealer);
    }

    public void Hit(List<Card> someone)
    {
        someone.Add(deck.DealCard());
    }



    public int GetScore(List<Card> cards)
    {
        int score = 0;
        int aceCount = 0;

        foreach (Card card in cards)
        {
            score += card.GetValue();
            if (card.value == Enums.Value.Ace)
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
