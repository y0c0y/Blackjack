using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{
    public Enums.GameResult _result = Enums.GameResult.None;

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

        isPlayerTurn = true;

        InGame();
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    public void InGame() // ResetGame() is called in InitializeGame()
    {
        player.ResetHand();
        dealer.ResetHand();

        DealInitialCards();
    }

    void DealInitialCards()
    {
        player.ReceiveCard(deck.DealCard());
        Debug.Log("Player's first card: " + player.GetHand()[0].GetSuit() + " " + player.GetHand()[0].GetValue());
        player.ReceiveCard(deck.DealCard());
        Debug.Log("Player's second card: " + player.GetHand()[1].GetSuit() + " " + player.GetHand()[1].GetValue());
        dealer.ReceiveCard(deck.DealCard());
        Debug.Log("Dealer's first card: " + dealer.GetHand()[0].GetSuit() + " " + dealer.GetHand()[0].GetValue());
        dealer.ReceiveCard(deck.DealCard());
        Debug.Log("Dealer's second card: " + dealer.GetHand()[1].GetSuit() + " " + dealer.GetHand()[1].GetValue());
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

        return card;
    }

    //public void Stay()
    //{
    //    isPlayerTurn = false;
    //}

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
    }

    public void CheckWinner(int playerScore ,int dealerScore)
    {
        if (playerScore == dealerScore)
        { 
            _result = Enums.GameResult.Push;
        }
        else
        {
            if (GetIsPlayerTurn())
            {
                if (playerScore > 21) _result = Enums.GameResult.PlayerBust;
                else _result = Enums.GameResult.None;
            }
            else
            {
                if (dealerScore == 21) _result = Enums.GameResult.DealerBlackjack;
                else if (dealerScore > 21) _result = Enums.GameResult.DealerBust;
                else if (playerScore > dealerScore) _result = Enums.GameResult.PlayerWin;
                else _result = Enums.GameResult.DealerWin;
            }
        }
       
    }
}
