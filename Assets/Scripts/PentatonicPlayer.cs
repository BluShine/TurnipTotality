using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentatonicPlayer : MonoBehaviour
{
    public bool playOnStart = false;

    public AudioClip[] scale;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (playOnStart)
        {
            PlaySound();
        }
    }

    public void PlaySound(float delay = 0)
    {
        if (source != null && !source.isPlaying)
        {
            source.clip = scale[Random.Range(0, scale.Length)];
            source.PlayDelayed(delay + Random.value * Time.fixedDeltaTime);
        }
    }
}
