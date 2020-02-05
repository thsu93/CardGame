using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//placeholder
//will need to revisit

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    ErrorBox ErrorDisplay;
    Text PlayerHP;
    Text EnemyHP;
    Text UpdateText;
    Text APText;
    Text TurnText;
    void Start()
    {
        Transform temp = this.transform.Find("ErrorBox");
        ErrorDisplay = temp.GetComponent<ErrorBox>();
        temp = this.transform.Find("PlayerHP");
        PlayerHP = temp.GetComponent<Text>();
        temp = this.transform.Find("EnemyHP");
        EnemyHP = temp.GetComponent<Text>();
        temp = this.transform.Find("UpdateText");
        UpdateText = temp.GetComponent<Text>(); 
        temp = this.transform.Find("APText");
        APText = temp.GetComponent<Text>();
        temp = this.transform.Find("TurnText");
        TurnText = temp.GetComponent<Text>();
    }

    public void Error(string text)
    {
        ErrorDisplay.DisplayError(text);
    }

    public void ChangePlayerHP(int currHP, int maxHP)
    {
        PlayerHP.text = "Player HP: " + currHP + "/" + maxHP; 
    }

    public void ChangeEnemyHP(int currHP, int maxHP)
    {
        EnemyHP.text = "Enemy HP: " + currHP + "/" + maxHP;
    }

    public void ShowAP(int currAP, int maxAP)
    {
        APText.text = "AP: " + currAP + "/" + maxAP;
    }

    public void ChangeTurn(int turn)
    {
        TurnText.text = "Turn Number: " + turn;
    }

    public void DisplayTurn(int DamageDealt, int DamageTaken)
    {
        UpdateText.enabled = true;
        UpdateText.text = "Player dealt " + DamageDealt + ", and received " + DamageTaken + "!";
    }
    public void TurnOffUpdate()
    {
        UpdateText.enabled = false;
    }

    public void GameOver()
    {
        UpdateText.text += "\n You Died!";
    }

    public void GameWin()
    {
        UpdateText.text += "\n You Killed the Enemy!";
    }
}
