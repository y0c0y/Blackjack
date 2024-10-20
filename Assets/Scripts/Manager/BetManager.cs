using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    private int currentBet = 0;
    private int playerChips = 0;
    private int initialChips = 200;

    public BlackjackManager blackjackManager;
    public UIManager uiManager;

    public int GetCurrentBet() => currentBet;
    public int GetPlayerChips() => playerChips;
    public int GetInitialChips() => initialChips;

    public void SetPlayerChips(int chips) => playerChips = chips;
    public void SetCurrentBet(int bet) => currentBet = bet;

    void Start()
    {
        CacheManagers();
        if (ManagersNotFound()) return;
    }

    private void CacheManagers()
    {
        blackjackManager = FindObjectOfType<BlackjackManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private bool ManagersNotFound()
    {
        if (blackjackManager == null || uiManager == null)
        {
            Debug.LogError(" blackjackGameManager or UIManager is missing in the scene!");
            return true;
        }
        return false;
    }

    public void InitBetting()
    {
        Debug.Log("Init betting");
        playerChips = blackjackManager.game.GetPlayer().GetChips();
        Debug.Log("Player chips: " + playerChips);
        currentBet = initialChips;

        UpdateUI();
    }

    public void RightArrow() => AdjustBet(10);
    public void LeftArrow() => AdjustBet(-10);
    public void UpArrow() => AdjustBet(100);
    public void DownArrow() => AdjustBet(-100);

    public void MinChips()
    {
        currentBet = initialChips;
        UpdateUI();
    }

    public void MaxChips()
    {
        currentBet = playerChips;
        UpdateUI();
    }

    private void AdjustBet(int changeAmount)
    {
        int newBet = currentBet + changeAmount;

        if (IsValidBetChange(newBet))
        {
            currentBet = newBet;
            uiManager.UpdateBetUI();
        }
    }

    private bool IsValidBetChange(int newBet)
    {
        return newBet >= initialChips && newBet <= playerChips;
    }

    public void ConfirmBet()
    {
        if (currentBet <= playerChips)
        {
            playerChips -= currentBet;
            blackjackManager.game.GetPlayer().SetChips(playerChips);
            blackjackManager.game.GetPlayer().SetBet(currentBet);

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        uiManager.UpdateChipUI();
        uiManager.UpdateBetUI();
    }
}
