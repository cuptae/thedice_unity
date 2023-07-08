using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Hp = 15;
    public int Shield = 0;
    public int Damage = 1;
    public bool Rest;
    public bool Power;
    public bool Turn;
    public int Rolling_Count;

    [SerializeField]
    private Deck deck;

    private Dice dice;
    
    public void Rolling()
    {
        if (Turn==true)
        {
            if (Rest == true)
            { 
                Rolling_Count = 5;
            }
            else
            {
                Rolling_Count = 4;
            }
            if(deck.diceList.Count<=Rolling_Count)
            {
                Rolling_Count=deck.diceList.Count;
            }
            //레스트api,소켓 통신
            for (int i = 0; i < Rolling_Count; i++)
            {
                    deck.diceList[i].transform.SetParent(deck.board.transform);
                    deck.diceList[i].transform.localPosition = Vector3.zero;
                    deck.diceList[i].Roll();
                    dice = deck.diceList[i];
                if (dice.Mark[dice.dicenum] == (int)DiceType.Reroll)
                {
                    deck.diceList.Add(dice);
                }
            }
            deck.diceList.RemoveRange(0, Rolling_Count);
            Rest = false;
            Power = false;
        }
        GameManager.instance.TurnOver();
    }
    public void PowerRoll()
    {
        Rolling_Count = 5;
        if (Rest == true)
        {
            Rolling_Count = 6;
        }
        if (Power==false&&Turn==true)
        {
            for (int i = 0; i < Rolling_Count; i++)
            {
                    deck.diceList[i].transform.SetParent(deck.board.transform);
                    deck.diceList[i].transform.localPosition = Vector3.zero;
                    deck.diceList[i].Roll();
                    dice = deck.diceList[i];
                    Power = true;
                if (dice.Mark[dice.dicenum] == (int)DiceType.Reroll)
                {
                    deck.diceList.Add(dice);
                }
            }
            Debug.Log("남은 덱" + deck.diceList.Count);
            deck.diceList.RemoveRange(0, Rolling_Count);
        }
        else if(Power==true)
        {
            Debug.Log("지난 턴에 파워롤을 이미 했습니다!");
        }
        GameManager.instance.TurnOver();
    }
    public void RestTurn()
    {
        Rest = true;
        GameManager.instance.TurnOver();
    }
}


