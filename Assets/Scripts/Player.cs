using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int shield;
    public int dicePerTurn;
    public List<int> diceValues;
    public int potentialShield;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        shield = 0;
        dicePerTurn = 3;
        for (int i = 1; i <= 3; i++)
        {
            diceValues.Add(i);
            diceValues.Add(i);
        }
        potentialShield = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {

        Start();

    }

    public void TakeDamage(int damageAmount)
    {
        //if there is a shield 
        if(shield > 0)
        {
            //if the shield is more than the incoming damage
            if(shield >= damageAmount)
            {
                shield -= damageAmount;
            }
            else //if sheild isn't, then take damage on shield first and the rest onto health
            {
                int damageLeft = damageAmount - shield;
                health -= damageLeft;
            }
        }
        else //no shield available so take damage on health
        {
            health -= damageAmount;
        }
    }

}
