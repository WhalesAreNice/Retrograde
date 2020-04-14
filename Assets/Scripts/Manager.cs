using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public string enemyIntent = "attack";
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text enemyActionText;
    public Text Instructions;
    //public Combat combat;
    public GameObject player;
    public GameObject enemy;
    public GameObject manager;

    private Player playerScript;
    private Enemy enemyScript;
    private Combat combatScript;

    // Start is called before the first frame update
    void Start()
    {

        playerScript = player.GetComponent<Player>();
        enemyScript = enemy.GetComponent<Enemy>();
        combatScript = manager.GetComponent<Combat>();

    }

    // Update is called once per frame
    void Update()
    {

        // Resets the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerScript.Reset(); //create a shield for the player
            enemyScript.Reset();//takes damage from enemy 
        }

        // Updates the displayed amount of health for the player and enemy
        playerHealthText.text = "Player Health: " + playerScript.health;
        enemyHealthText.text = "Enemy Health: " + enemyScript.health + "\nEnemy Shield: " + enemyScript.shield;

        // Informs the player of what the enemy will do on it's turn based on what its intent is.
        if (enemyIntent == "attack")
        {

            enemyActionText.text = "The enemy intends to deal " + enemyScript.DealDamage(2) + " damage to you two times";

        }
        else if (enemyIntent == "block")
        {

            enemyActionText.text = "The enemy intends to gain " + enemyScript.defense + " shield";

        }
        else if (enemyIntent == "attackAndBlock")
        {

            enemyActionText.text = "The enemy intends to deal " + enemyScript.damage / 2 + " damage to you and gain " + (enemyScript.defense * 2 / 3) + " shield";

        }

        // Tells the player how to play the game
        Instructions.text = "Press W to roll and attack die and E to roll a defense die. Both dice will roll a value between 1 and 3.\nYou can roll " + (playerScript.dicePerTurn - combatScript.diceRolled) + " more dice before the enemy takes their action.\nPress R to reset the game.";

    }

}
