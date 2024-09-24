using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    public Enums.GameResult _result = Enums.GameResult.None;

    public event Action<bool, int> OnCardDealt;

    private bool isPlayerTurn;

    private Deck deck;
    private Player player;
    private Player dealer;

    public BlackjackGame()
    {
        deck = new Deck();
        player = new Player();
        dealer = new Player();
    }

    public int GetPlayerScore() => player.CalculateScore();
    public int GetDealerScore() => dealer.CalculateScore();
    public bool GetIsPlayerTurn() => isPlayerTurn;

    public Player GetOnTurnPlayer() => isPlayerTurn ? player : dealer;
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
        isPlayerTurn = !isPlayerTurn;
    }

    public void InGame() // ResetGame() is called in InitializeGame()
    {
        isPlayerTurn = true;

        player.ResetHand();
        dealer.ResetHand();

        DealInitialCards();
    }

    void DealInitialCards()
    {
        // Player���� ī�� �� �� ���
        player.ReceiveCard(deck.DealCard());
        OnCardDealt?.Invoke(true, 0);
        // ù ��° ī�� �̹��� ����

        player.ReceiveCard(deck.DealCard());
        OnCardDealt?.Invoke(true, 1);
        // �� ��° ī�� �̹��� ����

        // Dealer���� ī�� �� �� ���
        dealer.ReceiveCard(deck.DealCard());
        OnCardDealt?.Invoke(false, 0);
        // ù ��° ī�� �̹��� ����

        dealer.ReceiveCard(deck.DealCard());
        OnCardDealt?.Invoke(false, 1);
        // �� ��° ī�� �̹��� ����

        CheckBlackjack();
    }


    public Card Hit()
    {
        Player tmp = isPlayerTurn ? player : dealer;

        Card card = deck.DealCard();
        if (card == null)
        {
            deck.DeckReset();
            card = deck.DealCard();
        }
        tmp.ReceiveCard(card);
        OnCardDealt?.Invoke(isPlayerTurn, tmp.GetHand().Count - 1);

        if (!isPlayerTurn)
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
        }
        else if (dealer.CalculateScore() == 21)
        {
            _result = Enums.GameResult.DealerBlackjack;
        }
        else _result = Enums.GameResult.None;

        Debug.Log("Result: " + _result);
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
