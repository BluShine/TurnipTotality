using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCarrier : MonoBehaviour
{
    public Transform carryPos;
    public Vector3 treeVelocity = new Vector3(0, 10, 20);
    public Vector3 treeRandomness = new Vector3(3, 1, 1);
    public Vector3 treeTorque = new Vector3(100, 0, 0);
    public float treeSpin = 50;
    public float treeCooldown = .5f;

    OakTree carried = null;
    TurnipSpawner turnipSpawner;

    // Start is called before the first frame update
    void Start()
    {
        turnipSpawner = GetComponent<TurnipSpawner>();
    }

    private void FixedUpdate()
    {
        if(Input.GetButton("Fire1") && carried != null)
        {
            //throw tree
            carried.transform.parent = null;
            carried.State = OakTree.OakState.Thrown;
            carried.Body.velocity = treeVelocity + Vector3.Scale(treeRandomness, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)));
            carried.Body.angularVelocity = new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), Random.Range(-1f, 1)).normalized * treeSpin;
            carried.Body.AddTorque(treeTorque, ForceMode.VelocityChange);
            carried = null;
            //delay turnips
            turnipSpawner.enabled = true;
            turnipSpawner.DelayTurnips(treeCooldown);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        OakTree tree = other.GetComponentInParent<OakTree>();
        if (carried == null && tree != null && tree.State == OakTree.OakState.Planted)
        {
            carried = tree;
            tree.State = OakTree.OakState.Carried;
            tree.transform.parent = carryPos;
            tree.transform.localPosition = Vector3.zero;
            turnipSpawner.enabled = false;
        }
    }
}
