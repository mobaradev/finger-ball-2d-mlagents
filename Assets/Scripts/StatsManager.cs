using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int pointsPlayerLeft;
    public int pointsPlayerRight;
    public int timeouts;
    public int touches;

    public GameObject AfterGameStatsObj;

    public GameObject EnvGame;
    // Start is called before the first frame update
    void Start()
    {
        this.touches = 0;
        this.pointsPlayerLeft = 0;
        this.pointsPlayerRight = 0;
        // FindObjectOfType<AfterGameStats>().gameObject.SetActive(true);
        // FindObjectOfType<AfterGameStats>().enabled = true;
        // FindObjectOfType<AfterGameStats>().Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAferGameStats()
    {
        this.pointsPlayerLeft = FindObjectOfType<EnvGameController>().playerScore;
        this.pointsPlayerRight = FindObjectOfType<EnvGameController>().computerScore;
        this.touches = FindObjectOfType<EnvGameController>().playerTouches;
        
        HeatMapController heatMapController = FindObjectOfType<HeatMapController>();
        List<float> heat = heatMapController.GetPercentage();
        this.AfterGameStatsObj.SetActive(true);
        
        this.AfterGameStatsObj.GetComponent<AfterGameStats>().text1.SetText(this.pointsPlayerLeft + " - " + this.pointsPlayerRight);
        this.AfterGameStatsObj.GetComponent<AfterGameStats>().text2.SetText(heat[0].ToString("0.00") + " - " + heat[1].ToString("0.00") + " - " + heat[2].ToString("0.00") + " - " + heat[3].ToString("0.00") + " - " + heat[4].ToString("0.00") + " - " + heat[5].ToString("0.00") + " - " + heat[6].ToString("0.00"));
        this.AfterGameStatsObj.GetComponent<AfterGameStats>().text3.SetText(this.touches.ToString());
        Destroy(this.EnvGame);
    }
}
