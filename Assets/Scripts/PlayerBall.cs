using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // this.AddForce(4, 1, 8);
        this.AddForceAngle(90,  8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddForce(float x, float y, float power)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
    }
    
    public void AddForceAngle(float angle, float power)
    {
        angle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
    }
}
