using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public EnvController EnvController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Gol");
        // col.GetComponent<PlayerAgent>().ScoredAGoal();

        if (col.CompareTag("goal1"))
        {
            this.EnvController.ScoredAGoal(0);            
        } else if (col.CompareTag("goal2"))
        {
            this.EnvController.ScoredAGoal(1);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }

    // public void OnTriggerExit2D(Collider2D col)
    // {
    //     FindObjectOfType<PlayerAgent>().SetArea(0);
    // }
}
