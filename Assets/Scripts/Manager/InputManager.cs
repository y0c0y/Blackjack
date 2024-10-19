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
    //public Button stopButton;
    //public Button playingButton;
    public Button restartButton;
    public Button exitButton;

    public Button betButton;
    public Button upArrow;
    public Button downArrow;
    public Button leftArrow;
    public Button rightArrow;

    private GameManager gameManager;
    private BetManager betManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        betManager = FindObjectOfType<BetManager>();
        if (gameManager == null || betManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return;
        }

    }

    public void ChangeBetButtons(bool tmp)
    {
        betButton.gameObject.SetActive(tmp);
        upArrow.gameObject.SetActive(tmp);
        downArrow.gameObject.SetActive(tmp);
        leftArrow.gameObject.SetActive(tmp);
        rightArrow.gameObject.SetActive(tmp);

        if(tmp)
        {
            betButton.onClick.AddListener(() => betManager.ConfirmBet());
            upArrow.onClick.AddListener(() => betManager.UpArrow());
            downArrow.onClick.AddListener(() => betManager.DownArrow());
            leftArrow.onClick.AddListener(() => betManager.LeftArrow());
            rightArrow.onClick.AddListener(() => betManager.RightArrow());
        }
        else
        {
            betButton.onClick.RemoveAllListeners();
            upArrow.onClick.RemoveAllListeners();
            downArrow.onClick.RemoveAllListeners();
            leftArrow.onClick.RemoveAllListeners();
            rightArrow.onClick.RemoveAllListeners();
        }
    }

    public void ChangeHitNStay(bool tmp)
    {
        hitButton.gameObject.SetActive(tmp);
        stayButton.gameObject.SetActive(tmp);

        if(tmp)
        {
            hitButton.onClick.AddListener(() => gameManager.OnHit());
            stayButton.onClick.AddListener(() => gameManager.OnStay());
        }
        else
        {
            hitButton.onClick.RemoveAllListeners();
            stayButton.onClick.RemoveAllListeners();
        }
    }

    public void ChangeStartNExit(bool tmp)
    {
        startButton.gameObject.SetActive(tmp);
        exitButton.gameObject.SetActive(tmp);

        if (tmp)
        { 
            startButton.onClick.AddListener(() => gameManager.StartGame());
            exitButton.onClick.AddListener(() => gameManager.ExitGame());
        }
        else
        {
            startButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }

    public void ChangeRestartNExit(bool tmp)
    {
        restartButton.gameObject.SetActive(tmp);
        exitButton.gameObject.SetActive(tmp);

        if(tmp)
        {
            restartButton.onClick.AddListener(() => gameManager.RestartGame());
            exitButton.onClick.AddListener(() => gameManager.ExitGame());
        }
        else
        {
            restartButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }

    }

}
