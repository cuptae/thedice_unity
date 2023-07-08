using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    [SerializeField]
    public List<Dice> diceList = new List<Dice>();
    [SerializeField]
    private Transform deck_position;
    [SerializeField]
    public  Transform board;

    private Dice dice;

    private void Start()
    {
        for (int i = 0; i < deck_position.childCount; i++)
        {
            Transform diceTransform = deck_position.GetChild(i);
            dice = diceTransform.GetComponent<Dice>();
            diceList.Add(dice);
        }
        //Shuffle();
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