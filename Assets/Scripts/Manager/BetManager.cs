using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    private int currentBet = 0;
    private int playerChips = 0;
    private int initialChips = 200;

    public GameManager gameManager;
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

        InitBetting();
    }

    private void CacheManagers()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private bool ManagersNotFound()
    {
        if (gameManager == null || uiManager == null)
        {
            Debug.LogError("GameManager or UIManager is missing in the scene!");
            return true;
        }
        return false;
    }

    public void InitBetting()
    {
        playerChips = gameManager.game.GetPlayer().GetChips();
        currentBet = initialChips;

        UpdateUI();
    }

    public void RightArrow() => AdjustBet(10);
    public void LeftArrow() => AdjustBet(-10);
    public void UpArrow() => AdjustBet(100);
    public void DownArrow() => AdjustBet(-100);

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
            gameManager.game.GetPlayer().SetChips(playerChips);
            gameManager.game.GetPlayer().SetBet(currentBet);

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        uiManager.UpdateChipUI();
        uiManager.UpdateBetUI();
    }
}
