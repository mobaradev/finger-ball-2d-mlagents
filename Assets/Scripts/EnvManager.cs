using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    public int numberOfEnv;
    public GameObject envPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GenerateEnvs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateEnvs()
    {
        for (int i = 0; i < numberOfEnv; i++)
        {
            int x = i % 16;
            int y = (int)Math.Floor((i / 16f));
            Instantiate(this.envPrefab, new Vector3(0f + x * 32f, 0f + y * 24f, 0), Quaternion.identity);
        }
    }
}
