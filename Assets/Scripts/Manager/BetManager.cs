using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    public Text chipText;
    public Text betAmountText;

    private int currentBet = 0;
    private int playerChips = 0;  // Set initial chips
    private int initialChips = 200;

    void Start()
    {
        UpdateChipUI();
        UpdateBetUI();
    }

    public void RightArrow()
    {
        if (playerChips > currentBet) // Only increase if the player has enough chips
        {
            currentBet += 10; // Adjust increment as needed
            UpdateBetUI();
        }
    }

    public void LeftArrow()
    {
        if (currentBet > initialChips)
        {
            currentBet -= 10; // Adjust decrement as needed
            UpdateBetUI();
        }
    }

    public void UpArrow()
    {
        if (playerChips > currentBet) // Only increase if the player has enough chips
        {
            currentBet += 100; // Adjust increment as needed
            UpdateBetUI();
        }
    }

    public void DownArrow()
    {
        if (currentBet > initialChips)
        {
            currentBet -= 100; // Adjust decrement as needed
            UpdateBetUI();
        }
    }

    public void ConfirmBet()
    {
        if (currentBet <= playerChips) // Ensure bet is valid
        {
            playerChips -= currentBet;
            UpdateChipUI();
            // Pass the bet to the GameManager or start the round here
        }
    }

    private void UpdateChipUI()
    {
        chipText.text = "Chips: " + playerChips.ToString();
    }

    private void UpdateBetUI()
    {
        betAmountText.text = "Bet: " + currentBet.ToString();
    }
}
