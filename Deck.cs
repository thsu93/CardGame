using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Draggable prefabCard;
    public DropZone hand;
    void Start() 
    {
        Button b = this.GetComponent<Button>();
        b.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Draggable NewCard = Instantiate(prefabCard);
        Image CardColor = NewCard.GetComponent<Image>();
        CardColor.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        NewCard.CardType = (Draggable.Slot)Random.Range(0, 7);
        Transform CardName = NewCard.transform.Find("Name");
        Text NameText = CardName.GetComponent<Text>();
        NameText.text = NewCard.CardType.ToString();
        NewCard.transform.SetParent(hand.transform);
    }
}
