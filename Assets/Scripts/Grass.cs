using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public AnimationCurve curve;
    public float randomDelay = 2;
    public float scaleSpeed = 3;
    public float scaleRandomness = 1.5f;

    float delay = 0;
    float scale = 0;
    float endScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        endScale = transform.localScale.y * Random.Range(1f, scaleRandomness);
        delay = Random.value * randomDelay;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        delay = Mathf.Max(0, delay - Time.deltaTime);
        if (delay == 0)
        {
            if (scale < endScale)
            {
                scale = Mathf.Min(scale + Time.deltaTime * scaleSpeed, endScale);
                transform.localScale = Vector3.one * curve.Evaluate(scale / endScale) * endScale;
            }
        }
    }
}
