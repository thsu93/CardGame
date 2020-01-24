using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image self;
    public Color NormalColor;
    public Draggable.Slot CardType = Draggable.Slot.Weapon;

    void Start()
    {
        self = GetComponent<Image>();
        NormalColor= self.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null && (d.CardType == CardType || d.CardType == Draggable.Slot.Any 
            || CardType == Draggable.Slot.Any))
        {
            d.initialParent = this.transform;
        }
    }
}
