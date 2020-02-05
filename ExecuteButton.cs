using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecuteButton : MonoBehaviour
{
    GameManager Calculator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Manager = GameObject.Find("GameManager");
        Calculator = Manager.GetComponent<GameManager>();
        Button b = this.GetComponent<Button>();
        b.onClick.AddListener(Execute);
    }

    void Execute()
    {
        Calculator.ExecuteTurn();
        Destroy(this.gameObject);
    }
}
