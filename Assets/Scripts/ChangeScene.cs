using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Text rewardOptions;

    private Player playerScript;
    private Enemy enemyScript;
    private Combat combatScript;
    private bool notChosen;
    private string[] possibleRewards = { "Reckless Swing", "Vampiric Strike", "Preparation", "Steady Shield", "Wildcard", "Hinder", "Shield Bash", "Reposition" };
    int reward1;
    int reward2;

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
            rewardOptions.text = "You have died!";
            playerScript.Reset();
            enemyScript.Reset();
        }
        else if (enemyScript.health <= 0){
            //number of enemies killed = reward
            Reward();
            enemyScript.Reset();
        }

        if (Input.GetKeyDown(KeyCode.Z) && notChosen)
        {

            combatScript.availableDices.Add(possibleRewards[reward1]);
            rewardOptions.text = "";
            notChosen = false;
        }
        else if (Input.GetKeyDown(KeyCode.X) && notChosen)
        {
            combatScript.availableDices.Add(possibleRewards[reward2]);
            rewardOptions.text = "";
            notChosen = false;

        }
    }

    public void Reward()
    {
        //ideas:
        //permanent buff to attack/def dice
        //access to different dice at start
        //increase to num of dice per turn

        reward1 = Random.Range(0, possibleRewards.Length);
        reward2 = Random.Range(0, possibleRewards.Length);

        while (reward1 == reward2)
        {

            reward2 = Random.Range(0, possibleRewards.Length);

        }
        rewardOptions.text = "Enemy killed, pick your reward! \n (Z) " + possibleRewards[reward1] + " \n (X) " + possibleRewards[reward2];

        notChosen = true;
    }

    public void RewardCheck(){
        if (playerScript.rewardMod % 2 == 0){
            playerScript.dicePerTurn++;
            Debug.Log("Dice per turn Increased to: " + playerScript.dicePerTurn);
            //rewardOptions.text = "Enemy killed!\nDice per turn Increased to: " + playerScript.dicePerTurn;
        }
        else{
            playerScript.health = playerScript.health + 5;
            Debug.Log("Health Increased to: " + playerScript.health);
            //rewardOptions.text = "Enemy killed!\nHealth Increased to: " + playerScript.health;
        }
    }
}
