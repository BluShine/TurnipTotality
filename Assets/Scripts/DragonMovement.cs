using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float maxSpeed = 10;
    public float accel = 30;

    Rigidbody body;

    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
        }
        if (inputVector.magnitude == 0)
        {
            if(body.velocity.magnitude < Time.fixedDeltaTime * accel)
            {
                body.velocity = new Vector3(0, body.velocity.y, 0);
            } else
            {
                body.AddForce(-body.velocity.normalized * accel);
            }
        }
        else
        {
            body.AddForce(inputVector * accel);
            Vector3 hVel = new Vector3(body.velocity.x, 0, body.velocity.z);
            if (hVel.magnitude > maxSpeed)
            {
                body.AddForce(-hVel.normalized * (hVel.magnitude - maxSpeed), ForceMode.VelocityChange);
            }
        }
        
    }
}
