using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbitifier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Lumberjack lumber = other.GetComponent<Lumberjack>();
        if(other != null)
        {
            lumber.Rabbitify();
        }
    }
}
