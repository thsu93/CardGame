using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Deck logic
//Deals cards out to hand
//Error handling -- don't use strings for errors?

public class Deck : MonoBehaviour
{
    public GameObject PlayerHand;
    GameManager Manager;
    DropZone handDrop;
    Hand hand;
    public int numAttack = 10;
    public int numDefense = 10;
    public int numMagic = 5;
    public List<Card> cardtypes;
    List<Card> fullDeck = new List<Card>();
    List<Card> currentDeck = null;
    UIController UIDisplay = null;
    public bool isDealt;

    void Start() 
    {
        //Create Deck Button
        Button b = this.GetComponent<Button>();
        b.onClick.AddListener(FullDeal);
        GameObject ManagerHolder = GameObject.Find("GameManager");
        Manager = ManagerHolder.GetComponent<GameManager>();
        
        UIDisplay = this.transform.parent.Find("UI").GetComponent<UIController>();      

        handDrop = PlayerHand.GetComponent<DropZone>();
        hand = PlayerHand.GetComponent<Hand>();

        //create deck
        for (int i = 0; i<numAttack; i++)
        {
            fullDeck.Add(cardtypes[0]);
        }
        for (int i = 0; i<numDefense; i++)
        {
            fullDeck.Add(cardtypes[1]);
        }
        for (int i = 0; i<numMagic; i++)
        {
            fullDeck.Add(cardtypes[2]);
        }
        currentDeck = new List<Card>(fullDeck);
        isDealt = false;
    }

    void FullDeal()
    {
        if (isDealt==false)
        {
            Manager.NewTurn();
            for (int i = handDrop.currentSize; i < hand.maxSize; i++)
            {
                Deal();
            }
        }
        else
        {
            UIDisplay.Error("Cards have been dealt for this turn");
        }
    }

    void Deal() //Deal
    {
        if (currentDeck.Count <= 0)
        {
            UIDisplay.Error("No Cards Left In Deck!");
        }
        else
        {
            Card NewCard = DealNewCard();
            NewCard.transform.SetParent(hand.transform);
            handDrop.currentSize++;
        }
    }

    Card DealNewCard()
    {
        int cardNumber = Random.Range(0, currentDeck.Count);
        Card NewCard = Instantiate(currentDeck[cardNumber]);
        currentDeck.RemoveAt(cardNumber);
        return NewCard;
    }

    public void Reset() 
    {
        currentDeck = new List<Card>(fullDeck);
        Debug.Log(fullDeck.Count);
        Debug.Log(currentDeck.Count);
    }


    // Outdated, do not use
    // Card GenerateNewCard()
    // {
    //     Card NewCard = Instantiate(prefabCards[0]);
    //     Image CardColor = NewCard.GetComponent<Image>();
    //     CardColor.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    //     NewCard.CardType = (Card.Slot)Random.Range(0, (int)Card.Slot.NUM_TYPES-1);
    //     RawImage CardPicture = NewCard.GetComponentInChildren<RawImage>();
    //     Transform CardName = NewCard.transform.Find("Name");
    //     Text NameText = CardName.GetComponent<Text>();
    //     NameText.text = NewCard.CardType.ToString();
    //     return NewCard;
    // }
}
