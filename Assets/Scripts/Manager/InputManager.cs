using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Button hitButton;
    public Button stayButton;
    public Button startButton;
    public Button homeButton;
    public Button restartButton;
    public Button exitButton;

    public Button confirmBetButton;
    public Button upArrow;
    public Button downArrow;
    public Button leftArrow;
    public Button rightArrow;

    public Button minButton;
    public Button maxButton;

    public GameManager gameDirector;
    public BlackjackManager blackjackManager;
    public BetManager betManager;
    public UIManager uiManager;

    void Start()
    {
        CacheManagers();
        if (ManagersNotFound()) return;

        InitOnClick();
    }

    void Update()
    {
        CheckArrowKeys();
        UpdateButtonStates();
    }

    private void CacheManagers()
    {
        blackjackManager = FindObjectOfType<BlackjackManager>();
        gameDirector = FindObjectOfType<GameManager>();
        betManager = FindObjectOfType<BetManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private bool ManagersNotFound()
    {
        if (gameDirector == null || betManager == null || uiManager == null)
        {
            Debug.LogError("gameDirector, BetManager, or UIManager is not found in the scene!");
            return true;
        }
        return false;
    }

    private void CheckArrowKeys()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            confirmBetButton.onClick.Invoke();

        CheckKey(KeyCode.UpArrow, upArrow);
        CheckKey(KeyCode.DownArrow, downArrow);
        CheckKey(KeyCode.LeftArrow, leftArrow);
        CheckKey(KeyCode.RightArrow, rightArrow);
    }

    private void CheckKey(KeyCode key, Button button)
    {
        if (Input.GetKeyDown(key))
            button.onClick.Invoke();
    }

    public void ChangeBetButtons(bool isActive)
    {
        SetButtonGroupActive(isActive, confirmBetButton, upArrow, downArrow, leftArrow, rightArrow);
    }

    public void ChangeHitNStay(bool isActive)
    {
        SetButtonGroupActive(isActive, hitButton, stayButton);
    }

    public void ChangeStartNExit(bool isActive)
    {
        SetButtonGroupActive(isActive, startButton, exitButton);
    }

    public void ChangeRestartNHome(bool isActive)
    {
        SetButtonGroupActive(isActive, restartButton, homeButton);
    }

    private void SetButtonGroupActive(bool isActive, params Button[] buttons)
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    public void InitOnClick()
    {
        // Bet buttons
        AddListener(confirmBetButton, () => uiManager.ConfirmBetAndStartGame());
        AddListener(upArrow, () => betManager.UpArrow());
        AddListener(downArrow, () => betManager.DownArrow());
        AddListener(leftArrow, () => betManager.LeftArrow());
        AddListener(rightArrow, () => betManager.RightArrow());
        AddListener(minButton, () => betManager.MinChips());
        AddListener(maxButton, () => betManager.MaxChips());

        // In-game buttons
        AddListener(hitButton, () => blackjackManager.OnHit());
        AddListener(stayButton, () => blackjackManager.OnStay());

        // Menu buttons
        AddListener(startButton, () => gameDirector.StartSinglePlayerGame());
        AddListener(exitButton, () => gameDirector.ExitGame());

        // Restart/Home buttons
        AddListener(restartButton, () => blackjackManager.OnBet());
        AddListener(homeButton, () => uiManager.ShowLobby());
    }

    private void AddListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }

    private void UpdateButtonStates()
    {
        int playerChips = betManager.GetPlayerChips();
        int currentBet = betManager.GetCurrentBet();
        int initialChips = betManager.GetInitialChips();

        rightArrow.interactable = playerChips - 10 >= currentBet;
        leftArrow.interactable = currentBet - 10 >= initialChips;
        upArrow.interactable = playerChips - 100 >= currentBet;
        downArrow.interactable = currentBet - 100 >= initialChips;
        confirmBetButton.interactable = currentBet <= playerChips;

        startButton.interactable = uiManager.GetCanPlay();
        restartButton.interactable = uiManager.GetCanPlay();
    }
}
