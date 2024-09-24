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

    public Button startButton;
    public Button stopButton;
    public Button playingButton;
    public Button restartButton;
    public Button exitButton;

    public Canvas resultCanvas;
    public Text resultText;
    public Text coinText;

    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;

    Vector3 playerPosition;
    Vector3 dealerPosition;


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

        // 카드 생성 및 위치 설정 (Parenting the card to playerHand or dealerHand)
        GameObject cardInstance = Instantiate(cardPrefab, cardPosition, Quaternion.identity, Hand.transform);
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

        Enums.GameResult _result = game._result;

        Debug.Log("CheckResult: " + _result);

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
                resultText.text = "";
                break;
        }

        if(_result != Enums.GameResult.None)
        {
            ShowResult();
        }

        Debug.Log("Result: " + resultText.text);
    }

    public void OnHit()
    {
        Debug.Log("Hit");

        Card card = game.Hit();
        Debug.Log("Hit: " + card.GetSuit() + " " + card.GetValue());
        UpdateScore();

        if(game.GetPlayerScore() > 21)
        {
            CheckResult();
        }

    }

    public void OnStay()
    {   
        game.ChangeTurn();

        Debug.Log(game.GetIsPlayerTurn() ? "player" : "dealer");

        hitButton.gameObject.SetActive(false);
        stayButton.gameObject.SetActive(false);

        Debug.Log("Stay");

        OnDealerTurn();
        UpdateScore();
        CheckResult();


    }

    public void OnDoubleDown()
    {
        Debug.Log("Double Down");
    }

    public void OnSurrender()
    {
        Debug.Log("Surrender");
    }

    public void OnInsurance()
    {
        Debug.Log("Insurance");
    }

    public void OnSplit()
    {
        Debug.Log("Split");
    }

    public void OnDealerTurn()
    {
        Debug.Log("Dealer Turn");

        while (game.GetDealerScore() < 17)
        {
            Card card = game.Hit();
            Debug.Log("Dealer Hit: " + card.GetSuit() + " " + card.GetValue());
        }
    }

    public void InitializeGame()
    {
        menuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        resultCanvas.gameObject.SetActive(false);

        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);

        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");

        playerPosition = playerHand.position;
        dealerPosition = dealerHand.position;

        hitButton.onClick.AddListener(OnHit);
        stayButton.onClick.AddListener(OnStay);

        restartButton.onClick.AddListener(RestartGame);
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

        hitButton.gameObject.SetActive(true);
        stayButton.gameObject.SetActive(true);

        ClearHands(playerHand);
        ClearHands(dealerHand);

        ResetTransform(playerHand, playerPosition);
        ResetTransform(dealerHand, dealerPosition);


        game.InGame();
        game.OnCardDealt += SetCardImg;

        UpdateScore();
    }

    private void ResetTransform(Transform transformToReset, Vector3 initialPosition)
    {
        // Assuming the initial state is position = (0, 0, 0), rotation = (0, 0, 0), scale = (1, 1, 1)
        transformToReset.localPosition = initialPosition;   // Resets position to (0, 0, 0)
        transformToReset.localRotation = Quaternion.identity; // Resets rotation to (0, 0, 0)
        transformToReset.localScale = Vector3.one; // Resets scale to (1, 1, 1)
    }

    private void ClearHands(Transform handTransform)
    {
        // Loop through each child of the hand transform (cards) and destroy them
        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
    }

    public void EndGame()
    {
        inGameCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
        //coinText.text = "Coin: " + game.GetCoin();

    }

    

}
