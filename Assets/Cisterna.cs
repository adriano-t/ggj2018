using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cisterna : MonoBehaviour {

    Color finalColor = new Color(1f, 0.3103447f, 0f);
    Light glow;

    public void Start()
    {
        glow = GetComponentInChildren<Light>();
    }
    public void IncreaseEmission()
    {
        Color current = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        Color nextColor = Color.Lerp(current, finalColor,Time.deltaTime*0.5f);
        glow.intensity = Mathf.Lerp(glow.intensity, 5f,Time.deltaTime*0.5f);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", nextColor);
    }
}
