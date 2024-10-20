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

    public GameManager gameManager;
    public BetManager betManager;
    public UIManager uiManager;    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        betManager = FindObjectOfType<BetManager>();
        uiManager = FindObjectOfType<UIManager>();

        if (gameManager == null || betManager == null || uiManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return;
        }

        InitOnClick();

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            confirmBetButton.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upArrow.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downArrow.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftArrow.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightArrow.onClick.Invoke();
        }

        UpdateButtonStates();
    }

    public void ChangeBetButtons(bool tmp)
    {
        confirmBetButton.gameObject.SetActive(tmp);
        upArrow.gameObject.SetActive(tmp);
        downArrow.gameObject.SetActive(tmp);
        leftArrow.gameObject.SetActive(tmp);
        rightArrow.gameObject.SetActive(tmp);

    }

    public void ChangeHitNStay(bool tmp)
    {
        hitButton.gameObject.SetActive(tmp);
        stayButton.gameObject.SetActive(tmp);
    }

    public void ChangeStartNExit(bool tmp)
    {
        startButton.gameObject.SetActive(tmp);
        exitButton.gameObject.SetActive(tmp);
    }

    public void ChangeRestartNHome(bool tmp)
    {
        restartButton.gameObject.SetActive(tmp);
        homeButton.gameObject.SetActive(tmp);
    }

    public void InitOnClick()
    {
        //bet
        confirmBetButton.onClick.AddListener(() => uiManager.ConfirmBetAndStartGame());
        upArrow.onClick.AddListener(() => betManager.UpArrow());
        downArrow.onClick.AddListener(() => betManager.DownArrow());
        leftArrow.onClick.AddListener(() => betManager.LeftArrow());
        rightArrow.onClick.AddListener(() => betManager.RightArrow());


        //inGame
        hitButton.onClick.AddListener(() => gameManager.OnHit());
        stayButton.onClick.AddListener(() => gameManager.OnStay());


        //menu
        startButton.onClick.AddListener(() => gameManager.OnBet());
        exitButton.onClick.AddListener(() => gameManager.ExitGame());


        //restart
        restartButton.onClick.AddListener(() => gameManager.OnBet());
        homeButton.onClick.AddListener(() => gameManager.OnMenu());
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

