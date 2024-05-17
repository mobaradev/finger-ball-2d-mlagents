using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpponentId(int id)
    {
        PlayerPrefs.SetInt("OpponentId", id);
        SceneManager.LoadScene("GameScene");
    }

    public void Training()
    {
        SceneManager.LoadScene("TrainingScene");
    }
}
