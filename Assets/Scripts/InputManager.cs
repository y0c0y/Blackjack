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

    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return;
        }


        ChangeHitNStay(true);

        if (hitButton == null || stayButton == null)
        {
            Debug.LogError("One of the buttons is not assigned in the Inspector!");
            return;
        }

        // Add listener to buttons
        hitButton.onClick.AddListener(() => gameManager.OnHit());
        stayButton.onClick.AddListener(() => gameManager.OnStay());
        restartButton.onClick.AddListener(() => gameManager.RestartGame());

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

    public void ChangeRestartNExit(bool tmp)
    {
        restartButton.gameObject.SetActive(tmp);
        exitButton.gameObject.SetActive(tmp);
    }

}
