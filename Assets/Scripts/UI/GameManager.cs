using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region Field
    private int turn = 1;
    #endregion

    #region SerializeField
         #region text
    [SerializeField]
    private Text Turn;
    [SerializeField]
    private Text Player01_HP_txt;
    [SerializeField]
    private Text Player02_HP_txt;
    [SerializeField]
    private Text Player01_Shield_txt;
    [SerializeField]
    private Text Player02_Shield_txt;
    [SerializeField]
    private Text Player01_Vic_txt;
    [SerializeField]
    private Text Player02_Vic_txt;
    [SerializeField]
    private Text Victory_txt;
    [SerializeField]
    private Text Defeat_text;
    #endregion text
         #region button
    [SerializeField]
    private Button Player01_Rolling_Button;
    [SerializeField]
    private Button Player02_Rolling_Button;
    [SerializeField]
    private Button Player01_Rest_Button;
    [SerializeField]
    private Button Player02_Rest_Button;
    [SerializeField]
    private Button Player01_PowerRoll_Button;
    [SerializeField]
    private Button Player02_PowerRoll_Button;
    #endregion
    [SerializeField]
    private Player Player01;
    [SerializeField]
    private Player Player02;
    #endregion

    #region ½Ì±ÛÅæ
    static public GameManager instance;
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }
    #endregion

    private void Start()
    {
        Player01.Turn = true;
        Player02.Turn = false;
        Player02_Rolling_Button.interactable = false;
        Player02_Rest_Button.interactable = false;
        Player02_PowerRoll_Button.interactable = false;
    }

    private void Update()
    {
        Turn.text = $"Turn: {turn}";
        Player01_HP_txt.text = $"HP: {Player01.Hp}";
        Player02_HP_txt.text = $"HP: {Player02.Hp}";
        Player01_Shield_txt.text = $"Shield : {Player01.Shield}";
        Player02_Shield_txt.text = $"Shield : {Player02.Shield}";
        if(Player02.Turn==true)
        {
            Player02turn();
        }
        GameOver();
    }
    #region Damaged_Method
    public void Damage()
    { 
        if (Player01.Turn&&!Player02.Turn)
        {
            if (Player02.Shield>0)
            {Player02.Shield -= 1;}
            else
            {Player02.Hp -= 1;}
        }
        else if(Player02.Turn&&!Player01.Turn)
        {
            if (Player01.Shield > 0)
            {Player01.Shield -= 1;}
            else
            {Player01.Hp -= 1;}     
        }   
    }

    public void Shield_Damage()
    {
        if (Player01.Turn && !Player02.Turn)
        { Player02.Shield -= 1; }
        else if(Player02.Turn && !Player01.Turn)
        { Player01.Shield -= 1;}
    }
    public void Shield_UP()
    {
        if (Player01.Turn && !Player02.Turn)
        { Player01.Shield += 1;}
        else if(Player02.Turn && !Player01.Turn)
        { Player02.Shield += 1;}
    }
    public void SelfDamaged()
    {
        if (Player01.Turn && !Player02.Turn)
        {
            if (Player01.Shield > 0)
            { Player01.Shield -= 1; }
            else
            { Player01.Hp -= 1; }  
        }
        else if(Player02.Turn && !Player01.Turn)
        {
            if (Player02.Shield > 0)
            { Player02.Shield -= 1; }    
            else
            { Player02.Hp -= 1; }   
        }
    }
    #endregion
    void Player02turn()
    {
        int randomChoice = UnityEngine.Random.Range(0, 2);
        if (randomChoice == 0)
        {
            Player02_Rolling_Button.onClick.Invoke();
            Debug.Log("Player02 : Roll");
        }
        else
        {
            Player02_Rest_Button.onClick.Invoke();
            Debug.Log("Player02 : Rest");
        }
        StartCoroutine(trunover1());
    }


    public void TurnOver()
    {
        if (Player01.Turn && !Player02.Turn)
        {
            Player01.Turn = false;
            Player02.Turn = true;
        }
        else if (Player02.Turn && !Player01.Turn)
        {
            Player02.Turn = false;
            Player01.Turn = true;
        }
        StartCoroutine(ButtonControll());
    }

    public void GameOver()
    {
         if(Player01.Hp<=0||Player02.Hp<=0||turn>=15)
         {
             if (Player01.Hp < Player02.Hp)
             {
                 Player02_Vic_txt.text = "Victory";
                 Player01_Vic_txt.text = "Defeat";
             }
             else if (Player02.Hp < Player01.Hp)
             {
                 Player01_Vic_txt.text = "Victory";
                 Player02_Vic_txt.text = "Defeat";
             }
         }
    }

    IEnumerator ButtonControll()
    {
        yield return new WaitForSeconds(1.0f);
        if (Player01.Turn)
        {
            Player01_Rolling_Button.interactable = true;
            Player02_Rolling_Button.interactable = false;
            Player01_Rest_Button.interactable = true;
            Player02_Rest_Button.interactable = false;
            Player01_PowerRoll_Button.interactable = true;
            Player02_PowerRoll_Button.interactable = false;
        }
        else if(Player02.Turn)
        {
            Player01_Rolling_Button.interactable = false;
            Player02_Rolling_Button.interactable = true;
            Player01_Rest_Button.interactable = false;
            Player02_Rest_Button.interactable = true;
            Player01_PowerRoll_Button.interactable = false;
            Player02_PowerRoll_Button.interactable = true;
        }
    }

    IEnumerator trunover1()
    {
        yield return new WaitForSeconds(1.0f);
        turn++;
    }
}