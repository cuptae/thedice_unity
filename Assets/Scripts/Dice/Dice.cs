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
    public Sprite diceIcon;

    [SerializeField]
    public int IDNUM;
    [SerializeField]
    public int DiceID;
    [SerializeField]
    private int Rank;
    [SerializeField]
    public int[] Mark;
    [SerializeField]
    private Sprite[] diceside;
    [SerializeField]
    private Transform deck;

    [SerializeField]
    private Player player;

    public int dicenum;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        List<Dictionary<string, object>> csvData = CSVReader.Read("Dice");
        Mark = new int[6];
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
    public void Start()
    {
        if (transform.parent != null && transform.parent.name == "Player01_Deck")
        {
            deck = GameObject.Find("Player01_Deck").transform;
            GameObject playerObject = GameObject.Find("Player01");
            if (playerObject != null)
            {               
                player = playerObject.GetComponent<Player>();
            }
        }
        else if (transform.parent != null && transform.parent.name == "Player02_Deck")
        {
            deck = GameObject.Find("Player02_Deck").transform;
            GameObject playerObject = GameObject.Find("Player02");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
            }
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Roll()
    {
        dicenum = Random.Range(0, 6);
        spriteRenderer.sprite = diceside[Mark[dicenum]-1];
        Effect(dicenum);
        destroy();
    }
    void Effect(int dicenum)
    {
        if (Mark[dicenum] == (int)DiceType.Attack)
        {
            player.Attack();
            Debug.Log("Attack");
        }
        else if (Mark[dicenum] == (int)DiceType.Shield)
        {
            player.Shield_UP();
            Debug.Log("Shield UP");
        }
        else if (Mark[dicenum] == (int)DiceType.Bomb)
        {
            player.SelfDamaged();
            Debug.Log("Bomb");
        }
        else if (Mark[dicenum] == (int)DiceType.Reroll)
        {
            Invoke("ReRoll", 1.0f);
            Debug.Log("Reroll");
        }
    }
    void ReRoll()
    {
        spriteRenderer.sprite = null;
        transform.SetParent(deck.transform);
        transform.position = deck.position;
    }
    public void destroy()
    {
        if (Mark[dicenum] != (int)DiceType.Reroll)
        {
            Destroy(gameObject, 1.0f);
        }
    }
    public SpriteRenderer GetSpriteRenderer() { return spriteRenderer; }
}