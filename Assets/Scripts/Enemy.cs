using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int DealDamage()
    {
        //return a number between 90% - 110% of damage
        return Mathf.RoundToInt(Random.Range(Mathf.Floor(damage * 0.9f), Mathf.Ceil(damage * 1.1f)));
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }
}
