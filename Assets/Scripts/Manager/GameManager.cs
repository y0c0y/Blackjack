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
    public BetManager betManager;

    public bool openDealerCard = false;

    private void Start()
    {
        uiManager.ShowMenu();
        inputManager.ChangeBetButtons(false);
        inputManager.ChangeHitNStay(false);
        inputManager.ChangeRestartNExit(false);
        inputManager.ChangeStartNExit(true);
    }

    public void StartGame()
    {
        game = new BlackjackGame();
        game.OnCardDealt += SetCardImg;
        game.StartGame();
        
        UpdateScore();
        CheckResult();

    }


    public IEnumerator Dealay(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Delay");
    }

    public void UpdateScore()
    {
        int playerScore= game.GetPlayerScore();
        int dealerScoregame = 0; 

        if(!openDealerCard)
        {
            dealerScoregame = game.GetDealer().GetHand()[0].GetIntValue();
        }
        else
        {
            dealerScoregame = game.GetDealerScore();
        }


        uiManager.UpdateScore(playerScore, dealerScoregame);
    }

    public void SetCardImg(bool isPlayer, int cardIndex)
    {
        Player player = isPlayer ? game.GetPlayer() : game.GetDealer();
        int spriteIndex = player.GetHand()[cardIndex].GetSourceIdx();

        if(!openDealerCard && cardIndex == 1 && !isPlayer)
        {
            cardManager.SetCardImg(isPlayer, cardIndex, 44);
        }
        else
        {
            cardManager.SetCardImg(isPlayer, cardIndex, spriteIndex);
        }

        Canvas.ForceUpdateCanvases();
    }


    public void CheckResult()
    {

        Enums.GameResult result = game.GetResult();
        uiManager.UpdateResult(result);

        if (result != Enums.GameResult.None)
        {
            uiManager.ShowResult();
        }
    }

    public void OnHit()
    {

        game.Hit();
        UpdateScore();

        if(game.GetPlayerScore()>21)
        {
           OnStay();
        }

    }

    public void OnStay()
    {
        game.ChangeTurn();
        openDealerCard = true;
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

    public void OnBet()
    {
        uiManager.ShowBetting();
    }

    public void ExitGame() {
        // Quit the game
        Application.Quit();

        // For editor mode, this line can be used (not necessary for builds)
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }



    public void OnDealerTurn()
    {
        Destroy(cardManager.dealerHand.GetChild(1).gameObject);
        SetCardImg(false, 1);
        int dealerScore = game.GetDealerScore();
        while (dealerScore < 17)
        {
            game.Hit();
            dealerScore = game.GetDealerScore();

        }

        game.CheckWinner();

        UpdateScore();
    }


    public void RestartGame()
    {
        uiManager.ShowInGame();
        cardManager.ClearHands(cardManager.playerHand);
        cardManager.ClearHands(cardManager.dealerHand);
        openDealerCard = false;
        game.InGame();

        UpdateScore();
        CheckResult();
    }

}
