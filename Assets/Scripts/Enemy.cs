using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int defense;
    public int shield;
    public bool isStunned;

    
    public Image[] hearts;
    public Image[] shields;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 3;
        defense = 5;
        //health = 8 + Random.Range(0, 6);
        //damage = 5 + Random.Range(0, 4);
        //defense = 4 + Random.Range(0, 6);
        shield = 0;
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //displaying enemy health
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        for (int i = 0; i < shields.Length; i++)
        {
            if (i < shield)
            {
                shields[i].enabled = true;
            }
            else
            {
                shields[i].enabled = false;
            }
        }
    }

    public void Reset()
    {

        Start();

    }

    public int DealDamage(int numberOfAttacks)
    {

        if (!isStunned)
        {

            isStunned = false;

            return damage - (damage / 4) * (numberOfAttacks - 1);

        }

        return 0;

    }

    public void GainBlock()
    {

        if (!isStunned)
        {

            isStunned = false;
            shield = defense;

        }

    }

    public int DealDamageAndGainBlock()
    {

        shield = defense * 2 / 3;
        return damage / 2;

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
