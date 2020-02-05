using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI Box in screen to display in-game messages

//To Do:
//Visual improvement necessary. 
//Setting Alpha likely not the proper way to do this.

public class ErrorBox : MonoBehaviour
{
    Text textfield = null;
    CanvasRenderer Drawer;
    const float blinktime = 3.0f;
    const float blinklength = .5f;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        textfield = this.GetComponent<Text>();
        Drawer = this.GetComponent<CanvasRenderer>();
        Drawer.SetAlpha(0);
    }

    // Update is called once per frame
    public void DisplayError(string DisplayText)
    {
        textfield.text = DisplayText;
        textfield.fontSize = 100;
        textfield.color = Color.red;
        Drawer.SetAlpha(1);
        if (!isRunning)
        {
            StartCoroutine("Blink");
        }
    }

    IEnumerator Blink()
    {
        isRunning = true;
        float timer = 0.0f;
        while (timer<blinktime)
        {
            textfield.enabled = !textfield.enabled;
            yield return new WaitForSeconds(blinklength);
            timer+=blinklength;
        }
        textfield.enabled=false;
        isRunning = false;
        Drawer.SetAlpha(0);
    }
}
