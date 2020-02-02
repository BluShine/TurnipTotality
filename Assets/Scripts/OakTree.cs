using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakTree : MonoBehaviour
{
    [HideInInspector]
    private OakState state = OakState.Planted;

    public float expirationTime = 10;
    public float expirationRandomness = 5;
    public float expirationSpeed = .5f;
    public AnimationCurve expirationCurve;
    public GameObject mesh;

    public GameObject stumpPrefab;

    public float collisionSoundSpeed = 10;

    float lifeTime = 0;
    Vector3 startScale = Vector3.one;
    Rigidbody _body;
    public Rigidbody Body { get => _body; }

    PentatonicPlayer sound;

    public OakState State { 
        get => state; 
        set
        {
            state = value;
            switch(state)
            {
                case OakState.Planted:
                    transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);
                    _body.constraints = RigidbodyConstraints.FreezeAll;
                    _body.isKinematic = false;
                    break;
                case OakState.Carried:
                    _body.constraints = RigidbodyConstraints.FreezeAll;
                    //_body.isKinematic = true;
                    break;
                case OakState.Thrown:
                    _body.constraints = RigidbodyConstraints.None;
                    _body.isKinematic = false;
                    break;
            }
        }
    }

    public enum OakState
    {
        Planted,
        Carried,
        Thrown
    }

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        lifeTime = expirationTime + Random.value * expirationRandomness;
        startScale = mesh.transform.localScale;
        sound = GetComponent<PentatonicPlayer>();
    }

    public void ChopDown()
    {
        GameObject.Instantiate(stumpPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > collisionSoundSpeed)
        {
            sound.PlaySound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case OakState.Planted:
                break;
            case OakState.Carried:
                transform.localPosition = Vector3.zero;
                break;
            case OakState.Thrown:
                lifeTime -= Time.deltaTime;
                if (lifeTime < -expirationSpeed)
                {
                    Destroy(this.gameObject);
                }
                else if (lifeTime < 0)
                {
                    mesh.transform.localScale = startScale * expirationCurve.Evaluate(-lifeTime / expirationSpeed);
                }
                break;
        }
    }
}
