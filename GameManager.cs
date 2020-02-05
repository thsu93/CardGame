using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manage in-game logic. 

//To Do:
//? of how to add edit phase

public class GameManager : MonoBehaviour
{
    public GameObject table;
    public GameObject ExecuteButton;
    GameObject Execute;
    bool createdButton = false;
    public GameObject queue;
    QueueHandler queueLogic;
    public GameObject hand;
    Hand handLogic;
    public GameObject deck;
    Deck deckLogic;
    public GameObject messager;
    UIController messagerLogic;
    int turnCount = 0;
    int PlayerHealth;
    int PlayerMaxHealth;
    int EnemyHealth;
    int EnemyMaxHealth;
    int EnemyDamage;
    int DamageTaken=0;
    int PlayerDamage=0;
    
    //NEED TO INCORPORATE OF THE GAME LOGIC, SHOULD BE IN HERE

    private void Start() 
    {        
        queueLogic = queue.GetComponent<QueueHandler>();
        deckLogic = deck.GetComponent<Deck>();
        messagerLogic = messager.GetComponent<UIController>();
        handLogic = hand.GetComponent<Hand>();
        PlayerMaxHealth = 20;
        PlayerHealth = PlayerMaxHealth;
        EnemyMaxHealth = 5;
        EnemyHealth = EnemyMaxHealth;
        EnemyDamage = 5;
    }

    private void Update()
    {
        UpdateDisplay();
        CheckIfExecutable();
    }

    void UpdateDisplay()
    {
        messagerLogic.ChangePlayerHP(PlayerHealth, PlayerMaxHealth);
        messagerLogic.ChangeEnemyHP(EnemyHealth, EnemyMaxHealth);
        messagerLogic.ShowAP(queueLogic.CurrentAP, queueLogic.MaxAP);
    }

    //Called when the Execute button is hit, calculates damage/defense values against existing player/opponent health
    //Updates player health, enemy health
    //Deck: resets to allow to deal again
    public void ExecuteTurn()
    {
        DamageTaken = EnemyDamage - queueLogic.CurrentDefenseVal;
        if (DamageTaken<0)
        {
            DamageTaken = 0;
        }
        
        PlayerDamage = queueLogic.CurrentAttackVal;
        PlayerHealth -= DamageTaken;
        EnemyHealth -= PlayerDamage;

        messagerLogic.DisplayTurn(PlayerDamage , DamageTaken);

        if (PlayerHealth <= 0)
        {
            PlayerHealth = 0;
            GameOver();
        }

        else if (EnemyHealth <= 0)
        {
            EnemyHealth = 0;
            GameWin();
        }
        
        deckLogic.isDealt = false;
        queueLogic.DisableQueue();
    }

    //Called when new hand is dealt
    //Queuehandler: Emptys queue, resets all queue values to defaults
    //Hand: re-fills hand
    public void NewTurn()
    {
        turnCount++;
        messagerLogic.ChangeTurn(turnCount);
        messagerLogic.TurnOffUpdate();
        queueLogic.Reset();
        deckLogic.isDealt = true;
        createdButton = false;
    }

    void CheckIfExecutable()
    {
        if ((hand.GetComponent<DropZone>().currentSize == 0 || queueLogic.CurrentAP==queueLogic.MaxAP) && deckLogic.isDealt && createdButton==false)
        {
            CreateExecuteButton();
            createdButton = true;
        }
        if ((hand.GetComponent<DropZone>().currentSize>0 && queueLogic.CurrentAP<queueLogic.MaxAP) && deckLogic.isDealt && createdButton==true)
        {
            Destroy(Execute);
            createdButton = false;
        }

    }
    
    //if queue is full, create execute button at some location (currently temp)
    //should this be in queuehandler!?
    void CreateExecuteButton()
    {
        Execute = Instantiate(ExecuteButton, new Vector3(1785, queueLogic.transform.position.y, 0), Quaternion.identity);
        Execute.transform.SetParent(table.transform);
    }

    void GameOver()
    {
        messagerLogic.GameOver();
        deckLogic.Reset();
        handLogic.Reset();
    }

    void GameWin()
    {
        messagerLogic.GameWin();
        deckLogic.Reset();
        handLogic.Reset();
    }
}
