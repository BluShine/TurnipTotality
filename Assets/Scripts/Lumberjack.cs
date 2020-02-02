using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    //tree searching
    public LayerMask treeMask;
    public LayerMask avoidanceMask;
    public float castLength = 20;
    public float castRadius = 10;
    public float avoidDistance = 5;
    //movement
    public float moveSpeed = 10;
    public float accel = 5;
    public float turnSpeed = 10;
    public float turnAccel = 10;
    public float aimAngleThreshold = 10f;
    //prefabs
    public GameObject rabbitPrefab;
    Rigidbody body;

    OakTree target = null;

    enum SpinState { 
        Forward = 0,
        Left = 1,
        Right = 2
    }

    SpinState spinState = SpinState.Forward;
    public float spinInterval = .2f;
    float spinTimer = 0;

    public void Rabbitify()
    {
        Rigidbody rBody = Instantiate(rabbitPrefab, transform.position, transform.rotation).GetComponent<Rigidbody>();
        rBody.velocity = body.velocity;
        rBody.angularVelocity = body.angularVelocity;
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(Vector3.up * 360 * Random.value);
    }

    private void FixedUpdate()
    {
        spinTimer += Time.fixedDeltaTime;
        bool upright = transform.up.y > .5f;
        if(!upright) { return; }
        if(target != null && target.State != OakTree.OakState.Planted)
        {
            target = null;
        }
        if(target == null)
        {
            //wander randomly
            if (spinTimer > spinInterval)
            {
                spinTimer = spinTimer % spinInterval;
                spinState = (SpinState)Mathf.Max(0, Random.Range(0, 3));
            }
            //search for trees
            RaycastHit hit;
            if(Random.value < .1f && Physics.SphereCast(transform.position, castRadius, transform.forward, out hit, castLength, treeMask))
            {
                OakTree oak = hit.transform.GetComponent<OakTree>();
                if(oak != null && oak.State == OakTree.OakState.Planted)
                {
                    target = oak;
                }
            }
        } else
        {
            //collision avoidance
            RaycastHit hit;
            if (Physics.Raycast(body.worldCenterOfMass, transform.forward, out hit, avoidDistance, avoidanceMask)) {
                OakTree hitTree = hit.transform.GetComponent<OakTree>();
                if(hitTree != null && hitTree.State == OakTree.OakState.Planted)
                {
                    hitTree.ChopDown();
                }
                target = null;
            }
            else {
                //Turn towards tree
                float yRot = Vector3.SignedAngle(transform.forward, Vector3.Normalize(target.transform.position - transform.position), transform.up);
                if(Mathf.Abs(yRot) > 90)
                {
                    target = null;
                }
                if (Mathf.Abs(yRot) < aimAngleThreshold)
                {
                    spinState = SpinState.Forward;
                }
                else if (yRot < 0)
                {
                    spinState = SpinState.Left;
                }
                else
                {
                    spinState = SpinState.Right;
                }
            }
        }

        //Wander randomly.
        float m = body.velocity.magnitude;
        if (m < moveSpeed)
        {
            body.AddRelativeForce(Vector3.forward * Mathf.Min(accel * Time.fixedDeltaTime, moveSpeed - m), ForceMode.VelocityChange);
        }
        
        float yAngular = transform.InverseTransformDirection(body.angularVelocity).y;
        switch (spinState)
        {
            case SpinState.Forward:
                body.AddRelativeTorque(Vector3.up * Mathf.Min(turnAccel * Time.fixedDeltaTime, Mathf.Abs(yAngular)) * Mathf.Sign(-yAngular), ForceMode.VelocityChange);
                break;
            case SpinState.Left:
                if (yAngular > -turnSpeed)
                {
                    body.AddRelativeTorque(Vector3.up * Mathf.Max(-turnAccel * Time.fixedDeltaTime, -turnSpeed - yAngular), ForceMode.VelocityChange);
                }
                break;
            case SpinState.Right:
                if (yAngular < turnSpeed)
                {
                    body.AddRelativeTorque(Vector3.up * Mathf.Min(turnAccel * Time.fixedDeltaTime, turnSpeed - yAngular), ForceMode.VelocityChange);
                }
                break;
        }
    }
}
