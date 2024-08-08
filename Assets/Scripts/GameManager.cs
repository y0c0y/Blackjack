using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BlackjackGame game;

    public GameObject playerCardPrefab;
    public GameObject dealerCardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        game = new BlackjackGame();
        UpdateScore();
    }

    // Update is called once per frame
    void UpdateScore()
    {
        List<Card> player = game.GetPlayer();
        List<Card> dealer = game.GetDealer();

        int playerScore = game.GetPlayerScore();
        int dealerScore = game.GetDealerScore();

        Debug.Log("Player Score: " + playerScore);
        Debug.Log("Dealer Score: " + dealerScore);
        
    }
}
