using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapController : MonoBehaviour
{
    public int currentArea;

    public List<float> scores;
    
    // Start is called before the first frame update
    void Start()
    {
        this.scores = new List<float>();
        for (int i = 0; i < 7; i++) this.scores.Add(0f);
        
        this.currentArea = 4;
    }

    // Update is called once per frame
    void Update()
    {
        this.scores[this.currentArea] += Time.deltaTime;
    }

    public List<float> GetPercentage()
    {
        float sum = 0;
        for (int i = 0; i < 7; i++)
        {
            sum += this.scores[i];
        }

        List<float> percentage = new List<float>();
        
        for (int i = 0; i < 7; i++)
        {
            percentage.Add(this.scores[i] / sum * 100f);
        }

        return percentage;
    }
}
