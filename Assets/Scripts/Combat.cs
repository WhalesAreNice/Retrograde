using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public List<string> availableDices; //list of current dices you can pull from
    List<int> activeDiceIndexes; //list of active dices for the turn
    int dicesPulledPerTurn = 5;

    //enemy's variables
    public GameObject manager;
    Manager managerScript;

    //using placeholder enemy object for now
    public GameObject enemy;
    Enemy enemyScript;
    private int turn;
    private int startingEnemyDamage;
    private bool hasSelected;

    //audio variables
    public AudioSource attack;
    public AudioSource defend;
    public AudioSource kill;
    public AudioSource damage;

    //buttons
    public Button attack_Btn;
    public Button defense_Btn;
    public Button reckless_Btn;
    //text
    public Text diceIndex;

    //dice rolls
    public int shieldroll;
    public int attackroll;
    public int recklessroll;

    public Image[] dices;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        //diceType = playerScript.diceType;
        //diceSides = playerScript.diceSides;
        diceValues = playerScript.diceValues;

        availableDices = new List<string>();
        //adding 3 attack and 3 shield dies to the list of available dices
        for(int i = 0; i < 3; i++)
        {
            availableDices.Add("attack");
            availableDices.Add("shield");
        }

        activeDiceIndexes = new List<int>();

        preparationBonus = 2;
        hasSelected = false;

    enemyScript = enemy.GetComponent<Enemy>();
        managerScript = manager.GetComponent<Manager>();
        diceRolled = 0;
        turn = 1;
        startingEnemyDamage = enemyScript.damage;

        //buttons for different dices
        attack_Btn.onClick.AddListener(DealDamage);
        defense_Btn.onClick.AddListener(MakeShield);
        reckless_Btn.onClick.AddListener(RecklessSwing);

        NewUsableDices();
    }

    // Update is called once per frame
    void Update()
    {
        //diceIndex.text = RollDiceIndex().ToString();

        if (enemyScript.health + enemyScript.shield > 8 && !hasSelected)
        {

            managerScript.enemyIntent = "attack";

            hasSelected = true;

        }
        else if (enemyScript.health + enemyScript.shield < 6 && !hasSelected)
        {

            managerScript.enemyIntent = "block";

            hasSelected = true;

        }
        else if (enemyScript.health + enemyScript.shield > 6 && !hasSelected)
        {

            managerScript.enemyIntent = "attackAndBlock";

            hasSelected = true;

        }

        if (diceRolled >= playerScript.dicePerTurn)
        {

            enemyScript.shield = 0;

            if (managerScript.enemyIntent == "attack")
            {

                playerScript.TakeDamage(enemyScript.DealDamage(2) * 2);//takes damage from enemy 

            }
            else if (managerScript.enemyIntent == "block")
            {

                enemyScript.GainBlock();//takes damage from enemy 

            }
            else if(managerScript.enemyIntent == "attackAndBlock")
            {

                playerScript.TakeDamage(enemyScript.DealDamageAndGainBlock());//takes damage from enemy 

            }


            turn++;
            NewUsableDices();
            diceRolled = 0;
            startingEnemyDamage = enemyScript.damage;
            playerScript.shield = playerScript.potentialShield;
            playerScript.potentialShield = 0;
            hasSelected = false;

        }

        //Debug.Log("Enemy Health : " + enemyScript.health +
        //    "\nPlayer Health: " + playerScript.health + " Player Shield: " + playerScript.shield);

        if (enemyScript.health <= 0)
        {
            kill.Play();
            playerScript.rewardMod++;
            Debug.Log(playerScript.rewardMod);

            turn = 0;
            NewUsableDices();
            diceRolled = 0;
            playerScript.potentialShield = 0;
            hasSelected = false;
        }
    }

    void NewUsableDices()
    {
        activeDiceIndexes.Clear();

        //loop forever until there are enough active dices
        while(activeDiceIndexes.Count < dicesPulledPerTurn)
        {
            bool alreadyActive = false; //check for if the dice is active or not 
            int index = Random.Range(0, availableDices.Count); //gets a random number (index)
            //loop through the index list to see if there is the same number
            for(int i = 0; i < activeDiceIndexes.Count; i++)
            {
                //checks to make sure there's no duplicates of the same index
                if(activeDiceIndexes[i] == index)
                {
                    alreadyActive = true;
                }
            }

            //if alreadyActive is still false, add the index to the list
            if (!alreadyActive)
            {
                activeDiceIndexes.Add(index);
                //Debug.Log("adding dice #" + index + " to list");
            }
        }
        for (int i = 0; i < activeDiceIndexes.Count; i++)
        {
            Debug.Log(availableDices[activeDiceIndexes[i]]);
        }
    }

    int RollDiceIndex()
    {
        return Random.Range(0, 6); //returns the index between 0 to amount of dice sides
    }

    void MakeShield()
    {
        shieldroll = RollDiceIndex();
        showDice(shieldroll);
        //diceIndex.text = shieldroll.ToString();
        playerScript.shield += diceValues[shieldroll];
        defend.Play();

        diceRolled++;
    }

    public void DealDamage()
    {
        attackroll = RollDiceIndex();
        showDice(attackroll);
        //diceIndex.text = attackroll.ToString();
        int dmg = diceValues[attackroll];
        if (playerScript.preparation)
        {
            dmg *= preparationBonus;
        }
        enemyScript.TakeDamage(dmg);
        attack.Play();

        diceRolled++;
    }

    void RecklessSwing()
    {
        recklessroll = RollDiceIndex();
        showDice(recklessroll);
        //diceIndex.text = recklessroll.ToString();
        int dmg = 4 + recklessroll; // 5 damage if roll 1, 10 damage if roll 6
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

        diceRolled++;
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
    
    void showDice(int rollnum)
    {
        for (int i = 0; i < 6; i++)
        {
            if(i == rollnum)
            {
                dices[i].enabled = true;
            }
            else
            {
                dices[i].enabled = false;
            }
        }
    }
}
