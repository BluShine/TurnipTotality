using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public GameObject treePrefab;

    Animator anim;
    public float maxSpeed = 1.5f;
    public float soundDelay = .5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(1, maxSpeed);
        GetComponent<PentatonicPlayer>().PlaySound(Random.value * soundDelay);
    }

    public void BecomeTree()
    {
        GameObject.Instantiate(treePrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
