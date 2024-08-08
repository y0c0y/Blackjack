using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private BlackjackGame game;

    public Text playerScoreText;
    public Text dealerScoreText;

    public Button hitButton;
    public Button stayButton;

    // Start is called before the first frame update
    void Start()
    {
        game = new BlackjackGame();
        UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        playerScoreText.text = "Player Score: " + game.GetPlayerScore();
        dealerScoreText.text = "Dealer Score: " + game.GetDealerScore();
    }


    void OnHit()
    {
        game.Hit(game.GetPlayer());
        UpdateScore();
    }

}
