using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public GameObject[] plants;
    public int foliageMin = 4;
    public int foliageMax = 7;
    public GameObject sapling;
    public int treeMin = 2;
    public int treeMax = 4;

    public float radius = 10;

    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        int trees = Random.Range(treeMin, treeMax);
        for(int i = 0; i < trees; i++)
        {
            Vector3 dist = Random.onUnitSphere * radius;
            Vector3 pos = new Vector3(transform.position.x + dist.x, 0, transform.position.z + dist.z);
            GameObject.Instantiate(sapling, pos, Quaternion.Euler(0, Random.value * 360, 0));
        }
        int foliage = Random.Range(foliageMin, foliageMax);
        for (int i = 0; i < foliage; i++)
        {
            Vector3 dist = Random.onUnitSphere * radius * 2;
            Vector3 pos = new Vector3(transform.position.x + dist.x, 0, transform.position.z + dist.z);
            GameObject.Instantiate(plants[Random.Range(0, plants.Length)], pos, Quaternion.Euler(0, Random.value * 360, 0));
        }

        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!particles.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
