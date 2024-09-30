using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private BlackjackGame game;
    public UIManager uiManager;
    public InputManager inputManager;
    public CardManager cardManager;

    void Start()
    {
        uiManager.ShowInGame();
        game = new BlackjackGame();
        game.OnCardDealt += SetCardImg;
        game.StartGame();
        
        UpdateScore();
        CheckResult();

    }

    public void UpdateScore()
    {
        uiManager.UpdateScore(game.GetPlayerScore(), game.GetDealerScore());
    }

    public void SetCardImg(bool isPlayer, int cardIndex)
    {
        Player player = isPlayer ? game.GetPlayer() : game.GetDealer();
        int spriteIndex = player.GetHand()[cardIndex].GetSourceIdx();
        cardManager.SetCardImg(isPlayer, cardIndex, spriteIndex);

        Canvas.ForceUpdateCanvases();
    }


    public void CheckResult()
    {

        Enums.GameResult result = game._result;
        uiManager.UpdateResult(result);

        if (result != Enums.GameResult.None)
        {
            uiManager.ShowResult();
        }
    }

    public void OnHit()
    {

        Card card = game.Hit();
        UpdateScore();

        if(game.GetPlayerScore() > 21)
        {
            CheckResult();
        }

    }

    public void OnStay()
    {   
        game.ChangeTurn();
        OnDealerTurn();
        CheckResult();
    }

    public void OnDoubleDown()
    {
        Debug.Log("Double Down");
    }

    public void OnSurrender()
    {
        Debug.Log("Surrender");
    }

    public void OnInsurance()
    {
        Debug.Log("Insurance");
    }

    public void OnSplit()
    {
        Debug.Log("Split");
    }

    public void OnDealerTurn()
    {
        bool checkwinner = false;
        int dealerScore = game.GetDealerScore();
        while (dealerScore < 17)
        {
            Card card = game.Hit();
            dealerScore = game.GetDealerScore();
            checkwinner = true;
        }

        if (!checkwinner)
        {
            game.CheckWinner();
        }

        UpdateScore();
    }


    public void RestartGame()
    {
        uiManager.ShowInGame();
        cardManager.ClearHands(cardManager.playerHand);
        cardManager.ClearHands(cardManager.dealerHand);

        game.InGame();
        //game.OnCardDealt += SetCardImg;

        UpdateScore();
        CheckResult();
    }

}
