using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public AudioClip clip;
    bool working;
    public GameObject leva;

    void Update()
    {
       if(GetComponent<Light>().intensity>=0.9 && !working)
        {
            working = true;
            GetComponent<AudioSource>().PlayOneShot(clip);
            leva.SetActive(true);
        }
    }
}
