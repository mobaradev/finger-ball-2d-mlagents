using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerGameAgent : Agent
{
    public EnvGameController EnvGameController;
    
    public GameObject ballObj;
    public SpriteRenderer fieldBackground;
    public float timer = 0.0f;
    public int movements = 0;
    public int opponentId;

    public bool isReversed;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            timer = 0;
            
            // move
            movements++;
            this.RequestDecision();
        }
    }

    private void Start()
    {
        this.opponentId = PlayerPrefs.GetInt("OpponentId");
    }

    public void Init()
    {
        this.timer = 0;
        this.movements = 0;
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
            sensor.AddObservation(new Vector2(this.CalculateReversedX(this.EnvGameController.player.transform.localPosition.x), this.EnvGameController.player.transform.localPosition.y));
        }
        else
        {
            sensor.AddObservation(new Vector2(this.transform.localPosition.x, this.transform.localPosition.y));
            sensor.AddObservation(new Vector2(this.ballObj.transform.localPosition.x, this.ballObj.transform.localPosition.y));         
            sensor.AddObservation(new Vector2(this.EnvGameController.player.transform.localPosition.x, this.EnvGameController.player.transform.localPosition.y));
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


            if (opponentId == 0)
            {
                if (this.isReversed)
                {
                    if (actionValue == 4) actionValue = 4;
                    else if (actionValue == 0) actionValue = 0;
                    else
                    {
                        float oldActionValue = actionValue;
                        actionValue = -1 * actionValue + 8;
                    }
                }
            
                float angleNumber = (actionValue/8f) * 360;
        
                this.AddForceAngle(angleNumber, 11);
            } else if (opponentId == 1)
            {
                if (this.isReversed)
                {
                    if (actionValue == 8) actionValue = 8;
                    else if (actionValue == 0) actionValue = 0;
                    else
                    {
                        float oldActionValue = actionValue;
                        actionValue = -1 * actionValue + 16;
                    }
                }
            
                float angleNumber = (actionValue/16f) * 360;
        
                this.AddForceAngle(angleNumber, 11);
            } else if (opponentId == 2)
            {
                if (this.isReversed)
                {
                    if (actionValue == 16) actionValue = 16;
                    else if (actionValue == 0) actionValue = 0;
                    else
                    {
                        float oldActionValue = actionValue;
                        actionValue = -1 * actionValue + 32;
                    }
                }
            
                float angleNumber = (actionValue/32f) * 360;
        
                this.AddForceAngle(angleNumber, 11);
            }
        }
    }
    
    public void AddForceAngle(float angle, float power)
    {
        angle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
    }
}
