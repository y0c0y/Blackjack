using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    public Enums.GameResult _result = Enums.GameResult.None;

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
    }

    public int GetPlayerScore() => player.CalculateScore();
    public int GetDealerScore() => dealer.CalculateScore();
    public bool GetIsPlayer() => isPlayer;

    public Player GetOnTurnPlayer() => isPlayer ? player : dealer;
    public Player GetPlayer() => player;
    public Player GetDealer() => dealer;
    public Deck GetDeck() => deck;

    public void StartGame() // StartGame() is called in InitializeGame()
    {
        deck.InitializeDeck();
        deck.DeckReset();
        InGame();
    }

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

        DealInitialCards();
    }

    void DealInitialCards()
    {

       for(int j = 0; j < 2; j++)
        {
            Player anyone = isPlayer ? GetPlayer() : GetDealer();
            for (int i = 0; i < 2; i++)
            {
                Card tmp = deck.DealCard();
                anyone.ReceiveCard(tmp);
                OnCardDealt?.Invoke(isPlayer, i);
            }

            ChangeTurn();
        }

        CheckBlackjack();
    }


    public Card Hit()
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

        if (!isPlayer)
        {
            CheckWinner();
        }
        else
        {
            CheckPlayerResult();
        }

        return card;
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
        if (playerScore == dealerScore)
        {
            _result =  Enums.GameResult.Push;
        }
        else
        {
            if (dealerScore == 21) _result = Enums.GameResult.DealerBlackjack;
            else if (dealerScore > 21) _result = Enums.GameResult.DealerBust;
            else if (playerScore > dealerScore) _result = Enums.GameResult.PlayerWin;
            else _result = Enums.GameResult.DealerWin;
            
        }
    }

    public void CheckPlayerResult()
    {
        _result = GetPlayerScore() > 21?  Enums.GameResult.PlayerBust : Enums.GameResult.None;
           
    }
}
