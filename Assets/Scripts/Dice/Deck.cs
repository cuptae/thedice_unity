using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    [SerializeField]
    public List<Dice> diceList = new List<Dice>();
    [SerializeField]
    private Transform deck_position;
    [SerializeField]
    private DeckData deckdata;

    private List<Dice> DeckList = new List<Dice>();
    private Dice dice;
    private void Start()
    {
        DeckList = deckdata.diceList;
        for (int i = 0; i < 15; i++)
        {
            dice = Instantiate(DeckList[i], deck_position);
            diceList.Add(dice);
        }
        Shuffle();
    }
    private void Shuffle()
    {
        System.Random random = new System.Random();
        int n = diceList.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Dice value = diceList[k];
            diceList[k] = diceList[n];
            diceList[n] = value;
        }
    }
}