using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public GameObject treePrefab;

    Animator anim;
    public float maxSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(1, maxSpeed);
    }

    public void BecomeTree()
    {
        GameObject.Instantiate(treePrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
