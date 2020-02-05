using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trash for cards.  Possibly unnecessary.

//To Do:
// revisit.

public class Trash : MonoBehaviour
{
    DropZone trashZone;
    // Start is called before the first frame update
    void Start()
    {
        trashZone = this.transform.GetComponent<DropZone>();
    }

    // Update is called once per frame
    void Update()
    {
        //finds draggables, deletes them. 
        Card d = this.GetComponentInChildren<Card>();
        if (d != null)
        {
            Destroy(d.gameObject);
        }
        trashZone.currentSize=0;
    }
}
