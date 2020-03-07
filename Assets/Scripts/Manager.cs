using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    public Text playerHealthText;
    public Text enemyHealthText;
    public Text enemyActionText;
    public Text Instructions;
    //public Combat combat;
    public GameObject player;
    public GameObject enemy;

    private Player playerScript;
    private Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {

        playerScript = player.GetComponent<Player>();
        enemyScript = enemy.GetComponent<Enemy>();

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
        enemyHealthText.text = "Enemy Health: " + enemyScript.health;
        // Informs the player of what the enemy will do on it's turn
        enemyActionText.text = "The enemy intends to deal " + enemyScript.damage + " damage to you and block " + enemyScript.shield + " incoming damage.";
        // Tells the player how to play the game
        Instructions.text = "Press W to roll and attack die and E to roll a defense die. Both dice will roll a value between 1 and 3. Press R to reset the game.";

    }

}
