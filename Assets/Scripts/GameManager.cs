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
        game.OnCardDealt += SetCardImg;
        game.StartGame();
        UpdateScore();

    }

    //Assets/Sprites/CuteCards.png

    public void SetCardImg(bool isPlayer, int cardIndex)
    {

        Debug.Log("SetCardImg called for " + (isPlayer ? "Player" : "Dealer") + " with cardIndex: " + cardIndex);
        GameObject Hand = isPlayer ? playerHand.gameObject : dealerHand.gameObject;

        // 카드가 나란히 배치되도록 offset 설정
        float offsetX = 1.0f; // 카드 간의 간격 (원하는 간격으로 조정 가능)
        Vector3 cardPosition = Hand.transform.position + new Vector3(cardIndex * offsetX, 0, 0);

        // 카드 생성 및 위치 설정
        GameObject cardInstance = Instantiate(cardPrefab, cardPosition, Quaternion.identity);
        SpriteRenderer spriteRenderer = cardInstance.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Player player = isPlayer ? game.GetPlayer() : game.GetDealer();
            int idx = player.GetHand()[cardIndex].GetSourceIdx();

            if (idx >= 0 && idx < cardSprites.Length)
            {
                spriteRenderer.sprite = cardSprites[idx];
                Debug.Log("Sprite Found: " + cardSprites[idx].name);
            }
            else
            {
                Debug.LogError("Index " + idx + " is out of bounds for cardSprites array.");
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer is null!");
        }

        Canvas.ForceUpdateCanvases();
    }


    public void UpdateScore()
    {
        playerScoreText.text = "Player Score: " + game.GetPlayerScore();
        dealerScoreText.text = "Dealer Score: " + game.GetDealerScore();
    }

   

    public void CheckResult()
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

    public void OnHit()
    {
        Debug.Log("Hit");

        Card card = game.Hit();
        Debug.Log("Hit: " + card.GetSuit() + " " + card.GetValue());
        CheckResult();

    }

    public void OnStay()
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

        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");

        hitButton.onClick.AddListener(OnHit);
        stayButton.onClick.AddListener(OnStay);


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
