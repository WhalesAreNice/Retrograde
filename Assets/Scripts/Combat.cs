using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    //player's variables
    public GameObject player;
    Player playerScript;
    string diceType;
    public int diceRolled;
    int diceSides;
    List<int> diceValues;   //list of dice face values

    //enemy's variables
    public GameObject manager;
    Manager managerScript;

    //using placeholder enemy object for now
    public GameObject enemy;
    Enemy enemyScript;
    private int turn;

    //audio variables
    public AudioSource attack;
    public AudioSource defend;
    public AudioSource kill;
    public AudioSource damage;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        //diceType = playerScript.diceType;
        //diceSides = playerScript.diceSides;
        diceValues = playerScript.diceValues;

        enemyScript = enemy.GetComponent<Enemy>();
        managerScript = manager.GetComponent<Manager>();
        diceRolled = 0;
        turn = 1;
    }

    // Update is called once per frame
    void Update()
    {


        if (turn % 3 == 1)
        {

            managerScript.enemyIntent = "attack";

        }
        else if (turn % 3 == 2)
        {

            managerScript.enemyIntent = "block";

        }
        else
        {

            managerScript.enemyIntent = "attackAndBlock";

        }

        //placeholder for now to test code
        if (Input.GetKeyDown(KeyCode.W))
        {
            DealDamage(); //player deals damage first
            diceRolled++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            MakeShield(); //create a shield for the player
            diceRolled++;
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            RecklessSwing(); //uses reckless swing dice
            diceRolled++;
        }

        if (diceRolled >= playerScript.dicePerTurn)
        {

            if (turn % 3 == 1)
            {

                playerScript.TakeDamage(enemyScript.DealDamage(2) * 2);//takes damage from enemy 

            }
            else if (turn % 3 == 2)
            {

                enemyScript.GainBlock();//takes damage from enemy 

            }
            else
            {

                playerScript.TakeDamage(enemyScript.DealDamageAndGainBlock());//takes damage from enemy 

            }


            turn++;
            diceRolled = 0;

        }

        Debug.Log("Enemy Health : " + enemyScript.health +
            "\nPlayer Health: " + playerScript.health + " Player Shield: " + playerScript.shield);

        if (enemyScript.health == 0)
        {
            kill.Play();
        }
    }

    int RollDiceIndex()
    {
        return Random.Range(0, 6); //returns the index between 0 to amount of dice sides
    }

    void MakeShield()
    {
        playerScript.shield += diceValues[RollDiceIndex()];
        defend.Play();
    }

    void DealDamage()
    {
        enemyScript.TakeDamage(diceValues[RollDiceIndex()]);
        attack.Play();
    }

    void RecklessSwing()
    {
        int dmg = 4 + RollDiceIndex(); // 5 damage if roll 1, 10 damage if roll 6
        int selfDmgChance = 60 - 10 * RollDiceIndex(); //50% if roll 1, 0% if roll 6
        int selfDamageAmount = 5; //the amount of damage the player takes if they self damage themselves
        if (Random.Range(0, 100) <= selfDmgChance) //if a random chance is within selfDamageChance
        {
            playerScript.TakeDamage(selfDamageAmount);
        }
        enemyScript.TakeDamage(dmg);
    }

}
