using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapArea : MonoBehaviour
{
    public HeatMapController Controller;

    public int AreaNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ball"))
        {
            this.Controller.currentArea = this.AreaNumber;
        }
    }
}
