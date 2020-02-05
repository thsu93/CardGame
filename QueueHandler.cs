using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Convert dropped Cards to QueueCards.  
//QueueCards cannot be rearranged, can be clicked to return to hand as a card.
//When queue is filled, will lock out new cards.

//To Do:
//Revisit how to handle dropzone vs AP logic 
//clean up the button creation code, decide where to place button

public class QueueHandler : MonoBehaviour
{

    DropZone queueDrop;
    public int MaxAP;
    public int CurrentAP = 0;
    public int CurrentAttackVal = 0;
    public int CurrentDefenseVal = 0;
    bool disabled = false;

    private void Start() 
    {
        MaxAP = 10;
        queueDrop = this.GetComponent<DropZone>();
    }

    public void Reset()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        CurrentAttackVal = 0;
        CurrentDefenseVal = 0;
        CurrentAP = 0;
        disabled = false;
    }

    public void DisableQueue()
    {
        disabled = true;
        foreach (Transform child in this.transform)
        {
            QueueCard tempCard = child.GetComponent<QueueCard>();
            if (tempCard != null)
            {
                tempCard.returnButton.enabled = false;
            }
        }
    }

    private void Update() 
    {
        if (!disabled)
        {
            CheckDroppable();
            CheckForNewCards();
            if (this.transform.childCount > 0)
            {
                TurnOnReturnButton();
            }
        }
    }

    void CheckDroppable()
    {
        queueDrop.droppable = !(CurrentAP>MaxAP);
    }

    //see if zone is considered droppable for a currently selected card
    public bool CheckIfDroppable(int CardAP)
    {
        return !(CurrentAP+CardAP > MaxAP);
    }

    //check for if a card has been dropped into the queue
    void CheckForNewCards()
    {
        for (int i = 0; i<this.transform.childCount; i++)
        {
            Card tempCard = this.transform.GetChild(i).GetComponent<Card>();
            if (tempCard != null && tempCard.APCost+CurrentAP<=MaxAP)
            {
                QueueCard NewQueue = Instantiate(tempCard.matchingQueueCard);
                NewQueue.transform.SetParent(this.transform);
                NewQueue.transform.SetAsLastSibling();
                Destroy(tempCard.gameObject);
                this.transform.GetComponent<DropZone>().currentSize--;
            }
        }
    }

    //Only the last item in the queue can be popped
    void TurnOnReturnButton()
    {
        int lastindex = this.transform.childCount-1;   
        for (int i = 0; i<this.transform.childCount; i++)
        {
            QueueCard tempCard = this.transform.GetChild(i).GetComponent<QueueCard>();
            if (tempCard != null)
            {
                tempCard.returnButton.enabled = (i==lastindex);
            }
        }
    }



}
