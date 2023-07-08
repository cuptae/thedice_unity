using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private string cardName;

    public void SetName(string name)
    {
        cardName = name;
    }

    public string GetName()
    {
        return cardName;
    }
}
