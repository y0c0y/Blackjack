using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private List<Card> cards;
    private int cardIdx;
    private bool isUsing;

    public Deck()
    {
        cards = new List<Card>();
        cardIdx = 0;
        isUsing = false;
    }

    // Getters and utility methods
    public int GetCardIdx() => cardIdx;
    public List<Card> GetCards() => cards;
    public void ChangeIsUsing() => this.isUsing = !isUsing;
    public bool GetIsUsing() => isUsing;

    // Initialize the deck with 6 full decks (312 cards)
    public void InitializeDeck()
    {
        List<Card> singleDeck = BuildSingleDeck();

        // Add 6 decks to the cards list
        for (int i = 0; i < 6; i++)
        {
            cards.AddRange(singleDeck);
        }
    }

    // Helper method to build a single deck (52 cards)
    private List<Card> BuildSingleDeck()
    {
        List<Card> singleDeck = new List<Card>();

        foreach (Enums.Suit suit in Enum.GetValues(typeof(Enums.Suit)))
        {
            int idx = (int)suit;
            foreach (Enums.Value value in Enum.GetValues(typeof(Enums.Value)))
            {
                Card card = new Card(suit, value, idx);
                singleDeck.Add(card);
                idx++;
            }
        }

        return singleDeck;
    }

    // Reset the deck index and shuffle
    public void DeckReset()
    {
        cardIdx = 0;
        isUsing = true;
        Shuffle();
    }

    // Modern Fisher-Yates shuffle
    private void Shuffle()
    {
        System.Random rand = new System.Random();
        int n = cards.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1); // Generate random index
            SwapCards(i, j); // Swap card positions
        }
    }

    // Helper method to swap two cards in the deck
    private void SwapCards(int i, int j)
    {
        Card temp = cards[i];
        cards[i] = cards[j];
        cards[j] = temp;
    }

    // Deal a card from the deck
    public Card DealCard()
    {
        if (cardIdx < cards.Count)
        {
            Card dealtCard = cards[cardIdx];
            cardIdx++;

            // Warn if the deck is running low
            if (cardIdx >= cards.Count * 0.8 && isUsing)
            {
                Debug.Log("Last Game!!");
            }

            return dealtCard;
        }
        else
        {
            Debug.Log("No more cards!!");
            return null;
        }
    }
}
