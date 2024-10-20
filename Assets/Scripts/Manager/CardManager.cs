using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;
    public Transform deckPosition;  // The starting point for cards

    public Sprite[] cardSprites;
    //public Sprite cardBackSprite;

    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");
    }

    // Set the card image for the player or dealer at the specified position with animation
    public void SetCardImg(bool isPlayer, int cardIndex, int cardSpriteIndex, bool openDealerCard)
    {
        Transform hand = isPlayer ? playerHand : dealerHand;

        // Calculate the final position for the card in the player's or dealer's hand
        float offsetX = 1.0f;
        Vector3 finalPosition = hand.position + new Vector3(cardIndex * offsetX, 0, 0);

        // Special case: if it's the dealer's second card and openDealerCard is true, place it directly
        if (!isPlayer && cardIndex == 1 && openDealerCard)
        {
            // Instantiate the card directly at the final position without animation
            GameObject cardInstance = Instantiate(cardPrefab, finalPosition, Quaternion.identity, hand);
            SetCardSprite(cardInstance, cardSpriteIndex);
        }
        else
        {
            // Normal case: Instantiate the card at the deck position (starting point for the animation)
            GameObject cardInstance = Instantiate(cardPrefab, deckPosition.position, Quaternion.identity, hand);
            SetCardSprite(cardInstance, 44);

            // Start the animation to move the card from the deck position to the final position in the hand
            StartCoroutine(AnimateCardPlacement(cardInstance, finalPosition, cardSpriteIndex));
        }
    }

    //private void SetCardBackSprite(GameObject cardInstance)
    //{
    //    SpriteRenderer spriteRenderer = cardInstance.GetComponent<SpriteRenderer>();
    //    if (spriteRenderer != null)
    //    {
    //        spriteRenderer.sprite = cardBackSprite;  // 카드의 뒷면을 설정
    //    }
    //}

    // Helper method to set the sprite for a card
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

    // Coroutine to animate the card placement
    private IEnumerator AnimateCardPlacement(GameObject card, Vector3 finalPosition, int cardSpriteIndex)
    {
        float animationDuration = 1.0f; // Animation duration in seconds
        float elapsedTime = 0f;

        Vector3 startPosition = card.transform.position;

        while (elapsedTime < animationDuration)
        {
            // Gradually move the card from the start position to the final position
            card.transform.position = Vector3.Lerp(startPosition, finalPosition, elapsedTime / animationDuration);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Ensure the card reaches the exact final position at the end of the animation
        card.transform.position = finalPosition;

        SetCardSprite(card, cardSpriteIndex);
    }

    // Clear all cards from the player's or dealer's hand
    public void ClearHands(Transform handTransform)
    {
        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
    }
}
