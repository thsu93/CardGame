using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform initialParent = null;
    public DropZone initialZone = null;
    
    //for card movement, hold empty spaces for cards when moving in hand. 
    private float xOffset = 0;
    private float yOffset = 0;
    GameObject placeholder = null;

    //Card descriptors
    public string CardName;
    public GameObject NameHolder;
    public int APCost;
    public GameObject APCostHolder;
    public int Value;
    public GameObject DescriptionHolder;
    public enum Slot {Attack, Magic, Defend, Any, NUM_TYPES};
    public Slot CardType = Slot.Any;

    public QueueCard matchingQueueCard;

    private void Start() 
    {
        Text NameField = NameHolder.GetComponent<Text>();
        NameField.text = CardName;
        Text APField = APCostHolder.GetComponent<Text>();
        APField.text = APCost.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //remember which panel card came from
        initialParent = this.transform.parent;
        initialZone = initialParent.GetComponent<DropZone>();
        initialZone.currentSize--;

        //create an invisible card to hold current card's place
        placeholder = new GameObject();
        placeholder.transform.SetParent (this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        //correct for non-centered clicks
        xOffset = this.transform.position.x - eventData.position.x;
        yOffset = this.transform.position.y - eventData.position.y;
        this.transform.SetParent(this.transform.parent.parent);
        
        //allow for click-through on card
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //change drop zone colors
        DropZone[] zones = GameObject.FindObjectsOfType<DropZone>();
        for (int i=0; i<zones.Length; i++)
        {
            if (zones[i].droppable)
            {
                Color tempColor = zones[i].NormalColor;
                zones[i].self.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0.9f);   
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //move the card
        this.transform.position = new Vector2(eventData.position.x + xOffset, eventData.position.y + yOffset);

        //check order of where selected card is relative to other cards 
        int placeholderIndex = initialParent.childCount;
        for (int i = 0; i < initialParent.childCount; i++)
        {
            if (this.transform.position.x < initialParent.GetChild(i).position.x)
            {
                placeholderIndex = i;
                if(placeholder.transform.GetSiblingIndex() < i)
                {
                    placeholderIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(placeholderIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //send card back to panel.  
        //Activates after OnDrop, so drop in other zone has higher priority
        this.transform.SetParent(initialParent);

        //re-allow card clicks
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        
        //correct colors
        DropZone[] zones = GameObject.FindObjectsOfType<DropZone>();
        for (int i=0; i<zones.Length; i++)
        {
            if (zones[i].droppable)
            {
                zones[i].self.color = zones[i].NormalColor;
            }
        }

        //move card to replace placeholder
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder);

        initialZone.currentSize++;
    }
}
