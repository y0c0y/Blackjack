using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;
    public Transform deckPosition; 

    public Sprite[] cardSprites;

    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");
        playerHand = GameObject.Find("PlayerHand").transform;
        dealerHand = GameObject.Find("DealerHand").transform;
        deckPosition = GameObject.Find("DeckPosition").transform;
    }

    public void SetCardImg(bool isPlayer, int cardIndex, int cardSpriteIndex, bool openDealerCard)
    {
        Transform hand = isPlayer ? playerHand : dealerHand;
        float offsetX = 1.0f;
        Vector3 finalPosition = hand.position + new Vector3(cardIndex * offsetX, 0, 0);

        // Special case: if it's the dealer's second card and openDealerCard is true, place it directly
        if (!isPlayer && cardIndex == 1 && openDealerCard)
        {
            GameObject cardInstance = Instantiate(cardPrefab, finalPosition, Quaternion.identity, hand);
            SetCardSprite(cardInstance, cardSpriteIndex);
        }
        else            // Normal case: Instantiate the card at the deck position (starting point for the animation)
        {
            GameObject cardInstance = Instantiate(cardPrefab, deckPosition.position, Quaternion.identity, hand);
            SetCardSprite(cardInstance, 44);
            StartCoroutine(AnimateCardPlacement(cardInstance, finalPosition, cardSpriteIndex));
        }
    }

    private void SetCardSprite(GameObject cardInstance, int cardSpriteIndex)
    {
        SpriteRenderer spriteRenderer = cardInstance.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (cardSpriteIndex >= 0 && cardSpriteIndex < cardSprites.Length)
            {
                spriteRenderer.sprite = cardSprites[cardSpriteIndex];
            }
            else
            {
                Debug.LogError("Index " + cardSpriteIndex + " is out of bounds for cardSprites array.");
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer is null!");
        }
    }

    private IEnumerator AnimateCardPlacement(GameObject card, Vector3 finalPosition, int cardSpriteIndex)
    {
        float animationDuration = 0.5f;
        float elapsedTime = 0f;

        Vector3 startPosition = card.transform.position;

        while (elapsedTime < animationDuration)
        {
            card.transform.position = Vector3.Lerp(startPosition, finalPosition, elapsedTime / animationDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        card.transform.position = finalPosition;

        SetCardSprite(card, cardSpriteIndex);
    }

    public void ClearHands(Transform handTransform)
    {
        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
    }
}
