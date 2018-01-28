using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cisterna : MonoBehaviour {

    Color finalColor = new Color(1f, 0.3103447f, 0f);
    Light glow;
    float c = 0;
    float max = 390;

    public void Start()
    {
        glow = GetComponentInChildren<Light>();
    }

    public void IncreaseEmission()
    {
        c++; 
        Debug.Log (c / max);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp (Color.black, finalColor, c / max));
        if(c/max > 0.5f)
        { 
            glow.range = Mathf.Lerp (1, 5, (c/max - 0.5f) * 2 );
        }

        if (c==max)
        {
            Grate.active = true;
            Fire.dead = true;
        }
    }
}
