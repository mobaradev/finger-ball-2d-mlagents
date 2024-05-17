using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public FixedJoystick fixedJoystick;
    public DynamicJoystick DynamicJoystick;
    public Rigidbody2D rigidbody2D;
    
    public float timer = 0.0f;
    private bool _canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        this.DynamicJoystick = FindObjectOfType<DynamicJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     var worldMousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.x, 0));
        //     var direction = worldMousePosition - this.transform.position;
        //     direction.Normalize();
        //     
        //     AddForce(direction);
        // }

        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            timer = 0;
            _canMove = true;
        }
    }
    
    public void FixedUpdate()
    {
        // Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        Vector2 direction = new Vector2(DynamicJoystick.Horizontal, DynamicJoystick.Vertical);
        if (direction.magnitude > 0.5f && _canMove)
        {
            Debug.Log("dir mag = " + direction.magnitude);
            rigidbody2D.AddForce(direction.normalized * 11f, ForceMode2D.Impulse);
            _canMove = false;
        }
    }
    
    public void AddForce(Vector2 direction)
    {
        this.GetComponent<Rigidbody2D>().AddForce(direction * 11f, ForceMode2D.Impulse);
    }
    
    public void AddForceAngle(float angle, float power)
    {
        angle *= Mathf.Deg2Rad;
        float x = Mathf.Sin(angle);
        float y = Mathf.Cos(angle);
        
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y) * power, ForceMode2D.Impulse);
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball")) FindObjectOfType<EnvGameController>().playerTouches++;
    }
}
