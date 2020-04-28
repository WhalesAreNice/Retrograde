using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Text rewardOptions;

    private Player playerScript;
    private Enemy enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    void Update()
    {
        if (playerScript.health <= 0)
        {
            rewardOptions.text = "You have died!";
            playerScript.Reset();
            enemyScript.Reset();
        }
        else if (enemyScript.health <= 0){
            //number of enemies killed = reward
            RewardCheck();
            enemyScript.Reset();
        }
    }

    public void Reward()
    {
        //ideas:
        //permanent buff to attack/def dice
        //access to different dice at start
        //increase to num of dice per turn

        rewardOptions.text = "Enemy killed, pick your reward! \n (Z) Health Increase \n (X) Increase Dice Per Turn \n (C) Increase Damage Potential";

        if (Input.GetKey(KeyCode.Z))
        {
            playerScript.health = playerScript.health + 5;
            Debug.Log("Health increased");
            rewardOptions.text = "Health increased";
            //rewardOptions.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            playerScript.dicePerTurn++;
            Debug.Log("Dice per turn increased");
            rewardOptions.text = "Dice per turn increased";
            //rewardOptions.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            playerScript.maxDamage = playerScript.maxDamage + 2;
            Debug.Log("Damage potential increased");
            rewardOptions.text = "Damage potential increased";
            //rewardOptions.text = "";
        }
    }

    public void RewardCheck(){
        if (playerScript.rewardMod % 2 == 0){
            playerScript.dicePerTurn++;
            Debug.Log("Dice per turn Increased to: " + playerScript.dicePerTurn);
            rewardOptions.text = "Enemy killed!\nDice per turn Increased to: " + playerScript.dicePerTurn;
        }
        else{
            playerScript.health = playerScript.health + 5;
            Debug.Log("Health Increased to: " + playerScript.health);
            rewardOptions.text = "Enemy killed!\nHealth Increased to: " + playerScript.health;
        }
    }
}
