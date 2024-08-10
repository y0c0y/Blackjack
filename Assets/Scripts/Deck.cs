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

    public int GetCardIdx() => cardIdx;
    public List<Card> GetCards() => cards;
    public void ChangeIsUsing() => this.isUsing = !isUsing;
    public bool GetIsUsing() => isUsing;

    public void InitializeDeck() // Make 6 Decks
    {
        List<Card> tmp_deck = new List<Card>();

        foreach (Enums.Suit suit in System.Enum.GetValues(typeof(Enums.Suit)))
        {
            foreach (Enums.Value value in System.Enum.GetValues(typeof(Enums.Value)))
            {
                Card card = new Card(suit, value);
                tmp_deck.Add(card);
            }
        }

        for (int i = 0; i < 6; i++)
        {
            cards.AddRange(tmp_deck);
        }
    }

    public void DeckReset() // Still using cards, but reset cardIdx
    {
        cardIdx = 0;
        isUsing = true;
        Shuffle();
    }

    void Shuffle()// Mordern Fisher-Yates shuffle
    {
        System.Random rand = new System.Random();

        int cnt = cards.Count;
        int last = cnt - 2;

        for (int i = 0; i <= last; i++)
        {
            int r = rand.Next(i, cnt); //[i, cnt]
            Card tmp = cards[i];
            cards[i] = cards[r];
            cards[r] = tmp;
        }
    }

    public Card DealCard()
    {
        if (cards.Count * 0.8 > cardIdx)
        {
            Card card = cards[cardIdx];
            cardIdx++;
            return card;
        }
        else if (cards.Count * 0.8 <= cardIdx && isUsing)
        {
            Debug.Log("Last Game!!");

            Card card = cards[cardIdx];
            cardIdx++;
            return card;
        }
        else
        {
            Debug.Log("No more cards!!");
            return null;
        }
    }
}
