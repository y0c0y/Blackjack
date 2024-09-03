using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private BlackjackGame game;

    public Canvas menuCanvas;
    public Canvas inGameCanvas;

    public Text playerScoreText;
    public Text dealerScoreText;

    public Button hitButton;
    public Button stayButton;

    public Canvas resultCanvas;
    public Text resultText;
    public Text coinText;

    public Button playAgainButton;
    public Button exitButton;
    public Button playingButton;

    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;

    public Sprite[] cardSprites;
    public SpriteRenderer cardSpriteRenderer;

    void Start()
    {
        InitializeGame();
        game = new BlackjackGame();
        game.StartGame();

        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");

        SetCardImg();
        UpdateScore();
    }

    //Assets/Sprites/CuteCards.png

    void SetCardImg()
    {

        GameObject Hand = game.GetIsPlayerTurn()? playerHand.gameObject : dealerHand.gameObject;

        // Card 프리팹 인스턴스화 후 cardImage에 Image 컴포넌트 할당

        GameObject cardInstance = Instantiate(cardPrefab, Hand.transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = cardInstance.GetComponent<SpriteRenderer>();
        int idx = game.GetOnTurnPlayer().GetHand()[0].GetSourceIdx();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = cardSprites[idx];
            Debug.Log("Sprite Found: " + cardSprites[idx].name);
        }
        else
        {
            Debug.LogError("SpriteRenderer is null!");
        }
    }



    //void SetCardImg()
    //{
    //    string sourcePath = game.GetOnTurnPlayer().GetHand()[0].GetSourcePath();
    //    cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");
    //    Debug.Log(cardSprites.Length);
    //    Debug.Log(sourcePath);
    //    //foreach (Sprite sprite in cardSprites)
    //    //{
    //    //    Debug.Log(sprite.name);
    //    //}

    //    Sprite targetSprite = null;

    //    for (int i = 0; i < cardSprites.Length; i++)
    //    {
    //        if (cardSprites[i].name == sourcePath)
    //        {
    //            targetSprite = cardSprites[i];
    //            break;
    //        }
    //    }

    //    Debug.Log(cardSprites.Length);

    //    if (targetSprite == null)
    //    {
    //        Debug.Log("No Sprite Found");
    //    }
    //    else
    //    {
    //        cardImage.sprite = targetSprite;
    //    }
    //}

    void ShowCard()
    {
        GameObject card = Instantiate(cardPrefab, playerHand);

        SpriteRenderer cardSpriteRenderer = card.GetComponent<SpriteRenderer>();
        if(cardSpriteRenderer != null)
        {
            cardSpriteRenderer.sprite = cardSprites[0];
        }
    }

    //private void Update()
    //{
    //    if (game.GetIsPlayerTurn())
    //    {
    //        hitButton.interactable = true;
    //        stayButton.interactable = true;
    //    }
    //    else
    //    {
    //        hitButton.interactable = false;
    //        stayButton.interactable = false;
    //    }
    //}

   

    void UpdateScore()
    {
        playerScoreText.text = "Player Score: " + game.GetPlayerScore();
        dealerScoreText.text = "Dealer Score: " + game.GetDealerScore();
    }

   

    void CheckResult()
    {
        UpdateScore();
        game.CheckWinner(game.GetPlayerScore(), game.GetDealerScore());

        Enums.GameResult _result = game._result;

        resultText.text = "";

        switch (_result)
        {
            case Enums.GameResult.PlayerWin:
                resultText.text = "You win!!!";
                break;
            case Enums.GameResult.DealerWin:
                resultText.text = "You lose....";
                break;
            case Enums.GameResult.Push:
                resultText.text = "Push, You can't get any coins ";
                break;
            case Enums.GameResult.PlayerBlackjack:
                resultText.text = "Congratulations, You got Blackjack!";
                break;
            case Enums.GameResult.DealerBlackjack:
                resultText.text = "Oh.. Dealer got Blackjack...";
                break;
            case Enums.GameResult.PlayerBust:
                resultText.text = "Hmm..., Let's not get carried away ";
                break;
            case Enums.GameResult.DealerBust:
                resultText.text = "So Luckly, Dealer were so greedy.";
                break;
            default:
                resultText.text = "Let's play!";
                break;
        }

        Debug.Log("Result: " + resultText.text);
    }

    void OnHit()
    {
        Card card = game.Hit();
        Debug.Log("Hit: " + card.GetSuit() + " " + card.GetValue());
        CheckResult();

    }

    void OnStay()
    {   
        game.ChangeTurn();

        hitButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);

        Debug.Log("Stay");
        while (game.GetDealerScore() < 17)
        {
            Card card = game.Hit();
            Debug.Log("Dealer Hit: " + card.GetSuit() + " " + card.GetValue());
        }
        CheckResult();

        //game.ChangeTurn();
    }

    public void InitializeGame()
    {
        //menuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        resultCanvas.gameObject.SetActive(false);

        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);

        //playAgainButton.onClick.AddListener(ResetGame);
        //exitButton.onClick.AddListener(EndGame);
        //playingButton.onClick.AddListener(ShowResult);


    }

    public void ShowResult()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        resultCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
        //coinText.text = "Coin: " + game.GetCoin();

    }

    public void ResetGame()
    {
        game.StartGame();
        CheckResult();
    }

}
