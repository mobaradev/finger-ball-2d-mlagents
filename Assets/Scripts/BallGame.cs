using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGame : MonoBehaviour
{
    public EnvGameController EnvGameController;
    
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
            this.EnvGameController.ScoredAGoal(0);            
        } else if (col.CompareTag("goal2"))
        {
            this.EnvGameController.ScoredAGoal(1);
        }
    }
}
