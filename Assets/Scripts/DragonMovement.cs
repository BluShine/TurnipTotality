using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float maxSpeed = 10;
    public float accel = 30;

    Rigidbody body;

    //flapping
    public Animator flapping;
    float fVel = 0;
    public float flappingSpeedBoost = 2;

    //Tilting
    public Transform tiltForward;
    public Transform tiltLeft;
    public Transform tiltRight;
    public Transform dragonMesh;
    Vector2 smoothInput = Vector2.zero;
    Vector2 smoothInputVel = Vector2.zero;
    Vector3 inputVector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //tilting
        smoothInput = Vector2.SmoothDamp(smoothInput, new Vector2(inputVector.x, inputVector.z), ref smoothInputVel, .2f, 10, Time.fixedDeltaTime);
        Quaternion rotation = Quaternion.identity;
        if(smoothInput.x < 0)
        {
            rotation = Quaternion.Lerp(rotation, tiltLeft.rotation, Mathf.Abs(smoothInput.x));
        } else
        {
            rotation = Quaternion.Lerp(rotation, tiltRight.rotation, Mathf.Abs(smoothInput.x));
        }
        if (smoothInput.y > 0)
        {
            rotation = Quaternion.Lerp(rotation, tiltForward.rotation, Mathf.Abs(smoothInput.y));
        }
        else
        {
            rotation = Quaternion.Lerp(rotation, Quaternion.Euler(-tiltForward.rotation.eulerAngles), Mathf.Abs(smoothInput.y));
        }
        dragonMesh.transform.rotation = rotation;
    }

    void FixedUpdate()
    {
        //input
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputVector.magnitude > 1)
        {
            inputVector.Normalize();
        }

        //flapping
        flapping.speed = Mathf.SmoothDamp(flapping.speed, 
            1 + flappingSpeedBoost * inputVector.magnitude, 
            ref fVel, .2f, 10, Time.fixedDeltaTime);

        //movement
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
