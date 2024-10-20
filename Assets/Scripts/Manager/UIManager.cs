using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas resultCanvas;
    public Canvas commonCanvas;
    public Canvas betCanvas;

    public Text chipText;
    public Text betAmountText;

    public Text playerScoreText;
    public Text dealerScoreText;
    public Text resultText;

    public Text notice;

    public BetManager betManager;
    public InputManager inputManager;
    public GameManager gameManager;

    private bool canPlay = true;

    public bool GetCanPlay() => canPlay;
    public void SetCanPlay(bool _canPlay) => canPlay = _canPlay;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        betManager = FindObjectOfType<BetManager>();
        gameManager = FindObjectOfType<GameManager>();

        if (inputManager == null || betManager == null || gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return;
        }

    }

    public void ShowMenu()
    {
        SetActiveCanvases(true, false, false, false, false);
        UpdateInputManagerStates(true, false, false, false);
    }

    public void ShowBetting()
    {

        SetActiveCanvases(false, true, false, false);
        //SetBettingTextActive(true);
        SetInGameTextActive(false);
        UpdateInputManagerStates(false, true, false, false);
    }

    public void ShowInGame()
    {
        SetActiveCanvases(false, false, true, false);
        SetInGameTextActive(true);
        UpdateInputManagerStates(false, false, true, false);
    }

    public void ShowResult()
    {

        Debug.Log("ShowResult: " + resultText.text);

        SetActiveCanvases(false, false, false, true);
        UpdateInputManagerStates(false, false, false, true);
       
    }

    private void SetActiveCanvases(bool menu, bool bet, bool inGame, bool result,  bool common = true)
    {
        menuCanvas.gameObject.SetActive(menu);
        commonCanvas.gameObject.SetActive(common);
        betCanvas.gameObject.SetActive(bet);
        inGameCanvas.gameObject.SetActive(inGame);
        resultCanvas.gameObject.SetActive(result);
       
    }

    private void UpdateInputManagerStates(bool startNExit, bool betButtons, bool hitNStay, bool restartNHome)
    {
        if (inputManager == null)
        {
            Debug.LogError("Input Manager is not assigned.");
            return;
        }

        inputManager.ChangeStartNExit(startNExit);
        inputManager.ChangeBetButtons(betButtons);
        inputManager.ChangeHitNStay(hitNStay);
        inputManager.ChangeRestartNHome(restartNHome);
    }

    private void SetBettingTextActive(bool isActive)
    {
        betAmountText.gameObject.SetActive(isActive);
        chipText.gameObject.SetActive(isActive);
    }

    private void SetInGameTextActive(bool isActive)
    {
        playerScoreText.gameObject.SetActive(isActive);
        dealerScoreText.gameObject.SetActive(isActive);
    }


    public void ConfirmBetAndStartGame()
    {
        betManager.ConfirmBet();
        ShowInGame();
        gameManager.StartGame();
    }


    public void UpdateChipUI()
    {
        int playerChips = betManager.GetPlayerChips();
        chipText.text = playerChips.ToString();
    }

    public void UpdateBetUI()
    {
        int bet = betManager.GetCurrentBet();
        betAmountText.text = bet.ToString();
    }

    public void UpdateScore(int playerScore, int dealerScore)
    {
        playerScoreText.text = "Player Score: " + playerScore;
        dealerScoreText.text = "Dealer Score: " + dealerScore;
    }
    public void UpdateResult(Enums.GameResult _result)
    {
        int playerChips = betManager.GetPlayerChips();
        int bet = betManager.GetCurrentBet();
        int payout = CalculatePayout(_result, bet);
        string resultMessage = GenerateResultMessage(_result, bet, payout, playerChips);

        gameManager.game.GetPlayer().SetChips(playerChips + payout);
        resultText.text = resultMessage;
    }

    
    private int CalculatePayout(Enums.GameResult _result, int bet)
    {
        return _result switch
        {
            Enums.GameResult.PlayerWin => bet * 2,
            Enums.GameResult.Push => bet,
            Enums.GameResult.PlayerBlackjack => (int)Math.Ceiling(bet * 2.5f),
            Enums.GameResult.DealerBust => bet * 2,
            _ => 0
        };
    }


    private string GenerateResultMessage(Enums.GameResult _result, int bet, int payout, int playerChips)
    {
        string message = _result switch
        {
            Enums.GameResult.PlayerWin => $"You win!!!\n You got {payout} coins!",
            Enums.GameResult.DealerWin => GenerateLossMessage(bet, playerChips, payout, "You lose.\n You lost "),
            Enums.GameResult.Push => "Push.\n No loss, no gain.",
            Enums.GameResult.PlayerBlackjack => $"Congratulations, You got Blackjack!\n You got {payout} coins!",
            Enums.GameResult.DealerBlackjack => GenerateLossMessage(bet, playerChips, payout, "Oh.. Dealer got Blackjack..."),
            Enums.GameResult.PlayerBust => GenerateLossMessage(bet, playerChips, payout, "Hmm..., Let's not get carried away.\n You lost "),
            Enums.GameResult.DealerBust => $"So lucky, the Dealer was too greedy.\n You got {payout} coins!",
            _ => ""
        };

        return message;
    }

    private string GenerateLossMessage(int bet, int playerChips, int payout ,string baseMessage)
    {
        string message = baseMessage + bet + " coins...";
        if (playerChips + payout < betManager.GetInitialChips())
        {
            message += "\nYou can't bet anymore!!";
            canPlay = false;
            notice.text = "Not enough chips to play!";
        }
        return message;
    }


}
