using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reckless_info : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnMouseOver();
        OnMouseExit();
    }


    void OnMouseOver()
    {
        Debug.Log("Mouse is over");
    }

    void OnMouseExit()
    {
        Debug.Log("no over");
    }
}
