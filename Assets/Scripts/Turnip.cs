using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnip : MonoBehaviour
{
    public float expirationTime = 10;
    public float expirationRandomness = 5;
    public float expirationSpeed = .5f;
    public AnimationCurve expirationCurve;
    public GameObject mesh;

    float lifeTime = 0;

    Vector3 startScale = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = expirationTime + Random.value * expirationRandomness;
        startScale = mesh.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < -expirationSpeed)
        {
            Destroy(this.gameObject);
        } 
        else if (lifeTime < 0)
        {
            mesh.transform.localScale = startScale * expirationCurve.Evaluate(-lifeTime / expirationSpeed);
        }
    }
}
