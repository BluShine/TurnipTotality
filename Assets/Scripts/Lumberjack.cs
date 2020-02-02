using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : MonoBehaviour
{
    public GameObject rabbitPrefab;
    Rigidbody body;


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
