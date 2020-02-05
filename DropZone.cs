using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Handles zones that can have cards dropped into them
//Accepts types of cards 

//To Do:
//Determine how to handle current/max size in hand vs queue, etc. 
//fix droppable - is handled here for queue, but in hand for hand

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image self;
    public Color NormalColor;
    public bool droppable;
    public int currentSize;
    public bool isQueue = false;
    void Start()
    {
        self = GetComponent<Image>();
        NormalColor= self.color;
        currentSize = 0;
        droppable = true;
    }
    private void Update() 
    {
        currentSize = this.transform.childCount;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Card d = eventData.pointerDrag.GetComponent<Card>();
        if (isQueue)
        {
            droppable = this.GetComponent<QueueHandler>().CheckIfDroppable(d.APCost);
        }
        if (d != null && droppable)
        {
            d.initialParent = this.transform;
        }
    }
}
