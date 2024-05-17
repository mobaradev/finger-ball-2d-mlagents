using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnvController : MonoBehaviour
{
    public SpriteRenderer fieldBackground;
    
    public GameObject agent1;
    public GameObject agent2;
    public GameObject ballObj;
    
    public TextMeshPro pointsAgent1Text;
    public TextMeshPro pointsAgent2Text;
    
    private int pointsAgent1 = 0;
    private int pointsAgent2 = 0;

    public StatsManager StatsManager;

    private void Start()
    {
        this.StatsManager = FindObjectOfType<StatsManager>();
    }


    public void StartEnvironment()
    {
        this.ballObj.transform.localPosition = new Vector3(11f, 0, 0);
        // this.ballObj.transform.localPosition = new Vector3(Random.Range(1.5f, 18.67f), Random.Range(-3.5f, 3.5f), 0);
        this.ballObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.ballObj.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        
        //this.agent1.transform.localPosition = new Vector3(1.67f, 0, 0);
        this.agent1.transform.localPosition = new Vector3(20.37f, 0, 0);
         //this.agent1.transform.localPosition = new Vector3(Random.Range(2, 20f), Random.Range(-3.64f, 3.64f), 0);
        this.agent1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.agent1.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        
        this.agent2.transform.localPosition = new Vector3(1.63f, 0, 0);
        this.agent2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.agent2.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        
        this.fieldBackground.color = Color.gray;
    }

    public void Update()
    {
        int movementsLimit = 300;
        if (agent1.GetComponent<PlayerAgent>().movements > movementsLimit ||
            agent2.GetComponent<PlayerAgent>().movements > movementsLimit)
        {
            agent1.GetComponent<PlayerAgent>().LimitHit();
            agent2.GetComponent<PlayerAgent>().LimitHit();
            this.StatsManager.timeouts++;
        }
    }

    /*
     * agent1 starts left side and is shooting a goal to right goal (goalSide = 1)
     * agent2 starts right side and is shooting a goal to left goal (goalSide = 0)
     */
    
    public void ScoredAGoal(int goalSide)
    {
        bool isAgent2Present = true;
        
        if (goalSide == 0)
        {
            this.pointsAgent2Text.SetText("" + this.pointsAgent1++);
            if (this.agent1.GetComponent<PlayerAgent>().isReversed)
            {
                this.StatsManager.pointsPlayerLeft++;
                this.agent1.GetComponent<PlayerAgent>().ScoredGoal();
                if(isAgent2Present) this.agent2.GetComponent<PlayerAgent>().LostGame();
            }
            else
            {
                this.StatsManager.pointsPlayerRight++;
                this.agent1.GetComponent<PlayerAgent>().LostGame();
                if(isAgent2Present) this.agent2.GetComponent<PlayerAgent>().ScoredGoal();
            }
            
            this.fieldBackground.color = Color.blue;
        } else if (goalSide == 1)
        {
            this.pointsAgent1Text.SetText("" + this.pointsAgent2++); ;
            
            if (this.agent1.GetComponent<PlayerAgent>().isReversed)
            {
                this.StatsManager.pointsPlayerRight++;
                this.agent1.GetComponent<PlayerAgent>().LostGame();
                if(isAgent2Present) this.agent2.GetComponent<PlayerAgent>().ScoredGoal();
            }
            else
            {
                this.StatsManager.pointsPlayerLeft++;
                this.agent1.GetComponent<PlayerAgent>().ScoredGoal();         
                if(isAgent2Present) this.agent2.GetComponent<PlayerAgent>().LostGame();
            }
            
            //this.agent2.GetComponent<PlayerAgent>().LostGame();

            this.fieldBackground.color = Color.blue;
        }
    }
}
