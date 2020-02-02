using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppleBuilding : MonoBehaviour
{
    public float tipAmount = .5f;

    public float deathDelay = 2.5f;
    public float deathRandomness = .25f;

    float delay = 0;
    public Explosion deathSplosion;

    bool topple = false;

    // Start is called before the first frame update
    void Start()
    {
        delay = deathDelay + Random.value * deathRandomness;
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(transform.up.y) < tipAmount)
        {
            if (!topple)
            {
                Topple();
            }
        }
        if(topple)
        {
            delay -= Time.fixedDeltaTime;
            if(delay <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void Topple()
    {
        topple = true;
    }

    private void OnDestroy()
    {
        Vector3 c = GetComponent<Collider>().bounds.center;
        if(c.y > 0)
        {
            GameObject.Instantiate(deathSplosion, new Vector3(c.x, 0, c.z), Quaternion.identity);
        }
    }
}
