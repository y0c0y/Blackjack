using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public BetManager betManager;

    public BlackjackManager blackjackManager;

    private List<BlackjackManager> gameSessions = new List<BlackjackManager>();

    private void Start()
    {
        CacheManagers();
        if (ManagersNotFound()) return;
        uiManager.ShowLobby();
    }

    private void CacheManagers()
    {
        uiManager = FindObjectOfType<UIManager>();
        betManager = FindObjectOfType<BetManager>();
    }

    private bool ManagersNotFound()
    {
        if (uiManager == null || betManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
            return true;
        }
        return false;
    }

    public void StartSinglePlayerGame()
    {
        //BlackjackManager newGame = CreateNewGameSession();
        //newGame.EnterGame();

        Debug.Log("Start single player game");
        blackjackManager.game = new BlackjackGame();
        blackjackManager.EnterGame();
        
    }

    public void StartMultiplayerGame()
    {
        for (int i = 0; i < 2; i++)  // Example: 2-player multiplayer
        {
            BlackjackManager newGame = CreateNewGameSession();
            gameSessions.Add(newGame); 
            newGame.OnBet();
        }
    }

    private BlackjackManager CreateNewGameSession()
    {
        GameObject gameObject = new GameObject("BlackjackGameSession");
        BlackjackManager newGame = gameObject.AddComponent<BlackjackManager>();
        return newGame;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
