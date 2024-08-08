using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{

    public List<Card> cards;
    public int cardIdx;
    public bool isUsing;
    void Start()
    {
        InitializeDeck();
        DeckReset();
    }

    void InitializeDeck()
    {
        cards = new List<Card>();

        List<Card> tmp_deck = new List<Card>();

        foreach (Card.Suit suit in System.Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Value value in System.Enum.GetValues(typeof(Card.Value)))
            {
                Card card = new Card();
                card.suit = suit;
                card.value = value;
                tmp_deck.Add(card);
            }
        }

        for (int i = 0; i < 6; i++)
        {
            cards.AddRange(tmp_deck);
        }
    }

    public void DeckReset()
    {
        cardIdx = 0;
        isUsing = true;
        Shuffle(ref cards);
    }

   public void Shuffle(ref List<Card> cards)// Mordern Fisher-Yates shuffle
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

        if (cards.Count * 0.8 >  cardIdx || isUsing)
        {
            Card card = cards[cardIdx];
            cardIdx++;
            return card;
        }

        return null;
    }

}
