using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public AudioClip clip;

    public void PlayClip()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
