using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerScript.health = playerScript.health + 5;
            Debug.Log("Health increased");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            playerScript.dicePerTurn++;
            Debug.Log("Dice per turn increased");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            playerScript.maxDamage = playerScript.maxDamage + 2;
            Debug.Log("Damage potential increased");
        }
    }

    public void RewardCheck(){
        if (playerScript.rewardMod % 2 == 0){
            playerScript.dicePerTurn++;
            Debug.Log("Dice per turn Increased to:" + playerScript.dicePerTurn);
        }
        else{
            playerScript.health = playerScript.health + 5;
            Debug.Log("Health Increased to: " + playerScript.health);
        }
    }
}
