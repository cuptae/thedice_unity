using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_Test : MonoBehaviour
{
    [SerializeField]
    private List<Card> cardList = new List<Card>();
    [SerializeField]
    private Transform[] cardPositions;

    private void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DrawCards(4);
    }

    private void InitializeDeck()
    {
        // 카드 이름 설정 (1, 2, 3, 4, ...)
        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].SetName((i + 1).ToString());
        }
    }

    private void ShuffleDeck()
    {
        System.Random random = new System.Random();
        int n = cardList.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card value = cardList[k];
            cardList[k] = cardList[n];
            cardList[n] = value;
        }
    }

    private void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card card = Instantiate(cardList[i], cardPositions[i]).GetComponent<Card>();
            Debug.Log("Card Name: " + card.GetName());
        }
    }
}
