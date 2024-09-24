using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public Transform dealerHand;

    //Vector3 playerPosition;
    //Vector3 dealerPosition;

    public Sprite[] cardSprites;
    public SpriteRenderer cardSpriteRenderer;

    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Sprites/CuteCards");
        //playerPosition = playerHand.position;
        //dealerPosition = dealerHand.position;
    }

    public void SetCardImg(bool isPlayer, int cardIndex, int cardSpriteIndex)
    {
        GameObject hand = isPlayer ? playerHand.gameObject : dealerHand.gameObject;
        float offsetX = 1.0f;
        Vector3 cardPosition = hand.transform.position + new Vector3(cardIndex * offsetX, 0, 0);

        // 카드 생성 및 위치 설정 (Parenting the card to playerHand or dealerHand)
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

        //Vector3 initialPosition = handTransform == playerHand ? playerPosition : dealerPosition; 

        for (int i = handTransform.childCount - 1; i >= 0; i--)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }

        //handTransform.localPosition = initialPosition;
        //handTransform.localRotation = Quaternion.identity;
        //handTransform.localScale = Vector3.one;
    }

}
