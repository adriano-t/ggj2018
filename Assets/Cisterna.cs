using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cisterna : MonoBehaviour {

    Color finalColor = new Color(1f, 0.3103447f, 0f);

    public void IncreaseEmission()
    {
        Color current = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        Color nextColor = Color.Lerp(current, finalColor,Time.deltaTime);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", nextColor);
    }
}
