using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cisterna : MonoBehaviour {

    Color finalColor = new Color(1f, 0.3103447f, 0f);
    Light glow;
    int c = 0;


    public void Start()
    {
        glow = GetComponentInChildren<Light>();
    }

    public void IncreaseEmission()
    {
        c++;
        Color current = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        Color nextColor = Color.Lerp(current, finalColor,Time.deltaTime*0.5f);
        glow.intensity = Mathf.Lerp(glow.intensity, 5f,Time.deltaTime*0.25f);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", nextColor);
        if(c==325)
        {
            Grate.active = true;
        }
    }
}
