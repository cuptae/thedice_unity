using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Hp = 15;
    public int Shield = 0;
    public int Damage = 1;
    
    bool Rest=false;
    bool Power=false;

    public bool ChoiceRoll=false;
    public bool ChoiceRest=false;
    public bool ChoicePower=false;
    public bool ischoice=false;

    private int Rolling_Count;
    private Dice dice;

    [SerializeField]
    private Deck deck;
    [SerializeField]
    private Transform board;
    [SerializeField]
    private Player OtherPlayer;

    [SerializeField]
    public Button Rolling_Button;
    [SerializeField]
    public Button Power_Button;
    [SerializeField]
    public Button Rest_Button;
    private void Start()
    {
        Rolling_Count = 4;
        DeckEquip();
    }

    public void Rolling()
    {
        DisableAllButtons(false);
        ischoice = true;
        ChoiceRoll= true;
        
    }
    public void PowerRoll()
    {
        DisableAllButtons(false);
        ischoice = true;
        ChoicePower= true;
    }
    public void RestTurn()
    {
        DisableAllButtons(false);
        ischoice = true;
        ChoiceRest = true;
    }


    public void PlayerAction()
    {
        if(ChoiceRoll == true)
        {
            for (int i = 0; i < Rolling_Count; i++)
            {
                deck.diceList[i].transform.SetParent(board.transform);
                deck.diceList[i].transform.localPosition = Vector3.zero;
                deck.diceList[i].Roll();
                dice = deck.diceList[i];
                if (dice.Mark[dice.dicenum] == (int)DiceType.Reroll)
                {
                    deck.diceList.Add(dice);
                }
            }
            deck.diceList.RemoveRange(0, Rolling_Count);
            ChoiceRoll = false;
        }
        if(ChoicePower == true)
        {
            ++Rolling_Count;
            if (Power == false)
            {
                for (int i = 0; i < Rolling_Count; i++)
                {
                    deck.diceList[i].transform.SetParent(board.transform);
                    deck.diceList[i].transform.localPosition = Vector3.zero;
                    deck.diceList[i].Roll();
                    dice = deck.diceList[i];
                    if (dice.Mark[dice.dicenum] == (int)DiceType.Reroll)
                    {
                        deck.diceList.Add(dice);
                    }
                }
                deck.diceList.RemoveRange(0, Rolling_Count);
                Power = true;
            }
            else if (Power == true)
            {
                Debug.Log("지난 턴에 파워롤을 이미 했습니다!");
            }
            Rolling_Count--;
            ChoicePower= false;
        }
        if(ChoiceRest == true)
        {
            if (Rest == true)
            {
                Debug.Log("지난턴에 쉬었습니다");
            }
            else if (Rest == false)
            {
                Rest = true;
                Rolling_Count++;
            }
            ChoiceRest=false;
        }
        ischoice= false;
    }

    public void Attack()
    {
        if (OtherPlayer.Shield >= 1)
        {
            OtherPlayer.Shield -= 1;
        }
        else
        {
            OtherPlayer.Hp -= 1;
        }
    }
    public void Shield_UP()
    {
        Shield += 1;
    }
    public void SelfDamaged()
    {
        if (Shield >= 1)
        {
            Shield -= 1;
        }
        else
        {
            Hp -= 1;
        }
    }

    public void DisableAllButtons(bool isDisable)
    {
        Rolling_Button.interactable = isDisable;
        Power_Button.interactable = isDisable;
        Rest_Button.interactable = isDisable;
    }


    private void DeckEquip()
    {
        if(gameObject.name=="Player01")
        {
            GameObject deckObject = GameObject.FindWithTag("Player01_Deck");

            if (deckObject != null)
            {
                deck = deckObject.GetComponent<Deck>();
            }
        }
        if (gameObject.name == "Player02")
        {
            GameObject deckObject = GameObject.FindWithTag("Player02_Deck");

            if (deckObject != null)
            {
                deck = deckObject.GetComponent<Deck>();
            }
        }

    }
}


