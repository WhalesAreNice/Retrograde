﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public int defense;
    public int shield;
    public bool isStunned;
    // Start is called before the first frame update
    void Start()
    {
        health = 25;
        damage = 12;
        defense = 10;
        shield = 0;
        isStunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
