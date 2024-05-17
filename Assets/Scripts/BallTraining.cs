using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallTraining : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 2.15f;
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
            SceneManager.LoadScene("Lobby");
        } else if (col.CompareTag("goal2"))
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}
