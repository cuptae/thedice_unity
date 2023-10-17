using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    bool hasDice;
    public Text DiceName;
    public Dice dice;
    public Image DiceIcon;

    private void Start()
    {
        DiceName.text = dice.transform.name;
        DiceIcon.sprite = dice.diceIcon;
    }
}
