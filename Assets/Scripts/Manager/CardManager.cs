using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;

    public Sprite[] cardSprites;
    public SpriteRenderer cardSpriteRenderer;

    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");

    }
    

    public void SetCardImg(bool isPlayer, int cardIndex, int cardSpriteIndex)
    {
        GameObject hand = isPlayer ? playerHand.gameObject : dealerHand.gameObject;

        float offsetX = 1.0f;
        Vector3 cardPosition = hand.transform.position + new Vector3(cardIndex * offsetX, 0, 0);

        GameObject cardInstance = Instantiate(cardPrefab, cardPosition, Quaternion.identity, hand.transform);
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

    public void ClearHands(Transform handTransform)
    {
        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
    }

}
