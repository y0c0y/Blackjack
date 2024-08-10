using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Enums.GameResult _result;


    private BlackjackGame game;

    public Canvas menuCanvas;

    public Canvas inGameCanvas;

    public Text playerScoreText;
    public Text dealerScoreText;

    public Button hitButton;
    public Button stayButton;

    public Canvas resultCanvas;
    public Text resultText;
    public Text coinText;

    public Button playAgainButton;
    public Button exitButton;
    public Button playingButton;

    void Start()
    {
        game = new BlackjackGame();
        game.StartGame();


        UpdateScore();

        hitButton.onClick.AddListener(OnHit);
        stayButton.onClick.AddListener(OnStay);
    }

    //int CheckResult()
    //{
    //    int playerScore = game.GetPlayerScore();
    //    int dealerScore = game.GetDealerScore();

    //    if (playerScore > 21)
    //    {
    //        return -1;
    //    }
    //    else if (dealerScore > 21)
    //    {
    //        return 1;
    //    }
    //    else if (playerScore == dealerScore)
    //    {
    //        return 0;
    //    }
    //    else if (playerScore > dealerScore)
    //    {
    //        return 1;
    //    }
    //    else
    //    {
    //        return -1;
    //    }
    //}

    void UpdateScore()
    {
        playerScoreText.text = "Player Score: " + game.GetPlayerScore();
        dealerScoreText.text = "Dealer Score: " + game.GetDealerScore();
    }

    void OnHit()
    {
        Card card = game.Hit();
        Debug.Log("Hit: " + card.GetSuit() + " " + card.GetValue());
        UpdateScore();

    }

    void OnStay()
    {
        //UpdateScore();

        Debug.Log("Stay");
        game.ChangeTurn();
    }

    public void InitializeGame()
    {
        menuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        resultCanvas.gameObject.SetActive(false);
    }

    public void ShowResult()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        resultCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
        //coinText.text = "Coin: " + game.GetCoin();
        
    }   

    public void ResetGame()
    {
        game.StartGame();
        UpdateScore();
    }

}
