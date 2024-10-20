using System;
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

    public void InGame()
    {
        ResetGame();
        DealInitialCards();
        CheckBlackjack();
    }

    private void ResetGame()
    {
        isPlayer = true;
        player.ResetHand();
        dealer.ResetHand();
    }

    private void DealInitialCards()
    {
        for (int i = 0; i < 4; i++)
        {
            Hit();
            ChangeTurn();
        }
    }


    public void Hit()
    {
        Player currentPlayer = isPlayer ? player : dealer;
        Card card = deck.DealCard() ?? ResetAndDealCard();

        currentPlayer.ReceiveCard(card);
        OnCardDealt?.Invoke(isPlayer, currentPlayer.GetHand().Count - 1);
    }


    private Card ResetAndDealCard()
    {
        deck.DeckReset();
        return deck.DealCard();
    }


    public void CheckBlackjack()
    {
        int playerScore = player.CalculateScore();
        int dealerScore = dealer.CalculateScore();

        if (playerScore == 21 && dealerScore == 21)
        {
            _result = Enums.GameResult.Push;
        }
        else if (playerScore == 21)
        {
            _result = Enums.GameResult.PlayerBlackjack;
            Debug.Log("Player Blackjack!");
        }
        else
        {
            _result = Enums.GameResult.None;
        }
    }

    public void CheckWinner()
    {
        int playerScore = GetPlayerScore();
        int dealerScore = GetDealerScore();

        if (playerScore > 21)
        {
            _result = Enums.GameResult.PlayerBust;
        }
        else if (dealerScore > 21)
        {
            _result = Enums.GameResult.DealerBust;
        }
        else if (playerScore == dealerScore)
        {
            _result = Enums.GameResult.Push;
        }
        else if (playerScore > dealerScore)
        {
            _result = Enums.GameResult.PlayerWin;
        }
        else
        {
            _result = Enums.GameResult.DealerWin;
        }
    }
}
