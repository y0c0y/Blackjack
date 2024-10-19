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

    private void Start()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        betCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        commonCanvas.gameObject.SetActive(false);
    }

    public void ShowBetting()
    {
        menuCanvas.gameObject.SetActive(false);
        commonCanvas.gameObject.SetActive(true);

        playerScoreText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);

        betCanvas.gameObject.SetActive(true);
    }

    public void ShowInGame()
    {
        menuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);

        playerScoreText.gameObject.SetActive(true);
        dealerScoreText.gameObject.SetActive(true);

        betCanvas.gameObject.SetActive(false);
    }

    public void ConfirmBetAndStartGame()
    {
        betManager.ConfirmBet();
        ShowInGame(); // Proceed to in-game Canvas after bet is confirmed
    }

    public void ShowResult()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
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
