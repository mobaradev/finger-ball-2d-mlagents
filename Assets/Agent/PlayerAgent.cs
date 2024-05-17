using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAgent : Agent
{
    public EnvController EnvController;
    
    public GameObject ballObj;
    public GameObject goalLeftObj;
    public GameObject goalRightObj;
    public SpriteRenderer fieldBackground;
    public float timer = 0.0f;
    public int movements = 0;

    public int AgentNumber; // 1 or 2
    public bool isReversed;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 4)
        {
            timer = 0;
            
            // move
            movements++;
            float distancePenalty = this.DistancePenalty();
            AddReward(-distancePenalty);
            this.RequestDecision();
        }
    }

    public float DistancePenalty()
    {
        // float deltaPosition = 0;
        // if (this.AgentNumber == 1)
        // {
        //     deltaPosition = this.ballObj.transform.localPosition.x - this.goalRightObj.transform.localPosition.x;            
        // } else if (this.AgentNumber == 2)
        // {
        //     deltaPosition = this.ballObj.transform.localPosition.x - this.goalLeftObj.transform.localPosition.x;
        // }
        //
        // return (10 * (1 / deltaPosition));
        return 0.5f;
    }
    
    public override void OnEpisodeBegin()
    {
        if (this.AgentNumber == 1) this.EnvController.StartEnvironment();
        this.timer = 0;
        this.movements = 0;
        this.fieldBackground.color = Color.gray;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            AddReward(0.5f);
        }
    }

    private float CalculateReversedX(float x)
    {
        return -x + 22;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (this.isReversed)
        {
            sensor.AddObservation(new Vector2(this.CalculateReversedX(this.transform.localPosition.x), this.transform.localPosition.y));
            sensor.AddObservation(new Vector2(this.CalculateReversedX(this.ballObj.transform.localPosition.x), this.ballObj.transform.localPosition.y));
        }
        else
        {
            sensor.AddObservation(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y));
            sensor.AddObservation(new Vector2(this.ballObj.transform.localPosition.x, this.ballObj.transform.localPosition.y));            
        }
        
        
        
        
         if (this.AgentNumber == 1)
         {
             if (this.isReversed)
             {
                 sensor.AddObservation(new Vector2(this.CalculateReversedX(this.EnvController.agent2.transform.localPosition.x), this.EnvController.agent2.transform.localPosition.y));
             }
             else
             {
                 sensor.AddObservation(new Vector2(this.EnvController.agent2.transform.localPosition.x, this.EnvController.agent2.transform.localPosition.y));
             }
             
         } else if (this.AgentNumber == 2)
         {
             // THERE SHOULD BE REVERSING TOO!!!!!
             if (this.isReversed)
             {
                 sensor.AddObservation(new Vector2(this.CalculateReversedX(this.EnvController.agent1.transform.localPosition.x), this.EnvController.agent1.transform.localPosition.y));
             }
             else
             {
                 sensor.AddObservation(new Vector2(this.EnvController.agent1.transform.localPosition.x, this.EnvController.agent1.transform.localPosition.y));
             }
         }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        bool isAgentSimple = true;

        if (!isAgentSimple)
        {
            float action = actions.ContinuousActions[0];
            float actionValue = action;

            float angleNumber = (actionValue + 1) * 360 / 2;
        
            this.AddForceAngle(angleNumber, 11);
        }
        else
        {
            int action = actions.DiscreteActions[0];
            int actionValue = action;
            
            if (this.isReversed)
            {
                if (actionValue == 16) actionValue = 16;
                else if (actionValue == 0) actionValue = 0;
                else
                {
                    float oldActionValue = actionValue;
                    actionValue = -1 * actionValue + 32;
                    // Debug.Log("Translated from " + oldActionValue + " to " + actionValue);
                }
            }
            
            float angleNumber = (actionValue/32f) * 360;
        
            this.AddForceAngle(angleNumber, 11);


        }
    }
    
    public void AddForceAngle(float angle, float power)
    {
        angle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
    }

    public void ScoredGoal()
    {
        AddReward(100f);
        EndEpisode();
    }

    public void LostGame()
    {
        AddReward(-50f);
        EndEpisode();
    }

    public void LimitHit()
    {
        AddReward(-50f);
        EndEpisode();
    }

}
