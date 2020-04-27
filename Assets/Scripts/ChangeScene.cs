using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    private Player playerScript;
    private Enemy enemyScript;
    private Combat combatScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
        combatScript = GameObject.Find("Manager").GetComponent<Combat>();
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

        string[] possibleRewards = {"RecklessSwing", "VampiricStrike", "Preparation", "SteadyShield", "Wildcard", "Hinder", "ShieldBash", "Reposition"};
        int reward1 = Random.Range(0, possibleRewards.Length);
        int reward2 = Random.Range(0, possibleRewards.Length);

        while (reward1 == reward2)
        {

            reward2 = Random.Range(0, 5);

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {

            combatScript.availableDices.Add(possibleRewards[reward1]);

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            combatScript.availableDices.Add(possibleRewards[reward2]);

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
