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
    int preparationBonus;
    List<int> diceValues;   //list of dice face values

    //enemy's variables
    public GameObject manager;
    Manager managerScript;

    //using placeholder enemy object for now
    public GameObject enemy;
    Enemy enemyScript;
    private int turn;
    private int startingEnemyDamage;

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
        preparationBonus = 2;

        enemyScript = enemy.GetComponent<Enemy>();
        managerScript = manager.GetComponent<Manager>();
        diceRolled = 0;
        turn = 1;
        startingEnemyDamage = enemyScript.damage;
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
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RecklessSwing(); //uses reckless swing dice
            diceRolled++;
        }

        if (diceRolled >= playerScript.dicePerTurn)
        {

            enemyScript.shield = 0;

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
            startingEnemyDamage = enemyScript.damage;
            playerScript.shield = playerScript.potentialShield;
            playerScript.potentialShield = 0;

        }

        Debug.Log("Enemy Health : " + enemyScript.health +
            "\nPlayer Health: " + playerScript.health + " Player Shield: " + playerScript.shield);

        if (enemyScript.health <= 0)
        {
            kill.Play();
            playerScript.rewardMod++;
            Debug.Log(playerScript.rewardMod);
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

    public void DealDamage()
    {
        int dmg = diceValues[RollDiceIndex()];
        if (playerScript.preparation)
        {
            dmg *= preparationBonus;
        }
        enemyScript.TakeDamage(dmg);
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
        if (playerScript.preparation)
        {
            dmg *= preparationBonus;
        }
        enemyScript.TakeDamage(dmg);
    }

    void VampiricStrike()
    {
        int dmg = 2 + RollDiceIndex(); //3 damage if roll 1, 8 damage if roll 6
        playerScript.Heal(dmg);
        if (playerScript.preparation)
        {
            dmg *= preparationBonus;
        }
        enemyScript.TakeDamage(dmg);
    }

    void Preparation()
    {
        int chance = 40 + 10 * RollDiceIndex(); //50% if rolled 1, 100% if rolled 6
        if (Random.Range(0, 100) <= chance) //if a random range is within chance
        {
            playerScript.preparation = true;
        }
    }

    void SteadyShield()
    {

        int block = 3;
        int roll = RollDiceIndex();

        if (roll % 2 == 1)
        {

            block = roll % 6;

        }

        playerScript.shield += block;

    }

    void Wildcard()
    {

        int block = 0;
        int damage = 4;
        int roll = RollDiceIndex();

        if (roll == 5)
        {

            damage = 0;
            enemyScript.isStunned = true;

        }
        else if (roll % 2 == 0)
        {

            block = 4;
            damage = 0;

            if (roll == 6)
            {

                diceRolled -= 2;

            }

        }

        playerScript.shield += block;
        enemyScript.TakeDamage(damage);

    }

    void Hinder()
    {

        int hinderance = 2;
        int roll = RollDiceIndex();

        if (hinderance % 2 == 0)
        {

            hinderance = 3;


            if (roll == 1)
            {

                hinderance = roll;

            }

        }

        enemyScript.damage -= hinderance;

        if (enemyScript.damage < 0)
        {

            enemyScript.damage = 0;

        }

    }

    void ShieldBash()
    {

        int block = 2;
        int roll = RollDiceIndex();

        playerScript.potentialShield += (int)Mathf.Ceil((float)roll / 2.0f);

        if (playerScript.potentialShield == 3)
        {

            block = 1;

        }

        playerScript.shield += block;

    }

    void Reposition()
    {

        playerScript.potentialShield += playerScript.shield / 4;
        playerScript.shield -= playerScript.shield / 4;

    }

}
