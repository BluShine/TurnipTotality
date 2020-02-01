using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnipSpawner : MonoBehaviour
{

    public GameObject turnipPrefab;
    public Vector3 turnipVelocity = new Vector3(0, 0, 10);
    public Transform turnipPoint;
    public float turnipRate = .1f;
    public Vector3 turnipRandomness = new Vector3(3, 1, 1);
    public float turnipSpin = 100;
    float turnipCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DelayTurnips(float delay)
    {
        turnipCooldown = Mathf.Max(turnipCooldown, delay);
    }

    private void FixedUpdate()
    {

        if (Input.GetButton("Fire1"))
        {
            if (turnipCooldown <= 0)
            {
                Rigidbody turnip = Instantiate(turnipPrefab, turnipPoint.position, 
                    Quaternion.Euler(new Vector3(Random.value, Random.value, Random.value) * 360f)).GetComponent<Rigidbody>();
                turnip.velocity = turnipVelocity + Vector3.Scale(turnipRandomness, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)));
                turnip.angularVelocity = new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)).normalized * turnipSpin;
                turnipCooldown += turnipRate;
            }
        }
        turnipCooldown = Mathf.Max(0, turnipCooldown - Time.fixedDeltaTime);
    }
}
