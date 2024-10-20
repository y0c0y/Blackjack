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

    public GameObject bettingChips;
    public GameObject playerOwnChips;


    public BetManager betManager;
    public InputManager inputManager;
    public BlackjackManager blackjackManager;

    private bool canPlay = true;

    public bool GetCanPlay() => canPlay;
    public void SetCanPlay(bool _canPlay) => canPlay = _canPlay;

    void Start()
    {
        CacheManagers();
        if (ManagersNotFound()) return;
    }

    private void CacheManagers()
    {
        inputManager = FindObjectOfType<InputManager>();
        betManager = FindObjectOfType<BetManager>();
        blackjackManager = FindObjectOfType<BlackjackManager>();
    }

    private bool ManagersNotFound()
    {
        if (inputManager == null || betManager == null || blackjackManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return true;
        }
        return false;
    }

    public void ShowLobby()
    {
        SetActiveCanvases(true, false, false, false, false);
        UpdateInputManagerStates(true, false, false, false);
    }

    public void ShowBetting()
    {

        SetActiveCanvases(false, true, false, false, true);
        SetBettingTextActive(true);
        SetInGameTextActive(false);
        UpdateInputManagerStates(false, true, false, false);

        UpdateScore(0, 0);// reset score

    }

    public void ShowInGame()
    {
        SetActiveCanvases(false, false, true, false, true);
        SetInGameTextActive(true);
        UpdateInputManagerStates(false, false, true, false);
    }

    public void ShowResult()
    {
        SetActiveCanvases(false, false, false, true, true);
        SetBettingTextActive(false);
        UpdateInputManagerStates(false, false, false, true);
       
    }

    private void SetActiveCanvases(bool lobby, bool bet, bool inGame, bool result,  bool common)
    {
        menuCanvas.gameObject.SetActive(lobby);
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
        bettingChips.gameObject.SetActive(isActive);
        playerOwnChips.gameObject.SetActive(isActive);
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
        blackjackManager.StartRound();
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

        blackjackManager.game.GetPlayer().SetChips(playerChips + payout);
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
            Enums.GameResult.PlayerWin => $"You win!!!\nYou got {payout} coins!",
            Enums.GameResult.DealerWin => GenerateLossMessage(bet, playerChips, payout, "You lose."),
            Enums.GameResult.Push => "Push.\n No loss, no gain.",
            Enums.GameResult.PlayerBlackjack => $"Congratulations\n You got Blackjack!\nYou got {payout} coins!",
            Enums.GameResult.DealerBlackjack => GenerateLossMessage(bet, playerChips, payout, "Dealer got Blackjack."),
            Enums.GameResult.PlayerBust => GenerateLossMessage(bet, playerChips, payout, "Let's not get carried away."),
            Enums.GameResult.DealerBust => $"So lucky.\nThe Dealer was too greedy.\nYou got {payout} coins!",
            _ => ""
        };

        return message;
    }

    private string GenerateLossMessage(int bet, int playerChips, int payout ,string baseMessage)
    {
        string message = baseMessage + "\nYou lost " + bet + " coins...";
        if (playerChips + payout < betManager.GetInitialChips())
        {
            message += "\nYou can't bet anymore!!";
            canPlay = false;
            notice.text = "Not enough chips to play!";
        }
        return message;
    }


}
