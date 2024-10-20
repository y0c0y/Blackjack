using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{


    private int currentBet = 0;
    private int playerChips = 0;  // Set initial chips
    private int initialChips = 200;

    public int GetCurrentBet() => currentBet;
    public int GetPlayerChips() => playerChips;

    public int GetInitialChips() => initialChips;

    public void SetPlayerChips(int chips) => playerChips = chips;

    public void SetCurrentBet(int bet) => currentBet = bet;

    public GameManager gameManager;
    public UIManager uiManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        if (gameManager == null || uiManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return;
        }
    }

    public void InitBetting()
    {

        playerChips = gameManager.game.GetPlayer().GetChips();
        currentBet = initialChips;

        uiManager.UpdateChipUI();
        uiManager.UpdateBetUI();
    }

    public void RightArrow()
    {
        if (playerChips - 10 >= currentBet) 
        {
            currentBet += 10;
            uiManager.UpdateBetUI();
        }
    }

    public void LeftArrow()
    {
        if (currentBet - 10 >= initialChips)
        {
            currentBet -= 10;
            uiManager.UpdateBetUI();
        }
    }

    public void UpArrow()
    {
        if (playerChips - 100 >= currentBet)
        {
            currentBet += 100;
            uiManager.UpdateBetUI();
        }
    }

    public void DownArrow()
    {
        if (currentBet - 100 >= initialChips)
        {
            currentBet -= 100;
            uiManager.UpdateBetUI();
        }
    }

    public void ConfirmBet()
    {
        if (currentBet <= playerChips) 
        {
            playerChips -= currentBet;
            uiManager.UpdateChipUI();

            gameManager.game.GetPlayer().SetChips(playerChips);
            gameManager.game.GetPlayer().SetBet(currentBet);
        }
    }

}
