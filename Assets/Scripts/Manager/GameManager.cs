using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BlackjackGame game;
    public UIManager uiManager;
    public InputManager inputManager;
    public CardManager cardManager;
    public BetManager betManager;

    public bool openDealerCard = false;

    private void Start()
    {
        game = new BlackjackGame();
        game.OnCardDealt += SetCardImg;
        uiManager.ShowMenu();
    }

    public void Delay()
    {
        Debug.Log("Delay");
    }

    // Update the player's and dealer's scores on the UI
    public void UpdateScore()
    {
        int playerScore = game.GetPlayerScore();
        int dealerScore = openDealerCard ? game.GetDealerScore() : game.GetDealer().GetHand()[0].GetIntValue();

        uiManager.UpdateScore(playerScore, dealerScore);
    }

    // Set the appropriate card image for the player or dealer
    public void SetCardImg(bool isPlayer, int cardIndex)
    {
        Player player = isPlayer ? game.GetPlayer() : game.GetDealer();
        int spriteIndex = player.GetHand()[cardIndex].GetSourceIdx();

        if (!openDealerCard && cardIndex == 1 && !isPlayer)
        {
            cardManager.SetCardImg(isPlayer, cardIndex, 44, openDealerCard);  // Placeholder image for a hidden card
        }
        else
        {
            cardManager.SetCardImg(isPlayer, cardIndex, spriteIndex, openDealerCard);
        }

    }

    // Check the result of the game and update the UI
    public void CheckResult()
    {
        Enums.GameResult result = game.GetResult();
        uiManager.UpdateResult(result);

        if (result != Enums.GameResult.None)
        {
            uiManager.ShowResult();
        }
    }

    // Handle player's Hit action
    public void OnHit()
    {
        game.Hit();
        UpdateScore();

        if (game.GetPlayerScore() > 21)
        {
            OnStay();  // Automatically stay if player busts
        }
    }

    // Handle player's Stay action
    public void OnStay()
    {
        game.ChangeTurn();
        openDealerCard = true;
        OnDealerTurn();
        CheckResult();
    }

    // Placeholder methods for future features
    public void OnDoubleDown() => Debug.Log("Double Down");
    public void OnSurrender() => Debug.Log("Surrender");
    public void OnInsurance() => Debug.Log("Insurance");
    public void OnSplit() => Debug.Log("Split");

    // Clear both player's and dealer's hands
    public void HandClear()
    {
        cardManager.ClearHands(cardManager.playerHand);
        cardManager.ClearHands(cardManager.dealerHand);
    }

    // Return to the menu screen
    public void OnMenu()
    {
        HandClear();
        uiManager.ShowMenu();
    }

    // Initialize betting and show betting UI
    public void OnBet()
    {
        HandClear();
        betManager.InitBetting();
        uiManager.ShowBetting();
    }

    // Exit the game
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Handle dealer's turn logic
    public void OnDealerTurn()
    {
        // Reveal dealer's second card
        Destroy(cardManager.dealerHand.GetChild(1).gameObject);
        SetCardImg(false, 1);

        // Dealer keeps hitting until reaching a score of 17 or higher
        int dealerScore = game.GetDealerScore();
        while (dealerScore < 17)
        {
            game.Hit();
            dealerScore = game.GetDealerScore();
        }

        game.CheckWinner();
        UpdateScore();
    }

    // Start the game and initialize necessary variables
    public void StartGame()
    {
        uiManager.ShowInGame();
        openDealerCard = false;
        game.InGame();
        UpdateScore();
        CheckResult();
    }
}
