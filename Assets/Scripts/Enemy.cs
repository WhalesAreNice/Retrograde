using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int shield;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 4;
        shield = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {

        Start();

    }

    public int DealDamage()
    {
        //return a number between 90% - 110% of damage
        //return Mathf.RoundToInt(Random.Range(Mathf.Floor(damage * 0.9f), Mathf.Ceil(damage * 1.1f)));
        shield = 2;
        return damage;
    }

    public void TakeDamage(int damageAmount)
    {
        //if there is a shield 
        if (shield > 0)
        {
            //if the shield is more than the incoming damage
            if (shield >= damageAmount)
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
