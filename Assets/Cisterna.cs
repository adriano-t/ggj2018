using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cisterna : MonoBehaviour
{

    Color startGlowColor = new Color (0.5f, 0.1f, 0f);
    Color finalColor = new Color (1f, 0.3103447f, 0f);
    Light glow;
    float c = 0;
    float max = 390;
    public AudioClip boilFx;

    public void Start()
    {
        glow = GetComponentInChildren<Light>();
        GetComponent<AudioSource>().clip = boilFx;
        GetComponent<AudioSource>().volume = 0.2f;
        GetComponent<AudioSource>().Play();
        startGlowColor = GetComponent<Renderer> ().material.GetColor ("_EmissionColor");
    }
    
    public void IncreaseEmission()
    {
        c++;
        GetComponent<AudioSource>().volume = c / max ;
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp (startGlowColor, finalColor, c / max));
        if(c/max > 0.5f)
        { 
            glow.range = Mathf.Lerp (1, 5, (c/max - 0.5f) * 2 );
        }

        if (c==max)
        {
            Grate.active = true;
            Grate.start = true;
            Fire.dead = true;
        }
    }
}
