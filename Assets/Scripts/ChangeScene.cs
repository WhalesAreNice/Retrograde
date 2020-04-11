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

    // Update is called once per frame
    void Update()
    {
        if (playerScript.health <= 0 || enemyScript.health <= 0)
        {
            playerScript.Reset();
            enemyScript.Reset();
        }
    }
}
