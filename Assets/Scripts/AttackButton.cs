using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{

    GameObject manager;
    Combat combatScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {

        combatScript.DealDamage(); //player deals damage first
        combatScript.diceRolled++;

    }

}
