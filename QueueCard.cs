using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Representation of Card in the queue
//Can be clicked to return to hand if last item only

//To Do:
//None ATM

public class QueueCard : MonoBehaviour
{
    public Card matchingCard;
    int APCost; 
    int Value;
    Card.Slot CardType;
    public Button returnButton;
    QueueHandler Queue;

    // Start is called before the first frame update
    // Copy over Card internal values
    void Start()
    {
        Queue = GameObject.Find("Queue").GetComponent<QueueHandler>();

        //sync costs with card
        APCost = matchingCard.APCost;
        Value = matchingCard.Value;
        CardType = matchingCard.CardType;

        if (Queue != null)
        {
            Queue.CurrentAP += APCost;
            if (CardType == Card.Slot.Defend)
            {
                Queue.CurrentDefenseVal += Value;
            }
            else if (CardType == Card.Slot.Attack || CardType == Card.Slot.Magic)
            {
                Queue.CurrentAttackVal += Value;
            }
        }

        returnButton = this.GetComponent<Button>();
        returnButton.enabled = false;
        returnButton.onClick.AddListener(ReturnToHand);
    }

    // Update is called once per frame
    // Check if button is pressed; if so, return MatchingCard to hand and destroy self
    void ReturnToHand()
    {
        //find the player's hand for return
        GameObject hand = GameObject.Find("Hand");

        Card NewCard = Instantiate(matchingCard);
        NewCard.transform.SetParent(hand.transform);
        hand.GetComponent<DropZone>().currentSize++;

        if (Queue != null)
        {
            Queue.CurrentAP -= APCost;
            if (CardType == Card.Slot.Defend)
            {
                Queue.CurrentDefenseVal -= Value;
            }
            else if (CardType == Card.Slot.Attack || CardType == Card.Slot.Magic)
            {
                Queue.CurrentAttackVal -= Value;
            }
        }
        Destroy(this.gameObject);
    }
}
