using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStack : ToppleBuilding
{
    
    public ParticleSystem smoke;

    public override void Topple()
    {
        base.Topple();
        if (smoke.isPlaying)
        {
            smoke.Stop();
        }
    }
}
