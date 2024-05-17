using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvGameController : MonoBehaviour
{
    public int playerScore;
    public int computerScore;
    public float timeLeft;
    
    public GameObject player;
    public List<GameObject> agents;
    public GameObject agent;
    public GameObject ballObj;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeLeftText;

    public int playerTouches;
    
    // Start is called before the first frame update
    void Start()
    {
        this.playerTouches = 0;
        int opponentId = PlayerPrefs.GetInt("OpponentId");

        for (int i = 0; i < 3; i++)
        {
            if (i != opponentId) Destroy(agents[i]);
        }
        
        
        this.agent = agents[opponentId];
        
        this.playerScore = 0;
        this.computerScore = 0;
        this.timeLeft = 150;
        
        this.PrepareRound();
    }
    
    public void PrepareRound()
    {
        this.ballObj.transform.localPosition = new Vector3(11f, 0, 0);
        this.ballObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.ballObj.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        
        this.agent.transform.localPosition = new Vector3(20.37f, 0, 0);
        this.agent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.agent.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        
        this.player.transform.localPosition = new Vector3(1.63f, 0, 0);
        this.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.player.GetComponent<Rigidbody2D>().angularVelocity = 0f;

        Time.timeScale = 2.15f;
    }

    // Update is called once per frame
    void Update()
    {
        this.timeLeft -= Time.deltaTime/2;

        if (this.timeLeft <= 0)
        {
            this.EndGame();
        }

        this.scoreText.text = this.playerScore.ToString() + " - " + this.computerScore.ToString();
        this.timeLeftText.text = Math.Round(this.timeLeft).ToString() + " s";
    }

    void EndGame()
    {
        FindObjectOfType<StatsManager>().ShowAferGameStats();
    }
    
    public void ScoredAGoal(int goalSide)
    {
        if (goalSide == 0)
        {
            this.computerScore++;
        } else if (goalSide == 1)
        {
            this.playerScore++;
        }
        
        this.PrepareRound();
    }
}
