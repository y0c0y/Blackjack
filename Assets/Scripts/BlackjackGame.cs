using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame
{

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

    public void Stay()
    {
        isPlayerTurn = false;
    }

    public Enums.GameResult CheckWinner()
    {
        int playerScore = player.CalculateScore();
        int dealerScore = dealer.CalculateScore();


        if (playerScore == dealerScore)
        {
            return Enums.GameResult.Push;
        }
        else if (playerScore == 21)
        {
            return Enums.GameResult.PlayerBlackjack;
        }
        else if (dealerScore == 21)
        {
            return Enums.GameResult.DealerBlackjack;
        }
        else if (playerScore > 21)
        {
            return Enums.GameResult.PlayerBust;
        }
        else if (dealerScore > 21)
        {
            return Enums.GameResult.DealerBust;
        }
        else if (playerScore > dealerScore)
        {
            return Enums.GameResult.PlayerWin;
        }
        else
        {
            return Enums.GameResult.DealerWin;
        }
    }


}
