using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DiceType
{
    Attack=1,
    Shield=2,
    Bomb=3,
    Reroll=4
}
public class Dice : MonoBehaviour
{

    [SerializeField]
    private int IDNUM;
    [SerializeField]
    private int DiceID;
    [SerializeField]
    private int Rank;
    [SerializeField]
    public int[] Mark;
    [SerializeField]
    private Sprite[] Dice_side;
    [SerializeField]
    private Transform deck;

    //BoxCollider2D collider2D;

    public int dicenum;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
    }
    public void Start()
    {
        List<Dictionary<string, object>> csvData = CSVReader.Read("Dice");
        Mark = new int[6];
        if (transform.parent != null && transform.parent.name == "Player 01_Deck")
        {
            deck = GameObject.FindWithTag("Player01_Deck").transform;
        }
        else if(transform.parent != null && transform.parent.name == "Player 02_Deck")
        {
            deck = GameObject.FindWithTag("Player02_Deck").transform;
        } 
        spriteRenderer = GetComponent<SpriteRenderer>();
        //gameObject.name = csvData[IDNUM+1][0];
        #region CSV Data
        DiceID = (int)csvData[IDNUM]["DiceID"];
        Rank = (int)csvData[IDNUM]["Rank"];
        Mark[0] = (int)csvData[IDNUM]["Mark1"];
        Mark[1] = (int)csvData[IDNUM]["Mark2"];
        Mark[2] = (int)csvData[IDNUM]["Mark3"];
        Mark[3] = (int)csvData[IDNUM]["Mark4"];
        Mark[4] = (int)csvData[IDNUM]["Mark5"];
        Mark[5] = (int)csvData[IDNUM]["Mark6"];
        #endregion
    }
    public void Roll()
    {
        //collider2D.isTrigger = false;
        dicenum = Random.Range(0, 6);
        Sprite Selectsprite = Dice_side[dicenum];
        spriteRenderer.sprite = Selectsprite;
        Effect(dicenum);
        destroy();
        
    }
    void Effect(int dicenum)
    {
        if (Mark[dicenum] == (int)DiceType.Attack)
        {
            GameManager.instance.Damage();
           // Debug.Log("Attack");
        }
        else if (Mark[dicenum] == (int)DiceType.Shield)
        {
            GameManager.instance.Shield_UP();
            //Debug.Log("Shield UP");
        }
        else if (Mark[dicenum] == (int)DiceType.Bomb)
        {
            GameManager.instance.SelfDamaged();
            //Debug.Log("Bomb");
        }
        else if (Mark[dicenum] == (int)DiceType.Reroll)
        {
            //코루틴으로 다이스가 파괴될때 같이 덱으로 돌려 넣고 다이스 마크를 null로 바꾼다.
           // Debug.Log("Reroll");
            StartCoroutine(ReRoll());

        }
    }
    public void destroy()
    {
        if (Mark[dicenum] != (int)DiceType.Reroll)
        {
            Destroy(gameObject, 1.0f);
        }

    }
    IEnumerator ReRoll()
    {
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.sprite = null;
        transform.SetParent(deck.transform);
        transform.position = deck.position;
        //collider2D.isTrigger = true;
    }
}
