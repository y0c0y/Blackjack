using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas resultCanvas;
    public Canvas commonCanvas;
    public Canvas betCanvas;

    public Text playerScoreText;
    public Text dealerScoreText;
    public Text resultText;
    public Text resultCoinText;
    public Text ownCoinText;

    public BetManager betManager;
    public InputManager inputManager;

    private GameManager gameManager;

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
        menuCanvas.gameObject.SetActive(true);
        commonCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        betCanvas.gameObject.SetActive(false);

        inputManager.ChangeStartNExit(true);
        inputManager.ChangeBetButtons(false);
        inputManager.ChangeHitNStay(false);
        inputManager.ChangeRestartNHome(false);
   
    }

    public void ShowBetting()
    {
        menuCanvas.gameObject.SetActive(false);

        commonCanvas.gameObject.SetActive(true);
        playerScoreText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);

        betCanvas.gameObject.SetActive(true);
        resultCanvas.gameObject.SetActive(false);


        inputManager.ChangeStartNExit(false);
        inputManager.ChangeHitNStay(false);
        inputManager.ChangeRestartNHome(false);
        inputManager.ChangeBetButtons(true);

        betManager.SetPlayerChips(gameManager.game.GetPlayer().GetChips());

    }

    public void ShowInGame()
    {
        betCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        playerScoreText.gameObject.SetActive(true);
        dealerScoreText.gameObject.SetActive(true);

        inputManager.ChangeBetButtons(false);
        inputManager.ChangeHitNStay(true);
    }

    public void ConfirmBetAndStartGame()
    {
        betManager.ConfirmBet();
        ShowInGame();
        gameManager.StartGame();
    }

    public void ShowResult()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);

        inputManager.ChangeHitNStay(false);
        inputManager.ChangeRestartNHome(true);
    }

    public void UpdateScore(int playerScore, int dealerScore)
    {
        playerScoreText.text = "Player Score: " + playerScore;
        dealerScoreText.text = "Dealer Score: " + dealerScore;
    }

    public void UpdateResult(Enums.GameResult _result)
    {
        switch (_result)
        {
            case Enums.GameResult.PlayerWin:
                resultText.text = "You win!!!";
                break;
            case Enums.GameResult.DealerWin:
                resultText.text = "You lose....";
                break;
            case Enums.GameResult.Push:
                resultText.text = "Push, You can't get any coins ";
                break;
            case Enums.GameResult.PlayerBlackjack:
                resultText.text = "Congratulations, You got Blackjack!";
                break;
            case Enums.GameResult.DealerBlackjack:
                resultText.text = "Oh.. Dealer got Blackjack...";
                break;
            case Enums.GameResult.PlayerBust:
                resultText.text = "Hmm..., Let's not get carried away ";
                break;
            case Enums.GameResult.DealerBust:
                resultText.text = "So Luckly, Dealer were so greedy.";
                break;
            default:
                resultText.text = "";
                break;
        }
    }
}
