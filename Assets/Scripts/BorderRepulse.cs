using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderRepulse : MonoBehaviour
{
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(x/2, y/2));
        }
    }
}
