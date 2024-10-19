using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;


public class InputManager : MonoBehaviour
{
    public Button hitButton;
    public Button stayButton;
    public Button startButton;
    public Button homeButton;
    public Button restartButton;
    public Button exitButton;

    public Button betButton;
    public Button upArrow;
    public Button downArrow;
    public Button leftArrow;
    public Button rightArrow;

    private GameManager gameManager;
    private BetManager betManager;
    private UIManager uiManager;    

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

    public void ChangeBetButtons(bool tmp)
    {
        betButton.gameObject.SetActive(tmp);
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

        Debug.Log("ChangeStartNExit");
    }

    public void ChangeRestartNHome(bool tmp)
    {
        restartButton.gameObject.SetActive(tmp);
        homeButton.gameObject.SetActive(tmp);
    }

    public void InitOnClick()
    {
        //bet
        betButton.onClick.AddListener(() => uiManager.ConfirmBetAndStartGame());
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

}
