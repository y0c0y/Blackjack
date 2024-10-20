using UnityEngine;
using UnityEngine.UI;

public class BlackjackManager : MonoBehaviour
{

    public UIManager uiManager;
    public CardManager cardManager;
    public BetManager betManager;


    public bool openDealerCard = false;
    public BlackjackGame game;

    private void Start()
    {
        CacheManagers();
        if (ManagersNotFound()) return;
        uiManager.ShowLobby();
    }

    private void CacheManagers()
    {
        uiManager = FindObjectOfType<UIManager>();
        cardManager = FindObjectOfType<CardManager>();
        betManager = FindObjectOfType<BetManager>();
    }

    private bool ManagersNotFound()
    {
        if (uiManager == null || betManager == null || cardManager ==null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return true;
        }
        return false;
    }

    public void EnterGame()
    {
        game.OnCardDealt += SetCardImg;
        openDealerCard = false;
        OnBet();
    }

    public void OnBet()
    {
        betManager.InitBetting();
        uiManager.ShowBetting();
    }

    public void StartRound()
    {
        game.InGame();
        UpdateScore();
        CheckResult();
    }

    public void UpdateScore()
    {
        int playerScore = game.GetPlayerScore();
        int dealerScore = openDealerCard ? game.GetDealerScore() : game.GetDealer().GetHand()[0].GetIntValue();
        uiManager.UpdateScore(playerScore, dealerScore);
    }

    public void SetCardImg(bool isPlayer, int cardIndex)
    {
        Player player = isPlayer ? game.GetPlayer() : game.GetDealer();
        int spriteIndex = player.GetHand()[cardIndex].GetSourceIdx();
        if (!openDealerCard && cardIndex == 1 && !isPlayer)
        {
            cardManager.SetCardImg(isPlayer, cardIndex, 44, openDealerCard);  // Placeholder for hidden dealer card
        }
        else
        {
            cardManager.SetCardImg(isPlayer, cardIndex, spriteIndex, openDealerCard);
        }
    }

    public void CheckResult()
    {
        Enums.GameResult result = game.GetResult();
        uiManager.UpdateResult(result);

        if (result != Enums.GameResult.None)
        {
            uiManager.ShowResult();
            Invoke(nameof(HandClear), 1.5f);
        }
    }

    public void OnHit()
    {
        game.Hit();
        UpdateScore();

        if (game.GetPlayerScore() > 21)
        {
            OnStay();
        }
    }

    public void OnStay()
    {
        game.ChangeTurn();
        openDealerCard = true;
        OnDealerTurn();
        CheckResult();
    }

    public void OnDealerTurn()
    {
        Destroy(cardManager.dealerHand.GetChild(1).gameObject);
        SetCardImg(false, 1);
        int dealerScore = game.GetDealerScore();
        while (dealerScore < 17)
        {
            game.Hit();
            dealerScore = game.GetDealerScore();
        }
        game.CheckWinner();
        UpdateScore();
    }

    public void HandClear()
    {

        if(cardManager.playerHand == null || cardManager.dealerHand == null) return;
        cardManager.ClearHands(cardManager.playerHand);
        cardManager.ClearHands(cardManager.dealerHand);
    }


}
