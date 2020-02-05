using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To Do:
//Decide what to do with this

public class Hand : MonoBehaviour
{
    public int maxSize = 9;
    DropZone HandZone;
    // Start is called before the first frame update
    void Start()
    {
        HandZone = this.GetComponent<DropZone>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDroppable();
    }

    void CheckDroppable()
    {
        HandZone.droppable = !(HandZone.currentSize>maxSize);
    }

    public void Reset() 
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
