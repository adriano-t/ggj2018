using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public AudioClip clip;
    bool working;
    public GameObject light;
    public PlayerController pc;

    void Update()
    {
       if(GetComponent<Light>().intensity>=0.7f && !working)
        {
            working = true;
            GetComponent<AudioSource>().PlayOneShot(clip);
            pc.done = true;
            light.SetActive(true);
        }
    }
}
