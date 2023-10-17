using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Slot slot;
    public List<Slot> slotList = new List<Slot>();
    public DeckPanel deckpanel;
    private void Start()
    {
        for(int i=0; i < gameObject.transform.childCount; i++)
        {
            Transform slotposition = gameObject.transform.GetChild(i);
            slot = slotposition.GetComponent<Slot>();
            slotList.Add(slot);
        }
        
    }
    private void Update()
    {
        SlotClick();
    }
    private void SlotClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                slot = hit.transform.GetComponent<Slot>();
                if (slot.dice != null)
                {
                    if(deckpanel.deckdata.diceList.Count<15)
                    {
                        deckpanel.AddDice(slot.dice);
                    }
                }
            }
        }
    }
}

