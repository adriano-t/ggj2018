using UnityEngine;
using System.Collections;

public class LightFade : MonoBehaviour
{
    Light light;
    public float minIntensity = 0.0f;
    public float maxIntensity = 3.5f;
    public float frequency = 0.3f;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        float x = (Time.time) * frequency;
        x = x - Mathf.Floor(x); // normalized value to 0..1
        light.intensity = maxIntensity * Mathf.Sin(2 * Mathf.PI * x) + minIntensity;
    }
}