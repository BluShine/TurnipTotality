using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBounce : MonoBehaviour
{
    public float castLength = .5f;
    public float jumpForce = 5;
    public Vector3 jumpRandomness = Vector3.one;
    public float jumpSpin = 100;
    public float jumpCooldown = .2f;
    public float cooldownRandomness = .5f;
    public LayerMask castMask;
    public float minVelocity = .2f;

    float cooldown = 0;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Mathf.Abs(body.velocity.y) < minVelocity && cooldown <= 0)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, castLength, castMask))
            {
                cooldown = jumpCooldown + cooldownRandomness * Random.value;
                body.AddRelativeForce(Vector3.up * jumpForce + 
                    Vector3.Scale(jumpRandomness, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(0, 1f))),
                    ForceMode.VelocityChange);
                body.AddRelativeTorque(0, Random.Range(-1f, 1) * jumpSpin, 0);
            } 
            else
            {
                cooldown = cooldownRandomness * Random.value;
            }
        }
        cooldown -= Time.fixedDeltaTime;
    }
}
