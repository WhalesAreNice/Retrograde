using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //player's variables
    public GameObject player;
    Player playerScript;
    string diceType;
    int diceSides;
    List<int> diceValues;   //list of dice face values

    //enemy's variables
    public GameObject manager;
    Manager managerScript;

    //using placeholder enemy object for now
    public GameObject enemy;
    Enemy enemyScript;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        //diceType = playerScript.diceType;
        //diceSides = playerScript.diceSides;
        diceValues = playerScript.diceValues;

        enemyScript = enemy.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //placeholder for now to test code
        if (Input.GetKeyDown(KeyCode.W))
        {
            DealDamage(); //player deals damage first
            playerScript.TakeDamage(enemyScript.DealDamage()); //takes damage from enemy 
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MakeShield(); //create a shield for the player
            playerScript.TakeDamage(enemyScript.DealDamage()); //takes damage from enemy 
        }

        Debug.Log("Enemy Health : " + enemyScript.health +
            "\nPlayer Health: " + playerScript.health + " Player Shield: " + playerScript.shield);
    }

    int RollDiceIndex()
    {
        return Random.Range(0, 6); //returns the index between 0 to amount of dice sides
    }

    void MakeShield()
    {
        playerScript.shield += diceValues[RollDiceIndex()];
    }

    void DealDamage()
    {
        enemyScript.TakeDamage(diceValues[RollDiceIndex()]);
    }
}
