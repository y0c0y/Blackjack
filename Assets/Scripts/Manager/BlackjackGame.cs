using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    private Enums.GameResult _result = Enums.GameResult.None;

    public event Action<bool, int> OnCardDealt;

    private bool isPlayer;

    private Deck deck;
    private Player player;
    private Player dealer;

    public BlackjackGame()
    {
        deck = new Deck();
        player = new Player();
        dealer = new Player();
        isPlayer = true;

        deck.InitializeDeck();
        deck.DeckReset();

        Debug.Assert(deck.GetCards().Count == 312, "Deck is not 312 cards");

        Debug.Log("Game Data Make");
    }

    public int GetPlayerScore() => player.CalculateScore();
    public int GetDealerScore() => dealer.CalculateScore();
    public bool GetIsPlayer() => isPlayer;

    public Player GetPlayer() => player;
    public Player GetDealer() => dealer;
    public Deck GetDeck() => deck;

    public Enums.GameResult GetResult() => _result;
    public void SetResult(Enums.GameResult result) => _result = result;


    public void ChangeTurn()
    {
        isPlayer = !isPlayer;
        Debug.Log("Turn: " + (isPlayer ? "Player" : "Dealer"));
    }

    public void InGame() // ResetGame() is called in InitializeGame()
    {
        isPlayer = true;

        player.ResetHand();
        dealer.ResetHand();

        for (int j = 0; j < 4; j++)
        {
            Hit();
            ChangeTurn();
        }

        CheckBlackjack();
    }


    public void Hit()
    {
        Player tmp = isPlayer ? player : dealer;

        Card card = deck.DealCard();
        if (card == null)
        {
            deck.DeckReset();
            card = deck.DealCard();
        }
        tmp.ReceiveCard(card);
        OnCardDealt?.Invoke(isPlayer, tmp.GetHand().Count - 1);
    }

    public void CheckBlackjack()
    {
        if (player.CalculateScore() == 21 && dealer.CalculateScore() == 21)
        {
            _result = Enums.GameResult.Push;
        }
        else if (player.CalculateScore() == 21)
        {
            _result = Enums.GameResult.PlayerBlackjack;
            Debug.Log("Blackjack");
        }
        else _result = Enums.GameResult.None;
    }


    public void CheckWinner()
    {
        int playerScore = GetPlayerScore();
        int dealerScore = GetDealerScore();
        if (playerScore > 21)
        {
            _result = Enums.GameResult.PlayerBust;
        }
        else if (playerScore == dealerScore)
        {
            _result =  Enums.GameResult.Push;
        }
        else
        {
            if (dealerScore > 21) _result = Enums.GameResult.DealerBust;
            else if (playerScore > dealerScore) _result = Enums.GameResult.PlayerWin;
            else _result = Enums.GameResult.DealerWin;
            
        }
    }
}
